using ChartIQ.Finsemble;
using System;
using System.Diagnostics;
using System.Windows;
using System.IO;

namespace WPFExample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private MainWindow mainWindow = null;

		protected override void OnStartup(StartupEventArgs e)
		{
			Debug.Print("OnStartup");

#if DEBUG
            Debugger.Launch();
#endif
			mainWindow = new MainWindow(e.Args); // send command line arguments to main window.
		}

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
#if DEBUG
            Debugger.Launch();
#endif
			LogUnhandledException(e.Exception);
			Finsemble.DispatcherUnhandledException(mainWindow, e);

			Debug.Print($"An Unhandled Exception has occurred. Exception: {e.Exception}");
			Shutdown();
		}

		private void LogUnhandledException(Exception e)
		{
			using (StreamWriter sw = new StreamWriter("Critical exceptions.log", true))
			{
				sw.WriteLine($"{DateTime.Now.ToUniversalTime()} - {e.Message}");
				sw.WriteLine(e.StackTrace);
				sw.WriteLine();
				sw.Close();
			}

			if (e.InnerException != null) LogUnhandledException(e.InnerException);
		}
	}
}
