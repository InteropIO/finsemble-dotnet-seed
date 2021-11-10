using ChartIQ.Finsemble;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application, ISingleInstanceApp
	{
		private static readonly object lockObj = new object();

		private const string Unique = "6bea6fc4-5d9c-4961-b39d-89addcd65a73";

		private static App application = null;

		private static Finsemble FSBL = null;

		private static JsonWebKey JWK = new JsonWebKey()
		{
			D = "S7msrBKYM_VhXmAWTLhoRobLTevToYbX3xkbkN-EiaZ6Hg-xfozn5uAQGnBnoP1ldKOgoj5Z3dx6kTgR-3xfonEfdkk6wn0OVNbuFYyGkeeV4ts5JmyVpihFqE3RbkWuQ5D5xpIhXWl1fOWEuFfGCYIib2pmBUyc4Lz4OYmMOIGEC9nJg6ZuoKOh0nDZBjjO6vbYbXCEi0ys-FD7NAWsM8jTNDxLyXmpCNSVJOnGTX9CcxnFGdLVO8fqbooaydSHtFJE9YVqUKWp54hOBFMHdsTY5iT88urrvdBLxtGf6NGUVetpw-nFiOihDRPb9wMuLY9CT4DDzLecxadrLKh0PQ",
			DP = "dw8tnJQEpXazaYFcOhCvlU0Y4kGiul1W-_MTCXml8njCEx0Gp4s8jWf_QK7PcdzZRl-t_NTu12i2UGn8lCvOrSc4g66OkwZxPCVuvGXqQQ2DHTgFR2vk-Q53cFrMtjo8FvplkQuf92vS58ulq-iogDp7xxxXTAhmWaPA_d2i3C0",
			DQ = "EffO_SIA7qpBsSDHDKN3TdgybckYjN738roGKcU23ZXDDRy8h9X_lYtMSvjBQz7CRdia7F7aXGJLS08LSAfajRH-W8ssYfRge3twkrqxDXspWwIb77eMSINUUzRA1QQD7kSs_-LsU-rujsqZa-dnBxWhareFtVM-957lkzp4lTU",
			E = "AQAB",
			N = "zPOxYfLiAd3rM7KOLDBIeLl0kjQ7fk43mTTc1Nm9BDaSNVWqvOshSMCHmqOrKZX_WwA67Y6CQxWI5rZ6WNzoLHQU3PyqOvAdB7RgXCHlSeVYZ2haTcbWRjAXQ89H1WfnW96VwAbZn5nccxQGYlZIl3AMcwNRqV4hmiKtJVq4-2OzA-zs9Yg4Pfs6TbKR1XbNKz6EAPHDev0-7Xdnb6x1-qr4uL2Bq0ZwgfnyKQIRLc3zhD5kwqiJ5N7uZAZomLRkMFMXwy1UftD6fQUlFT2yoISn403RwN7YHEL8KoA9X7Jgs-dtlBh8c38QzQ1vbdMBzPuyRb07GMMKb6bdjdBV1w",
			P = "6RJ2697p-SS6CIJYdDU8AxgyKHx3kMkHnFzLQakoi1h6UUNxb-r8v1PUU5xZA5ijmvJv9Ag5uIOhT3u3vSOkADqGsqctNGtFf60hbjnkMDQnDNr2b-1_ayCn2pQM6mmisASfcnOJKUag4tLw3weK-t5uN9KYqfDBU7fVjqnq8HM",
			Q = "4R0RUlCnmZRzEw9sqjwxMuvNM1BTSubvMvG0VIlBkYbCn9MOdwurBPxrYqnUcbw-q-qzQy6st6a4L-EAZSnfD3FEEFKeINOJK06l0EwjcLeP8B4YQ-bxd9UroXpl9ACiMqHzyvJCNOpw8A22nbjKVnVhW1E17F-LFAJoWBetYA0",
			QI = "PODgpJrXxPAp72v_O0fNfAhWjHLeTk9TfLARl9lzPpYIoYR5tgP1Y_A-3feH_xtCfkzcCskfXIerQlY9lVmqs-eGEYjfuuPVYIruN4OsskMY1nz-h_14clyUmUwfCQJDV4qjcAzf80IMu53jYEW1BydRf90snRjk1dYgSq_qtTQ",
		};

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
					else
					{
						// Register with Finsemble as a windowless component so the application will close when Finsemble is closed. 
						//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
						var fsbl = new Finsemble(args.ToArray(), null);

						fsbl.Connect("MultiWindowExample", JWK);
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

			if (!args.Any())
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
				Debug.Print($"Could not create window: {name}");
			}
			else
			{
				// Register with Finsemble
				//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
				var fsbl = new Finsemble(args.ToArray(), window);
				fsbl.Connected += (s, e) =>
				{
					IIntegratable fsblWin = window as IIntegratable;
					if (fsblWin == null)
					{
						Debug.Print($"The window \"{name}\" is not a window that can be integrated into Finsemble.");
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

				fsbl.Connect("MultiWindowExample", JWK);
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