using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using ChartIQ.Finsemble;
using log4net;
using Microsoft.Shell;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, ISingleInstanceApp
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private static readonly object lockObj = new object();

		private const string Unique = "6bea6fc4-5d9c-4961-b39d-89addcd65a73";

		private static App application = null;

		private static Finsemble FSBL = null;

		/// <summary>
		///
		/// </summary>
		[STAThread]
		public static void Main(string[] args)
		{
#if DEBUG
            Debugger.Launch();
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
                if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
                {

                    application = new App();

                    // If window type passed for initial launch, add listener to launch window when connected.
                    var argsList = args.ToList();
                    IEnumerable<string> nonFSBLArgs = GetNonFinsembleArgs(argsList);
                    if ((nonFSBLArgs != null) && nonFSBLArgs.Any())
                    {
                        // Non-finsemble arguments passed, launch window
                        LaunchWindow(args.ToList());
                    }

                    application.InitializeComponent();
                    mutex.ReleaseMutex();
                    application.Run();

                    // Allow single instance code to perform cleanup operations
                    SingleInstance<App>.Cleanup();
                }

            }

		}

		/// <summary>
		/// Giving a list of arguments, returns an enumerable of non-Finsemble arguments (no =)
		/// </summary>
		/// <param name="args">List of arguments</param>
		/// <returns>Enumerable of non-Finsemble arguments</returns>
		private static IEnumerable<string> GetNonFinsembleArgs(IList<string> args)
		{
			var nonFSBLArgs = args.Where(str => !str.Contains("=") && !str.Contains(".exe"));

			return nonFSBLArgs;
		}

		/// <summary>
		/// Launches a Finsemble aware window with the passed arguments.
		/// </summary>
		/// <param name="args">The arguments passed to the process.</param>
		/// <returns>Always true?</returns>
		private static bool LaunchWindow(IList<string> args)
		{
#if DEBUG
			Debugger.Launch();
#endif

			if (args.Count < 2)
			{
				// Invalid number of arguments
				return true;
			}

			var nonFSBLArgs = GetNonFinsembleArgs(args);
			if ((nonFSBLArgs == null) || !nonFSBLArgs.Any())
			{
				// no non-finsemble arguments. Cannot launch window.
				return true;
			}

			string name = nonFSBLArgs.First();

			// handle command line arguments of second instance
			Window window = null;
			Current.Dispatcher.Invoke(() =>
			{
				window = CreateWindow(name);
			});

			if (window == null)
			{
				Logger.Error($"Could not create window: {name}");
			}
			else
			{
				// Register with Finsemble
				var fsbl = new Finsemble(args.ToArray(), window);
				fsbl.Connected += (s, e) =>
				{
					IIntegratable fsblWin = window as IIntegratable;
					if (fsblWin == null)
					{
						Logger.Warn($"The window \"{name}\" is not a window that can be integrated into Finsemble.");
					}
					else
					{
						fsblWin.SetFinsemble(fsbl);
					}

					Current.Dispatcher.Invoke(window.Show);
				};

				// Dispose of Finsemble object when window is closed.
				window.Closed += (s, e) =>
				{
					Debug.WriteLine("disposing window from app.xaml");
					fsbl.Dispose();
					Debug.WriteLine("dispose completed");
				};

				fsbl.Connect();
			}

			return true;
		}

		private static void OnShutdown(object sender, EventArgs e)
		{
			if (FSBL != null)
			{
				lock (lockObj)
				{
					if (FSBL != null)
					{
						try
						{
							// Dispose of Finsemble.
							FSBL.Dispose();
						}
						catch { }
						finally
						{
							FSBL = null;
						}
					}

				}
			}

			try
			{
				// Release main thread so application can exit.
				Current.Dispatcher.Invoke(application.Shutdown);
			}
			catch
			{
				// An error occurred, but I don't care as long as it exits.
				;
			}
		}

		/// <summary>
		/// Create a window based on the name passed.
		/// </summary>
		/// <param name="name">The name passed in</param>
		/// <returns>The window created.</returns>
		private static Window CreateWindow(string name)
		{
			Window window = null;
			switch (name)
			{
				case "Window1":
					{
						window = new Window1();
						break;
					}
				case "Window2":
					{
						window = new Window2();
						break;
					}
				case "Window3":
					{
						window = new Window3();
						break;
					}
				case "Window4":
					{
						window = new Window4();
						break;
					}
				default:
					{
						// Unknown window, ignore
						break;
					}
			}

			return window;
		}

		#region ISingleInstanceApp Members
		/// <summary>
		///
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public bool SignalExternalCommandLineArgs(IList<string> args)
		{
			return LaunchWindow(args);
		}
		#endregion

		/// <summary>
		///
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
#if DEBUG
			Debugger.Launch();
#endif

			MessageBox.Show("An Unhandled Exception has occurred. Please Check your event Logs.");
		}
	}
}