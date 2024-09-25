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
			
			// show form after connection to Finsemble prevents flickering on startup
			var form = new FormExample(args);
			form.Hide();

			form.FormClosed += (s, e) =>
			{
				Application.Exit();
			};

			form.FSBL.Connected += (s,e) =>
			{
				form.Invoke(new Action(() =>
				{
					form.Show();
				}));
			};
			Application.Run();
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
