using System;
using System.Threading;
using ChartIQ.Finsemble;
using ChartIQ.Finsemble.Events;
using ChartIQ.Finsemble.Router;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WindowlessExample
{
	class Program
	{
		private static readonly System.Timers.Timer timer = new System.Timers.Timer();
		private static readonly AutoResetEvent autoEvent = new AutoResetEvent(false);
		private static readonly object lockObj = new object();

		private static Finsemble FSBL = null;
		private static JsonWebKey JWK = new JsonWebKey()
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
			FSBL.Connect("WindowlessExample", JWK);
			
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
