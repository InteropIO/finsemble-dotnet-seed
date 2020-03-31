using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows;
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
				string folder = e1.response?["data"]?["selectedFolder"]?.ToString();
				if (string.IsNullOrEmpty(folder))
				{
					folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				}

				using (var folderBrowserDialog = new FolderBrowserDialog())
				{
					var folderName = string.Empty;
					folderBrowserDialog.Description = "Select the directory that you want to use";
					folderBrowserDialog.ShowNewFolderButton = true;
					folderBrowserDialog.SelectedPath = folder;

					Thread thread = new Thread(new ThreadStart(() =>
					{
						var parent = new Form() { StartPosition = FormStartPosition.CenterScreen, Opacity = 0, ShowInTaskbar = false };
					  parent.Show();
					  parent.Activate();
						if (folderBrowserDialog.ShowDialog(parent) == DialogResult.OK)
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
					// TODO: return error

				} else if (!Directory.Exists (folder))
				{
					// TODO: return error
				} else
				{
					string[] filePaths = Directory.GetFiles(folder);
					string[] folderPaths = Directory.GetDirectories(folder);

					var msg = new JArray();
					foreach (string folderPath in folderPaths)
					{
						var directoryInfo = new DirectoryInfo(folderPath);
						var item = new JObject()
						{
							["name"] = directoryInfo.Name,
							["modified"] = directoryInfo.LastWriteTime.ToString("MM/dd/yyyy h:mm tt"), ["type"] = "Folder"
						};
						msg.Add (item);
					}

					foreach (string filePath in filePaths)
					{
						var fileInfo = new FileInfo(filePath);
						var item = new JObject()
						{
							["name"] = fileInfo.Name,
							["modified"] = fileInfo.LastWriteTime.ToString("MM/dd/yyyy h:mm tt"),
							["type"] = fileInfo.Extension,
							["size"] = fileInfo.Length
						};
						msg.Add(item);
					}

					e1.sendQueryMessage(msg);
				}
			});

			FSBL.RouterClient.AddResponder("Windowless.WriteFile", (s1, e1) =>
			{
				string filename = e1.response?["data"]?["filename"]?.ToString();
				string contents = e1.response?["data"]?["contents"]?.ToString();

				JObject msg = new JObject();
				try
				{
					File.WriteAllText(filename, contents);
					msg["success"] = true;
				} catch (Exception ex)
				{
					// TODO: Send error
				}
				e1.sendQueryMessage(msg);
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
