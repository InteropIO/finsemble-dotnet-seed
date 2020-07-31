using System.Diagnostics;
using System.Reflection;
using System.Windows;
using log4net;
using ChartIQ.Finsemble;


namespace WPFExample
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
		/// <summary>
		/// The logger
		/// </summary>
		private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		private MainWindow mainWindow = null;

		protected override void OnStartup(StartupEventArgs e)
        {
			Logger.Debug("OnStartup");

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
			Finsemble.DispatcherUnhandledException(mainWindow, e);
			Logger.Error("An Unhandled Exception has occurred. Please Check your event Logs.", e.Exception);
			Shutdown();
		}
    }
}
