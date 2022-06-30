using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WPFMultiWindowExampleCore
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public class AppStartup : Application
	{
		private static readonly object lockObj = new object();

		private const string Unique = "97511d7d-0dbc-4e4f-b18c-972f6d871ce0";

		private static AppStartup application = null;

		/// <summary>
		/// Application entry point
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
#if DEBUG
			Debugger.Launch();
#endif

#if LOGGING && TRACE
			TextWriterTraceListener logger = new TextWriterTraceListener("Finsemble.log");
			logger.TraceOutputOptions = TraceOptions.DateTime;

			Trace.Listeners.Add(logger);
			Trace.AutoFlush = true;
			Trace.TraceInformation("Logging started");
#endif

			// create a global mutex
			using (var mutex = new Mutex(false, "Finsemble"))
			{
				var mutexAcquired = false;
				try
				{
					// acquire the mutex (or timeout after 60 seconds)
					// will return false if it timed out
					mutexAcquired = mutex.WaitOne(60000);
				}
				catch (AbandonedMutexException)
				{
					// abandoned mutexes are still acquired, we just need
					// to handle the exception and treat it as acquisition
					mutexAcquired = true;
				}

				// if it wasn't acquired, it timed out, so can handle that how ever we want
				if (!mutexAcquired)
				{
					Console.WriteLine("I have timed out acquiring the mutex and can handle that somehow");
					return;
				}

				// otherwise, we've acquired the mutex and should do what we need to do,
				// then ensure that we always release the mutex
				if (SingleInstance.InitializeAsFirstInstance(Unique))
				{
					application = new AppStartup();
					application.DispatcherUnhandledException += Application_DispatcherUnhandledException;

					SingleInstance.MessageReceived += SingleInstance_MessageReceived;

					Application.Current.Dispatcher.Invoke(new Action(() =>
					{
						var window = CreateWindow(args);
						mutex.ReleaseMutex();
						if (window != null)
						{
							//Shutdown the application when the main window closed
							window.Closed += MainWindow_Closed;
							application.Run(window);
						}
					}));

					// Allow single instance code to perform cleanup operations
					SingleInstance.Cleanup();
				}
			}
		}

		/// <summary>
		/// Shutdown the application when the main window closed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void MainWindow_Closed(object sender, EventArgs e)
		{
			application.Shutdown();
		}

		/// <summary>
		/// Occurs when subsequent instances are started.
		/// </summary>
		/// <param name="args"></param>
		private static void SingleInstance_MessageReceived(string[] args)
		{
			Application.Current.Dispatcher.Invoke(new Action(() =>
			{
				//create and show new window
				var window = CreateWindow(args);
			}));
		}

		/// <summary>
		/// Create a window based on the name passed.
		/// </summary>
		/// <param name="args">The command line args</param>
		/// <returns>The window created.</returns>
		private static Window CreateWindow(string[] args)
		{
			Window window = null;

			var nonFSBLArgs = GetNonFinsembleArgs(args);
			string name = nonFSBLArgs.FirstOrDefault();

			switch (name)
			{
				case "Window1":
					{
						window = new Window1(args);
						break;
					}
				case "Window2":
					{
						window = new Window2(args);
						break;
					}
				case "Window3":
					{
						window = new Window3(args);
						break;
					}
				case "Window4":
					{
						window = new Window4(args);
						break;
					}
				default:
					{
						// Unknown window, ignore
						Trace.TraceError($"Could not create window: {name}");
						break;
					}
			}

			return window;
		}

		/// <summary>
		/// Giving a list of arguments, returns an enumerable of non-Finsemble arguments (no =)
		/// </summary>
		/// <param name="args">List of arguments</param>
		/// <returns>Enumerable of non-Finsemble arguments</returns>
		private static IEnumerable<string> GetNonFinsembleArgs(IList<string> args)
		{
			var nonFSBLArgs = args.Where(str => !str.Contains("=") && !str.Contains(".exe") && !str.Contains(".dll"));
			return nonFSBLArgs;
		}

		/// <summary>
		///
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private static void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			Trace.TraceError($"An Unhandled Exception has occurred. {e.Exception.Message}");
			MessageBox.Show("An Unhandled Exception has occurred. Please Check your event Logs.");
		}
	}
}
