using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Diagnostics;

namespace WinformMultiWindowExampleCore
{
	public static class SingleInstance
	{
		/// <summary>
		/// TokenSource for canceling waiting to pipe
		/// </summary>
		private static CancellationTokenSource stopWaitingPipeConnectionTokenSource;

		/// <summary>
		/// NamedPipeServerStream for communications.
		/// </summary>
		private static NamedPipeServerStream pipeServer;

		/// <summary>
		/// Application mutex.
		/// </summary>
		static System.Threading.Mutex singleInstanceMutex;

		internal delegate void MessageReciveHandler(string[] args);

		/// <summary>
		/// Message receive event from another app instances
		/// </summary>
		internal static event MessageReciveHandler MessageReceived;

		/// <summary>
		/// Checks if the instance of the application attempting to start is the first instance. 
		/// If not, activates the first instance.
		/// </summary>
		/// <returns>True if this is the first instance of the application.</returns>
		public static bool InitializeAsFirstInstance(string uniqueName)
		{
			var commandLineArgs = Environment.GetCommandLineArgs();

			// Build unique application Id and the channel name.
			string applicationIdentifier = uniqueName + Environment.UserName;
			string channelName = String.Concat(applicationIdentifier, ":", "SingeInstanceChannel");

			singleInstanceMutex = new Mutex(true, applicationIdentifier, out var isSingleInstance);

			if (isSingleInstance)
			{
				CreateRemoteService(channelName);
			}
			else
			{
				SignalFirstInstance(channelName, commandLineArgs);
			}

			return isSingleInstance;
		}

		/// <summary>
		/// Cleans up single-instance code, clearing shared resources, mutexes, etc.
		/// </summary>
		public static void Cleanup()
		{
			if (stopWaitingPipeConnectionTokenSource != null)
			{
				stopWaitingPipeConnectionTokenSource.Cancel();
			}

			if (singleInstanceMutex != null)
			{
				singleInstanceMutex.Close();
				singleInstanceMutex = null;
			}

			if (pipeServer != null)
			{
				pipeServer.Close();
				pipeServer = null;
			}
		}


		/// <summary>
		/// Creates a remote service for communication.
		/// </summary>
		/// <param name="channelName">Application's channel name.</param>
		private static void CreateRemoteService(string channelName)
		{
			//Run Pipe listener
			Task.Run(async () =>
			{
				pipeServer = new NamedPipeServerStream(channelName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
				stopWaitingPipeConnectionTokenSource = new CancellationTokenSource();

				await pipeServer.WaitForConnectionAsync(stopWaitingPipeConnectionTokenSource.Token);

				if (stopWaitingPipeConnectionTokenSource.Token.IsCancellationRequested)
					return;

				try
				{
					using (var reader = new System.IO.StreamReader(pipeServer))
					{
						var stopRead = false;
						while (!stopRead)
						{
							var message = reader.ReadLine();

							if (message != null)
							{
								var args = JsonSerializer.Deserialize<string[]>(message);
								MessageReceived?.Invoke(args);
							}

							if (pipeServer.IsMessageComplete)
							{
								stopRead = true;
							}
						}
						reader.Close();
					}
				}
				catch (Exception ex)
				{
					Trace.TraceError(ex.Message);
				}
				finally
				{
					//Create new instance of NamedPipeServerStream for new client
					CreateRemoteService(channelName);
				}
			});
		}


		/// <summary>
		/// Creates a client channel and send message to the remoting service exposed by the server - 
		/// in this case, the remoting service exposed by the first instance. Calls a function of the remoting service 
		/// class to pass on command line arguments from the second instance to the first and cause it to activate itself.
		/// </summary>
		/// <param name="channelName">Application's channel name.</param>
		/// <param name="args">
		/// Command line arguments for the second instance, passed to the first instance to take appropriate action.
		/// </param>
		private static void SignalFirstInstance(string channelName, string[] args)
		{
			try
			{
				using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream(channelName))
				{
					namedPipeClient.Connect(1000);

					if (namedPipeClient.IsConnected)
					{
						using (var writer = new System.IO.StreamWriter(namedPipeClient))
						{
							writer.WriteLine(JsonSerializer.Serialize(args));
							writer.Flush();
							writer.Close();
						}
					}

					namedPipeClient.Close();
				}
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.Message);
			}
		}
	}
}
