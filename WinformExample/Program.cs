using System;
using System.Diagnostics;
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
#if LOGGING && TRACE
			TextWriterTraceListener logger = new TextWriterTraceListener("Finsemble.log");
			logger.TraceOutputOptions = TraceOptions.DateTime;

			Trace.Listeners.Add(logger);
			Trace.AutoFlush = true;
			Trace.TraceInformation("Logging started");
#endif
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
			Trace.TraceInformation("Shutting down");
			Application.Exit();
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogUnhandledException(e.ExceptionObject as Exception);
			Trace.TraceInformation("Shutting down");
			Application.Exit();
		}

		static void LogUnhandledException(Exception e)
		{
			Trace.TraceError(e.Message);
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
