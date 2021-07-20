using System;
using System.Threading;
using ChartIQ.Finsemble;
using ChartIQ.Finsemble.Events;
using ChartIQ.Finsemble.Router;
using Newtonsoft.Json.Linq;

namespace WindowlessExample
{
	class Program
	{
		private static readonly System.Timers.Timer timer = new System.Timers.Timer();
		private static readonly AutoResetEvent autoEvent = new AutoResetEvent(false);
		private static readonly object lockObj = new object();

		private static Finsemble FSBL = null;

		static void Main(string[] args)
		{
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif

			// Initialize Finsemble
			//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			FSBL = new Finsemble(args, null);
			FSBL.Connected += OnConnected;
			FSBL.Disconnected += OnShutdown;
			FSBL.Connect();
			
			// Block main thread until worker is finished.
			autoEvent.WaitOne();
		}

		private static void OnConnected(object sender, EventArgs e)
		{
			FSBL.Logger.Log("Windowless example connected to Finsemble.");

			// Send log message every 5 seconds
			timer.Interval = 5 * 1000;
			timer.AutoReset = true;
			timer.Elapsed += (s1, e1) => FSBL.Logger.Log(string.Format("Windowless example elapsed event was raised at {0}", e1.SignalTime));
			timer.Start();

			//Search provider example
			FSBL.SearchClient.Register(
				"Windowless example", 
				(object o, FinsembleQueryArgs args) => { 
					FSBL.Logger.Log("Received query", args.response?["data"]?["text"]);
					JArray results = new JArray{
						new JObject {
							["name"] = "Example search result",
							["score"] = 100,
							["type"] = "example",
							["actions"] = new JArray { new JObject { ["name"] = "example action" } }
						}
					};
					args.sendQueryMessage(new FinsembleEventResponse(results, null));
				},
				(object o, FinsembleQueryArgs args) => {
					FSBL.Logger.Log("Search result action clicked on", args.response["item"], "action:", args.response["action"]);
					args.sendQueryMessage(new FinsembleEventResponse("Performed search result action", null));
				},
				(object o, FinsembleQueryArgs args) => { 
					FSBL.Logger.Log("Search provider title was click on");
					args.sendQueryMessage(new FinsembleEventResponse("Performed search provider action", null));
				},
				"Windowless example search provider", 
				(object o, FinsembleEventArgs args) =>
					{
						if (args.error != null)
						{
							FSBL.Logger.Error("Error returned when registering as a search provider", args.error);
						} else
                        {
							FSBL.Logger.Log("Registered search provider");
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
