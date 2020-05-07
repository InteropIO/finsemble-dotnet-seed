using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WinformExample
{
	
	public partial class FormExample : Form
    {
		public FormExample(String[] args)
        {
#if DEBUG
                System.Diagnostics.Debugger.Launch();
#endif
			InitializeComponent();

			FSBL = new Finsemble(args, this);
            FSBL.Connected += FinsembleConnected;
            FSBL.Connect();
        }

		private void FinsembleConnected(object sender, EventArgs e)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{ 
				FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble." });

				System.Diagnostics.Debug.WriteLine("FSBL Ready.");
				FSBL.WindowClient.getSpawnData((err, res) =>
				{
					var receivedData = (JObject)res.response;
					textBox1.Text = receivedData.ToString();
				});

				FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
				{
					try
					{
						if (state.response != null)
						{
							var pubSubData = (JObject)state.response;
							// The initial publish may be an empty object if not data has been published yet.
							// Therefore, we need these null operators to handle that case.
							var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
							if (theData != null)
							{
								textBox2.Text = theData;
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				});
			});
        }

		//If you encounter issues with the initial window placement (due to the handling of window borders in Windows 10) SetBoundsCore can be overridden to implement custom logic 
		//protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		//{
		//	base.SetBoundsCore(x, y, width, height, specified);
		//}
	}
}
