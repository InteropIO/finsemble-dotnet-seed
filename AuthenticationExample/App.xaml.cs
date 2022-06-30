using System;
using System.Diagnostics;
using System.Windows;

namespace AuthenticationExample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
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
				var mainWindow = new MainWindow(e.Args); // send command line arguments to main window.
			}
			catch (Exception ex)
			{
				Trace.TraceError(ex.ToString());
				Trace.TraceInformation("Shutting down");
				this.Shutdown();
			}
		}
	}
}
