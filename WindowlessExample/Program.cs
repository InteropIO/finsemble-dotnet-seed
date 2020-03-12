using System;
using System.Collections.Generic;
using System.Threading;
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

        static void Main() // ignore the commandline arguments as we're going to manually generate them in this example
        {
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif

			string[] args = new string[] {
				"finsembleWindowName=WindowlessExample-non-finsemble-launch", //artificial window name - note as this should always be unique across the finsemble instance so only run one isntance or find a way to make it unique 
				"componentType=WindowlessExample", //ensure that the component type appears in your Finsemble configuration
				"uuid=dummy_uuid", //provide a dummy uuid
				"iac=true",
				"serverAddress=ws://127.0.0.1:3376",
				//the position parameters can be included if your window will be managed by Finsemble, omit otherwise
				//"left=100", 
				//"top=100",
				//"width=800",
				//"height=600"
			};

			// Initialize Finsemble
			FSBL = new Finsemble(args, null); //Note that second argument is null if the window is not managed by FInsemble (or there is no window) 
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