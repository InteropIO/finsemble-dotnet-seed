namespace WinformMultiWindowExample
{
	using ChartIQ.Finsemble;
	using Microsoft.VisualBasic.ApplicationServices;
	using MultiWiformExample;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Windows.Forms;

	internal static class Program
	{
		/// <summary>
		/// The Main.
		/// </summary>
		/// <param name="args">The args<see cref="string[]"/>.</param>
		[STAThread]
		internal static void Main(string[] args)
		{
#if DEBUG
			Debugger.Launch();
#endif

			ApplicationContext app = new ApplicationContext(args);
			app.Run(args);
		}

		internal class ApplicationContext : WindowsFormsApplicationBase
		{
			public ApplicationContext(string[] args)
			{
				var nonFSBLArgs = GetNonFinsembleArgs(args);
				string name = nonFSBLArgs.First();
				Form form = CreateForm(name);
				var fsbl = new Finsemble(args.ToArray(), form);
				fsbl.Connected += (s, e) =>
				{
					Debug.WriteLine("FSBL connected");

					IIntegratable fsblForm = form as IIntegratable;
					if (fsblForm != null)
					{
						fsblForm.SetFinsemble(fsbl);
					}
				};

				// Dispose of Finsemble object when window is closed.
				form.Closed += (s, e) =>
				{
					Debug.WriteLine("disposing window from app.xaml");
					fsbl.Dispose();
					Debug.WriteLine("dispose completed");
				};
				fsbl.Connect();
				this.MainForm = form;
				this.IsSingleInstance = true;
			}

			/// <summary>
			/// The OnStartupNextInstance.
			/// </summary>
			/// <param name="eventArgs">The eventArgs<see cref="StartupNextInstanceEventArgs"/>.</param>
			protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
			{
				var nonFSBLArgs = GetNonFinsembleArgs(eventArgs.CommandLine);
				string name = nonFSBLArgs.First();
				Form form = CreateForm(name);
				var fsbl = new Finsemble(eventArgs.CommandLine.ToArray(), form);
				fsbl.Connected += (s, e) =>
				{
					Debug.WriteLine("FSBL connected");
					IIntegratable fsblForm = form as IIntegratable;
					if (fsblForm != null)
					{
						fsblForm.SetFinsemble(fsbl);
					}
				};

				// Dispose of Finsemble object when window is closed.
				form.Closed += (s, e) =>
				{
					Debug.WriteLine("disposing window from app.xaml");
					fsbl.Dispose();
					Debug.WriteLine("dispose completed");
				};
				fsbl.Connect();
				form.Show();
			}

			/// <summary>
			/// The GetNonFinsembleArgs.
			/// </summary>
			/// <param name="args">The args<see cref="IList{string}"/>.</param>
			/// <returns>The <see cref="IEnumerable{string}"/>.</returns>
			private static IEnumerable<string> GetNonFinsembleArgs(IList<string> args)
			{
				var nonFSBLArgs = args.Where(str => !str.Contains("=") && !str.Contains(".exe"));
				return nonFSBLArgs;
			}

			/// <summary>
			/// The CreateForm.
			/// </summary>
			/// <param name="name">The name<see cref="string"/>.</param>
			/// <returns>The <see cref="Form"/>.</returns>
			private static Form CreateForm(string name)
			{
				Form form = null;
				switch (name)
				{
					case "Form1":
						{
							form = new Form1();
							break;
						}
					case "Form2":
						{
							form = new Form2();
							break;
						}
					case "Form3":
						{
							form = new Form3();
							break;
						}
					case "Form4":
						{
							form = new Form4();
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
}
