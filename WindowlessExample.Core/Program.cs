using System;
using System.Threading;
using Finsemble.Core;
using Finsemble.Core.Clients.Router;
using Newtonsoft.Json.Linq;

namespace WindowlessExample.Core
{
	class Program
	{
		private static readonly System.Timers.Timer timer = new System.Timers.Timer();
		private static readonly AutoResetEvent autoEvent = new AutoResetEvent(false);
		private static readonly object lockObj = new object();

		private static Finsemble.Core.Finsemble FSBL = null;

		static void Main(string[] args)
		{
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif

			FSBL = new Finsemble.Core.Finsemble(args, null);
			FSBL.Connected += OnConnected;
			FSBL.Disconnected += OnShutdown;
			FSBL.Connect();

			// Block main thread until worker is finished.
			autoEvent.WaitOne();
		}

		private static void OnConnected(object sender, EventArgs e)
		{
			FSBL.Clients.Logger.Log("Windowless example connected to Finsemble.");

			// Send log message every 5 seconds
			timer.Interval = 5 * 1000;
			timer.AutoReset = true;
			timer.Elapsed += (s1, e1) => FSBL.Clients.Logger.Log(string.Format("Windowless example elapsed event was raised at {0}", e1.SignalTime));
			timer.Start();

			//Search provider example
			FSBL.Clients.SearchClient.Register(
				"Windowless example",
				(o, args) => {
					FSBL.Clients.Logger.Log("Received query", args.response?["data"]?["text"]);
					JArray results = new JArray{
						new JObject {
							["name"] = "Example search result",
							["score"] = 100,
							["type"] = "example",
							["actions"] = new JArray { new JObject { ["name"] = "example action" } }
						}
					};
				},
				(o, args) => {
					FSBL.Clients.Logger.Log("Search result action clicked on", args.response["item"], "action:", args.response["action"]);
				},
				(o, args) => {
					FSBL.Clients.Logger.Log("Search provider title was click on");
				},
				"Windowless example search provider",
				(o, args) =>
				{
					if (args.error != null)
					{
						FSBL.Clients.Logger.Error("Error returned when registering as a search provider", args.error);
					}
					else
					{
						FSBL.Clients.Logger.Log("Registered search provider");
					}
				}
			);
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
