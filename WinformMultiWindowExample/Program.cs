namespace WinformMultiWindowExample
{
	using ChartIQ.Finsemble;
	using Microsoft.IdentityModel.Tokens;
	using Microsoft.VisualBasic.ApplicationServices;
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Windows.Forms;
	using System.Diagnostics;

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

#if LOGGING && TRACE
			TextWriterTraceListener logger = new TextWriterTraceListener("Finsemble.log");
			logger.TraceOutputOptions = TraceOptions.DateTime;

			Trace.Listeners.Add(logger);
			Trace.AutoFlush = true;
			Trace.TraceInformation("Logging started");
#endif

			ApplicationContext app = new ApplicationContext(args);
			app.Run(args);
		}

		internal class ApplicationContext : WindowsFormsApplicationBase
		{
			private JsonWebKey JWK = new JsonWebKey()
			{
				D = "S7msrBKYM_VhXmAWTLhoRobLTevToYbX3xkbkN-EiaZ6Hg-xfozn5uAQGnBnoP1ldKOgoj5Z3dx6kTgR-3xfonEfdkk6wn0OVNbuFYyGkeeV4ts5JmyVpihFqE3RbkWuQ5D5xpIhXWl1fOWEuFfGCYIib2pmBUyc4Lz4OYmMOIGEC9nJg6ZuoKOh0nDZBjjO6vbYbXCEi0ys-FD7NAWsM8jTNDxLyXmpCNSVJOnGTX9CcxnFGdLVO8fqbooaydSHtFJE9YVqUKWp54hOBFMHdsTY5iT88urrvdBLxtGf6NGUVetpw-nFiOihDRPb9wMuLY9CT4DDzLecxadrLKh0PQ",
				DP = "dw8tnJQEpXazaYFcOhCvlU0Y4kGiul1W-_MTCXml8njCEx0Gp4s8jWf_QK7PcdzZRl-t_NTu12i2UGn8lCvOrSc4g66OkwZxPCVuvGXqQQ2DHTgFR2vk-Q53cFrMtjo8FvplkQuf92vS58ulq-iogDp7xxxXTAhmWaPA_d2i3C0",
				DQ = "EffO_SIA7qpBsSDHDKN3TdgybckYjN738roGKcU23ZXDDRy8h9X_lYtMSvjBQz7CRdia7F7aXGJLS08LSAfajRH-W8ssYfRge3twkrqxDXspWwIb77eMSINUUzRA1QQD7kSs_-LsU-rujsqZa-dnBxWhareFtVM-957lkzp4lTU",
				E = "AQAB",
				N = "zPOxYfLiAd3rM7KOLDBIeLl0kjQ7fk43mTTc1Nm9BDaSNVWqvOshSMCHmqOrKZX_WwA67Y6CQxWI5rZ6WNzoLHQU3PyqOvAdB7RgXCHlSeVYZ2haTcbWRjAXQ89H1WfnW96VwAbZn5nccxQGYlZIl3AMcwNRqV4hmiKtJVq4-2OzA-zs9Yg4Pfs6TbKR1XbNKz6EAPHDev0-7Xdnb6x1-qr4uL2Bq0ZwgfnyKQIRLc3zhD5kwqiJ5N7uZAZomLRkMFMXwy1UftD6fQUlFT2yoISn403RwN7YHEL8KoA9X7Jgs-dtlBh8c38QzQ1vbdMBzPuyRb07GMMKb6bdjdBV1w",
				P = "6RJ2697p-SS6CIJYdDU8AxgyKHx3kMkHnFzLQakoi1h6UUNxb-r8v1PUU5xZA5ijmvJv9Ag5uIOhT3u3vSOkADqGsqctNGtFf60hbjnkMDQnDNr2b-1_ayCn2pQM6mmisASfcnOJKUag4tLw3weK-t5uN9KYqfDBU7fVjqnq8HM",
				Q = "4R0RUlCnmZRzEw9sqjwxMuvNM1BTSubvMvG0VIlBkYbCn9MOdwurBPxrYqnUcbw-q-qzQy6st6a4L-EAZSnfD3FEEFKeINOJK06l0EwjcLeP8B4YQ-bxd9UroXpl9ACiMqHzyvJCNOpw8A22nbjKVnVhW1E17F-LFAJoWBetYA0",
				QI = "PODgpJrXxPAp72v_O0fNfAhWjHLeTk9TfLARl9lzPpYIoYR5tgP1Y_A-3feH_xtCfkzcCskfXIerQlY9lVmqs-eGEYjfuuPVYIruN4OsskMY1nz-h_14clyUmUwfCQJDV4qjcAzf80IMu53jYEW1BydRf90snRjk1dYgSq_qtTQ",
			};

			public ApplicationContext(string[] args)
			{
				var nonFSBLArgs = GetNonFinsembleArgs(args);
				string name = nonFSBLArgs.FirstOrDefault();
				Form form = CreateForm(name);

				if(form == null)
				{
					MessageBox.Show($"\"{name}\" unknown name of form!");
					return;
				}

				var fsbl = new Finsemble(args.ToArray(), form);
				fsbl.Connected += (s, e) =>
				{
					Trace.TraceInformation("FSBL connected");

					IIntegratable fsblForm = form as IIntegratable;
					if (fsblForm != null)
					{
						fsblForm.SetFinsemble(fsbl);
					}
				};

				// Dispose of Finsemble object when window is closed.
				form.Closed += (s, e) =>
				{
					Trace.TraceInformation("disposing window from app.xaml");
					fsbl.Dispose();
					Trace.TraceInformation("dispose completed");
				};
				fsbl.Connect("WinformMultiWindowExample", JWK);
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
					Trace.TraceInformation("FSBL connected");
					IIntegratable fsblForm = form as IIntegratable;
					if (fsblForm != null)
					{
						fsblForm.SetFinsemble(fsbl);
					}
				};

				// Dispose of Finsemble object when window is closed.
				form.Closed += (s, e) =>
				{
					Trace.TraceInformation("disposing window from app.xaml");
					fsbl.Dispose();
					Trace.TraceInformation("dispose completed");
				};
				fsbl.Connect("WinformMultiWindowExample", JWK);
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
