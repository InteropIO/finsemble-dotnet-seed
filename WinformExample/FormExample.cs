using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WinformExample
{
	
	public partial class FormExample : Form
    {
		private SortedDictionary<string, Label> LinkerGroups = new SortedDictionary<string, Label>();

		public FormExample(String[] args)
        {
#if DEBUG
                System.Diagnostics.Debugger.Launch();
#endif
			InitializeComponent();

			FSBL = new Finsemble(args, this);
            FSBL.Connected += FinsembleConnected;
            FSBL.Connect();

			// Add lable to LinkerGroup
			LinkerGroups.Add("group1", group1);
			LinkerGroups.Add("group2", group2);
			LinkerGroups.Add("group3", group3);
			LinkerGroups.Add("group4", group4);
			LinkerGroups.Add("group5", group5);
			LinkerGroups.Add("group6", group6);
		}

		private void FinsembleConnected(object sender, EventArgs e)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{ 
				FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble." });
				System.Diagnostics.Debug.WriteLine("FSBL Ready.");

				// Example for  Handling Spawn data
				FSBL.WindowClient.getSpawnData((err, res) =>
				{
					var receivedData = (JObject)res.response;
					if (res.response != null) {
						JToken value = receivedData.GetValue("test");
						if (value != null) {
							datavalue.Text = value.ToString();
							datasource.Text = "via Spawndata";
						}
					}
				});

				// Example for Handling PubSub data
				FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
				{
					try
					{
						if (state.response != null)
						{
							var pubSubData = (JObject)state.response;
							var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
							if (theData != null)
							{
								datavalue.Text = theData;
								datasource.Text = "via PubSub";
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				});

				// Example for handling Drag and Drop
				FSBL.DragAndDropClient.SetScrim(scrim);
				FSBL.DragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
				{
					new KeyValuePair<string, EventHandler<FinsembleEventArgs>>("symbol", (s, args) =>
					{
						var data = args.response["data"]?["symbol"];
						if(data != null)
						{
						    //Check if we received an object (so data.symbol.symbol) or a string (data.symbol) to support variations in the format
						    if(data.HasValues) {
								data = data?["symbol"];
								datavalue.Text = data.ToString();
								datasource.Text = "via Drag and Drop";
							}
						}
					})
				});

				// Emitters for data that can be dragged using the drag icon.
				FSBL.DragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
				{
					new KeyValuePair<string, DragAndDropClient.emitter>("symbol", () =>
					{
						return new JObject
						{
							["symbol"] = input.Text,
							["description"] = "Symbol " + input.Text
						};
					})
				});

				// Example for LinkerClient
				FSBL.LinkerClient.Subscribe("symbol", (error, response) =>
				{
					datavalue.Text = response.response?["data"]?.ToString();
					datasource.Text = "via Linker";
				});

				// Listen to Liner state change
				FSBL.LinkerClient.OnStateChange((error, response) =>
				{
					var channels = response.response["channels"] as JArray;
					var allChannels = response.response["allChannels"] as JArray;
					foreach(JObject obj in allChannels)
					{
						LinkerGroups[obj["name"].ToString()].Visible = false;
						LinkerGroups[obj["name"].ToString()].BackColor = ColorTranslator.FromHtml(obj["color"].ToString());
					}

					foreach (JValue channel in channels)
					{
						LinkerGroups[channel.Value.ToString()].Visible = true;
					}
				});

				// Example for getting Spawnable component list
				FSBL.ConfigClient.GetValue(new JObject { ["field"] = "finsemble.components" }, (routerClient, response) =>
				{
					if (response.error != null)
					{
						return;
					}

					var components = (JObject)response.response?["data"];
					foreach (var property in components?.Properties())
					{
						object value = components?[property.Name]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
						if ((value != null) && bool.Parse(value.ToString()))
						{
							componentList.Items.Add(property.Name);
						}
					}
				});

			});
        }

		private void pubsub_Click(object sender, EventArgs e)
		{
			FSBL.RouterClient.Publish("Finsemble.TestWPFPubSubSymbol", new JObject
			{
				["symbol"] = input.Text
			});
		}

		private void input_MouseDown(object sender, MouseEventArgs e)
		{
			FSBL.DragAndDropClient.DragStartWithData(sender);
		}

		private void linker_Click(object sender, EventArgs e)
		{
			FSBL.LinkerClient.ShowLinkerWindow(linker.Location.X, linker.Location.Y + linker.Height);
		}

		private void pubLinker_Click(object sender, EventArgs e)
		{
			FSBL.LinkerClient.Publish(new JObject
			{
				["dataType"] = "symbol",
				["data"] = input.Text
			});
		}

		private void spawnBtn_Click(object sender, EventArgs e)
		{
			object selected = componentList.SelectedItem;			
			if (selected != null)
			{
				string componentName = selected.ToString();
				FSBL.LauncherClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
			}
		}

		//If you encounter issues with the initial window placement (due to the handling of window borders in Windows 10) SetBoundsCore can be overridden to implement custom logic 
		//protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		//{
		//	base.SetBoundsCore(x, y, width, height, specified);
		//}
	}
}
