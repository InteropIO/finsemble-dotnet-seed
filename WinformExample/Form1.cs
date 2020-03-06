using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformExample
{
    public partial class Form1 : Form
    {
        public Form1(String[] args)
        {
            InitializeComponent();

#if DEBUG
                System.Diagnostics.Debugger.Launch();
#endif
			
            FSBL = new Finsemble(args, this);
            FSBL.Connected += FinsembleConnected;
            FSBL.Connect();
        }


        private void FinsembleConnected(object sender, EventArgs e)
        {
            FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble." });

            System.Diagnostics.Debug.WriteLine("FSBL Ready.");
            FSBL.RouterClient.Query("Launcher.getActiveDescriptors", new JObject(), (err, res) =>
            {
                var receivedData = (JObject)res.response;
                String windowName = FSBL.WindowClient.windowIdentifier.GetValue("windowName").ToString();
                textBox1.Text = receivedData["data"]?[windowName]?["customData"]?["spawnData"]?.ToString();
            });

			FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
			{
				try
				{
					if (state.response != null)
					{
						var pubSubData = (JObject)state.response;
						// The initial publish will always be an empty object.
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
        }
	}
}
