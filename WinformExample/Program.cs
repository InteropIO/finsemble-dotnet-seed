using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WinformExample
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		/// 
		[STAThread]
		static void Main()
		{
			string[] args = Environment.GetCommandLineArgs();

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new FormExample(args));
		}

		static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			LogUnhandledException(e.Exception);
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
