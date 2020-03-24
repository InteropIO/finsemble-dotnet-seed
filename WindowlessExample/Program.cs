using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;

namespace ConsoleAppExample
{
  class Program
  {
		private static readonly System.Timers.Timer timer = new System.Timers.Timer();
		private static readonly AutoResetEvent autoEvent = new AutoResetEvent(false);
		private static readonly object lockObj = new object();

		private static Finsemble FSBL = null;

		[STAThread]
		static void Main(string[] args)
		{
	#if DEBUG
			System.Diagnostics.Debugger.Launch();
	#endif

			// Initialize Finsemble
			FSBL = new Finsemble(args, null);
			FSBL.Connected += OnConnected;
			FSBL.Disconnected += OnShutdown;
			FSBL.Connect();

			// Block main thread until worker is finished.
			autoEvent.WaitOne();
		}

		private static void OnConnected(object sender, EventArgs e)
		{
			FSBL.RPC("Logger.log", new List<JToken> { "Windowless example connected to Finsemble." });

			// Send log message every 5 seconds
			timer.Interval = 5 * 1000;
			timer.AutoReset = true;
			timer.Elapsed += (s1, e1) => FSBL.RPC("Logger.log", new List<JToken> { string.Format("Windowless example elapsed event was raised at {0}", e1.SignalTime) });
			timer.Start();

			FSBL.RouterClient.AddResponder("Windowless.SelectDirectory", (s1, e1) =>
			{
			using (var folderBrowserDialog = new FolderBrowserDialog())
			{
				var folderName = string.Empty;
				folderBrowserDialog.Description = "Select the directory that you want to use";
				folderBrowserDialog.ShowNewFolderButton = true;
				folderBrowserDialog.RootFolder = Environment.SpecialFolder.Personal;

				Thread thread = new Thread(new ThreadStart(() =>
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						folderName = folderBrowserDialog.SelectedPath;
					}

					var msg = new JObject()
					{
						["selectedPath"] = folderName
					};

					// Return folder name is query
					e1.sendQueryMessage(msg);
				}));
				thread.SetApartmentState(ApartmentState.STA);
				thread.Start();
			}

			});
			FSBL.RouterClient.AddResponder("Windowless.DirectoryList", (s1, e1) =>
			{
				string folder = e1.response?["data"]?["folder"]?.ToString();

				// if folder is not null, list the folder contents, if the folder is null, return an error message
				if (string.IsNullOrEmpty(folder))
				{
					// return error

				}
				else
				{
					string[] filePaths = Directory.GetFiles(folder);
					IList<string> filenames = new List<string>();
					foreach (string filePath in filePaths)
					{
						filenames.Add(Path.GetFileName(filePath));
					}

					var msg = new JArray(filenames);
					e1.sendQueryMessage(msg);
				}
			});

			FSBL.RouterClient.AddResponder("Windowless.WriteFile", (s1, e1) =>
			{
				string filename = e1.response?["data"]?["filename"]?.ToString();
				string contents = e1.response?["data"]?["contents"]?.ToString();
				File.WriteAllText(filename, contents);
			});
		}

		private static void OnShutdown(object sender, EventArgs e)
		{
			if (FSBL != null)
			{
				lock (lockObj)
				{
					// Disable log timer
					timer.Stop();

					if (FSBL != null)
					{
						try
						{
							// Dispose of Finsemble.
							FSBL.Dispose();
						}
						catch { }
						finally
						{
							FSBL = null;
						}
					}

				}
			}

			// Release main thread so application can exit.
			autoEvent.Set();
		}
  }
}
