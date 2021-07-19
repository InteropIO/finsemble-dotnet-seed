using Finsemble.WPF.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
            Debug.Print($"An Unhandled Exception has occurred. Exception: {e.Exception}");
        }
    }
}
