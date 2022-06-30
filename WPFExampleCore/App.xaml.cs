using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace WPFExampleCore
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private MainWindow mainWindow = null;

		protected override void OnStartup(StartupEventArgs e)
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

			try
			{
				mainWindow = new MainWindow(e.Args); // send command line arguments to main window.
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.ToString());
				Trace.TraceInformation("Shutting down");
				this.Shutdown();
			}
		}

		private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			LogUnhandledException(e.Exception);
			Trace.TraceError($"An Unhandled Exception has occurred. Exception: {e.Exception}");
			Trace.TraceInformation("Shutting down");
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
