using ChartIQ.Finsemble;
using System.Diagnostics;
using System.Windows;

namespace FDC3WPFExample
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

            Finsemble.DispatcherUnhandledException(mainWindow, e);

            Debug.Print($"An Unhandled Exception has occurred. Exception: {e.Exception}");
            Shutdown();
        }
    }
}
