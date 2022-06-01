using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
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

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

#if DEBUG
			Debugger.Launch();
			Debug.Print("OnStartup");
#endif

			string[] args = Environment.GetCommandLineArgs();
			Application.Run(new MainForm(args));
		}

		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogUnhandledException(e.Exception);
			Debug.Print($"An Unhandled Exception has occurred. Exception: {e.Exception}");
			Application.Exit();
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogUnhandledException(e.ExceptionObject as Exception);
			Application.Exit();
		}

		static void LogUnhandledException(Exception e)
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
