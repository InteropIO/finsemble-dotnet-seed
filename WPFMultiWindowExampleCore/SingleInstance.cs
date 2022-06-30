using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WPFMultiWindowExampleCore
{
	/// <summary>
	/// This class checks to make sure that only one instance of 
	/// this application is running at a time.
	/// </summary>
	/// <remarks>
	/// Note: this class should be used with some caution, because it does no
	/// security checking. For example, if one instance of an app that uses this class
	/// is running as Administrator, any other instance, even if it is not
	/// running as Administrator, can activate it with command line arguments.
	/// For most apps, this will not be much of an issue.
	/// </remarks>
	public static class SingleInstance
	{
		#region Private Fields

		/// <summary>
		/// TokenSource for canceling waiting to pipe
		/// </summary>
		private static CancellationTokenSource stopWaitingPipeConnectionTokenSource;

		/// <summary>
		/// NamedPipeServerStream for communications.
		/// </summary>
		private static NamedPipeServerStream pipeServer;

		/// <summary>
		/// String delimiter used in channel names.
		/// </summary>
		private const string Delimiter = ":";

		/// <summary>
		/// Suffix to the channel name.
		/// </summary>
		private const string ChannelNameSuffix = "SingeInstanceChannel";

		/// <summary>
		/// Remote service name.
		/// </summary>
		private const string RemoteServiceName = "SingleInstanceApplicationService";

		/// <summary>
		/// Application mutex.
		/// </summary>
		private static Mutex singleInstanceMutex;

		internal delegate void MessageReciveHandler(string[] args);

		/// <summary>
		/// Message receive event from another app instances
		/// </summary>
		internal static event MessageReciveHandler MessageReceived;
		
		#endregion
		

		#region Public Methods

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

			string channelName = String.Concat(applicationIdentifier, Delimiter, ChannelNameSuffix);

			// Create mutex based on unique application Id to check if this is the first instance of the application. 
			bool firstInstance;
			singleInstanceMutex = new Mutex(true, applicationIdentifier, out firstInstance);
			if (firstInstance)
			{
				CreateRemoteService(channelName);
			}
			else
			{
				SignalFirstInstance(channelName, commandLineArgs);
			}

			return firstInstance;
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

		#endregion

		#region Private Methods
		
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
		/// Creates a client channel and obtains a reference to the remoting service exposed by the server - 
		/// in this case, the remoting service exposed by the first instance. Calls a function of the remoting service 
		/// class to pass on command line arguments from the second instance to the first and cause it to activate itself.
		/// </summary>
		/// <param name="channelName">Application's channel name.</param>
		/// <param name="args">
		/// Command line arguments for the second instance, passed to the first instance to take appropriate action.
		/// </param>
		private static void SignalFirstInstance(string channelName, IList<string> args)
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

		#endregion
	}
}
