using Finsemble.Winform.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Pipes;

namespace WinformMultiWindowExampleCore
{
	static class Program
	{
		private static readonly string Unique = "0f03c714-b597-4c17-a351-62f35535599a";

		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
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

			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			Application.ThreadException += Application_ThreadException;
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

			// create a global mutex
			using (var mutex = new Mutex(false, "Finsemble"))
			{
				var mutexAcquired = false;
				try
				{
					// acquire the mutex (or timeout after 60 seconds)
					// will return false if it timed out
					mutexAcquired = mutex.WaitOne(60000);
				}
				catch (AbandonedMutexException)
				{
					// abandoned mutexes are still acquired, we just need
					// to handle the exception and treat it as acquisition
					mutexAcquired = true;
				}

				// if it wasn't acquired, it timed out, so can handle that how ever we want
				if (!mutexAcquired)
				{
					Trace.TraceError("I have timed out acquiring the mutex and can handle that somehow");
					return;
				}

				// otherwise, we've acquired the mutex and should do what we need to do,
				// then ensure that we always release the mutex
				if (SingleInstance.InitializeAsFirstInstance(Unique))
				{
					SingleInstance.MessageReceived += SingleInstance_MessageReceived;
					mutex.ReleaseMutex();

					var form = CreateForm(Environment.GetCommandLineArgs(), out string formName);
					if (form != null)
					{
						Application.Run(form);
					}
					else
					{
						MessageBox.Show($"\"{formName}\" unknown name of form!");
					}


					// Allow single instance code to perform cleanup operations
					SingleInstance.Cleanup();
				}
			}
		}

		/// <summary>
		/// Occurs when subsequent instances are started.
		/// </summary>
		/// <param name="args"></param>
		private static void SingleInstance_MessageReceived(string[] args)
		{
			if (Application.OpenForms.Count > 0)
			{
				//create and show new form
				Application.OpenForms[0].Invoke(new Action(() =>
				{
					var form = CreateForm(args, out string formName);
					form?.Show();
				}));
			}
		}

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			Trace.TraceError($"An Unhandled Exception has occurred. Exception: {e.Exception}");
			Trace.TraceInformation("Shutting down");
			Application.Exit();
		}

		/// <summary>
		/// The GetNonFinsembleArgs.
		/// </summary>
		/// <param name="args">The args <see cref="IList{string}"/>.</param>
		/// <returns>The <see cref="IEnumerable{string}"/>.</returns>
		private static IEnumerable<string> GetNonFinsembleArgs(IList<string> args)
		{
			var nonFSBLArgs = args.Where(str => !str.Contains("=") && !str.Contains(".exe") && !str.Contains(".dll"));
			return nonFSBLArgs;
		}

		/// <summary>
		/// The CreateForm.
		/// </summary>
		/// <param name="args">The args <see cref="string[]"/>.</param>
		/// <returns>The <see cref="Form"/>.</returns>
		private static Form CreateForm(string[] args, out string passedFormName)
		{
			Form form = null;
			passedFormName = string.Empty;

			if (args.Length < 2)
			{
				// Invalid number of arguments
				return form;
			}

			var nonFSBLArgs = GetNonFinsembleArgs(args);
			passedFormName = nonFSBLArgs.FirstOrDefault();

			switch (passedFormName)
			{
				case "Form1":
					{
						form = new Form1(args);
						break;
					}
				case "Form2":
					{
						form = new Form2(args);
						break;
					}
				case "Form3":
					{
						form = new Form3(args);
						break;
					}
				case "Form4":
					{
						form = new Form4(args);
						break;
					}
				default:
					{
						// Unknown window, ignore
						break;
					}
			}

			return form;
		}
	}
}
