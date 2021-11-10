using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformExampleCore
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += Application_ThreadException;

#if DEBUG
			Debugger.Launch();
			Debug.Print("OnStartup");
#endif

			string[] args = Environment.GetCommandLineArgs();
			Application.Run(new MainForm(args));
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Debug.Print($"An Unhandled Exception has occurred. Exception: {e.Exception}");
			Application.Exit();
		}
	}
}
