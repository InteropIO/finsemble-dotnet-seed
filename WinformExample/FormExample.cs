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

			this.input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(handleKeyPresses);

			//connect to Finsemble
			FSBL = new Finsemble(args, this);
			FSBL.Connected += FinsembleConnected;
			FSBL.Connect();

		}

		//If you encounter issues with the initial window placement (due to the handling of window borders in Windows 10) SetBoundsCore can be overridden to implement custom logic 
		//protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		//{
		//	base.SetBoundsCore(x, y, width, height, specified);
		//}

		private void FinsembleConnected(object sender, EventArgs e)
		{
			
			FSBL.RPC("Logger.log", new List<JToken> { "Winform example connected to Finsemble." });
			System.Diagnostics.Debug.WriteLine("FSBL Ready.");

			//setup linker channels
			FSBL.LinkerClient.GetAllChannels(handleLinkerChannelLabels);

			// Listen to Linker state change to render connected channels
			FSBL.LinkerClient.OnStateChange(handleLinkerStateChange);

			// Handle Window grouping
			FSBL.RouterClient.Subscribe("Finsemble.WorkspaceService.groupUpdate", handleWindowGrouping);

			//Example for handling component state in a workspace (and hand-off to getSpawnData if no state found)
			FSBL.WindowClient.GetComponentState(new JObject { ["field"] = "symbol" }, handleGetComponentState);


			// Example for Handling PubSub data
			FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", handlePubSub);

			// Example for handling Drag and Drop
			FSBL.DragAndDropClient.SetScrim(scrim);
			FSBL.DragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
			{
				new KeyValuePair<string, EventHandler<FinsembleEventArgs>>("symbol", handleDragAndDropReceive)
			});

			// Emitters for data that can be dragged using the drag icon.
			FSBL.DragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
			{
				new KeyValuePair<string, DragAndDropClient.emitter>("symbol", handleDragAndDropEmit)
			});

			// Example for LinkerClient subscribe
			FSBL.LinkerClient.Subscribe("symbol", handleLinkerData);

			// Example for getting Spawnable component list
			FSBL.ConfigClient.GetValue(new JObject { ["field"] = "finsemble.components" }, handleComponentsList);

			
		}

		private void handleLinkerChannelLabels(object sender, FinsembleEventArgs args)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (args.error == null)
				{
					Label[] groupLabels = new Label[] { group1, group2, group3, group4, group5, group6 };
					var allChannels = args.response as JArray;
					int labelcount = 0;
					foreach (JObject item in allChannels)
					{
						Label theLabel = groupLabels[labelcount++];
						theLabel.Visible = false;
						LinkerGroups.Add(item["name"].ToString(), theLabel);
						//limit channels to ones we enough labels for
						if (labelcount == groupLabels.Length)
						{
							break;
						}
					}
				}
				else
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when retrieving linker channels: ", args.error.ToString() });
				}
			});
		}

		private void handleLinkerStateChange(Object sender, FinsembleEventArgs response)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (response.error != null)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when receiving linker state change data: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					var channels = response.response["channels"] as JArray;
					var allChannels = response.response["allChannels"] as JArray;
					foreach (JObject obj in allChannels)
					{
						LinkerGroups[obj["name"].ToString()].Visible = false;
						LinkerGroups[obj["name"].ToString()].BackColor = ColorTranslator.FromHtml(obj["color"].ToString());
					}

					foreach (JValue channel in channels)
					{
						LinkerGroups[channel.Value.ToString()].Visible = true;
					}
				}
			});
		}

		private void handleWindowGrouping(object s, FinsembleEventArgs res)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (res.error != null)
				{
					return;
				}
				else
				{
					JObject groupData = res.response["data"]["groupData"] as JObject;
					String currentWindowName = FSBL.WindowClient.windowIdentifier["windowName"].ToString();
					JObject thisWindowGroups = new JObject();
					thisWindowGroups.Add("dockingGroup", "");
					thisWindowGroups.Add("snappingGroup", "");
					thisWindowGroups.Add("topRight", false);

					foreach (var obj in groupData)
					{
						string windowgroupid = obj.Key;
						JObject windowgroup = groupData[windowgroupid] as JObject;
						JArray windownames = windowgroup["windowNames"] as JArray;

						bool windowingroup = false;
						for (int i = 0; i < windownames.Count; i++)
						{
							string windowname = windownames[i].ToString();
							if (windowname == currentWindowName)
							{
								windowingroup = true;
							}
						}

						if (windowingroup)
						{
							bool isMovable = (bool)windowgroup["isMovable"];
							if (isMovable)
							{
								thisWindowGroups["dockingGroup"] = windowgroupid;
								if (windowgroup["topRightWindow"].ToString() == currentWindowName)
								{
									thisWindowGroups["topRight"] = true;
								}
							}
							else
							{
								thisWindowGroups["snappingGroup"] = windowgroupid;
							}
						}
					}

					if (thisWindowGroups["dockingGroup"].ToString() != "")
					{
						// docked
						groupCb.Checked = true;
					}
					else if (thisWindowGroups["snappingGroup"].ToString() != "")
					{
						// Snapped
						groupCb.Enabled = true;
					}
					else
					{
						// unsnapped/undocked
						groupCb.Checked = false;
						groupCb.Enabled = false;
					}
				}
			});
		}

		private void handleGetComponentState(object s, FinsembleEventArgs state)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (state.response != null)
				{
					// Example for restoring state
					if (state.response is JValue)
					{
						var symbol = (JValue)state.response;
						if (symbol != null)
						{
							var symbolTxt = symbol?.ToString();
							if (!string.IsNullOrEmpty(symbolTxt))
							{
								datavalue.Text = symbolTxt;
								input.Text = symbolTxt;
								datasource.Text = "via component state";
							}
						}
					} else
					{
						// Example for Handling Spawn data
						FSBL.WindowClient.getSpawnData(handleSpawnData);
					}
				}
				else
				{
					// Example for Handling Spawn data
					FSBL.WindowClient.getSpawnData(handleSpawnData);
				}
			});
		}

		private void handleSpawnData(Object sender, FinsembleEventArgs res) 
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				var receivedData = (JObject)res.response;
				if (res.error != null)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when retrieving spawn data: ", res.error.ToString() });
				}
				else if (res.response != null)
				{
					JToken value = receivedData.GetValue("spawndata");
					if (value != null)
					{
						datavalue.Text = value.ToString();
						input.Text = value.ToString();
						datasource.Text = "via Spawndata";
						saveState();
					}
				}
			});
		}

		private void handlePubSub(object sender, FinsembleEventArgs state)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				try
				{
					if (state.error != null)
					{
						FSBL.RPC("Logger.error", new List<JToken> { "Error when retrieving spawn data: ", state.error.ToString() });
					}
					else if (state.response != null)
					{
						var pubSubData = (JObject)state.response;
						var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
						if (theData != null)
						{
							datavalue.Text = theData;
							input.Text = theData;
							datasource.Text = "via PubSub";
							saveState();
						}
					}
				}
				catch (Exception ex)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when retrieving linker channels: ", ex.Message });
				}
			});
		}

		private void handleDragAndDropReceive(object sender, FinsembleEventArgs args) 
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (args.error != null)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when receiving drag and drop data: ", args.error.ToString() });
				}
				else if (args.response != null)
				{
					var data = args.response["data"]?["symbol"];
					if (data != null)
					{
						//Check if we received an object (so data.symbol.symbol) or a string (data.symbol) to support variations in the format
						if (data.HasValues)
						{
							data = data?["symbol"];
							datavalue.Text = data.ToString();
							input.Text = data.ToString();
							datasource.Text = "via Drag and Drop";
							saveState();
						}
					}
				}
			});
		}

		private JObject handleDragAndDropEmit() 
		{
			return new JObject
			{
				["symbol"] = datavalue.Text,
				["description"] = "Symbol " + datavalue.Text
			};
		}

		private void handleLinkerData(object sender, FinsembleEventArgs response) 
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (response.error != null)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when receiving linker data: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					string value = response.response?["data"]?.ToString();
					datavalue.Text = value;
					datasource.Text = "via Linker";
					input.Text = value;
					saveState();
				}
			});
		}

		private void handleComponentsList(Object sender, FinsembleEventArgs response)
		{
			Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
			{
				if (response.error != null)
				{
					FSBL.RPC("Logger.error", new List<JToken> { "Error when receiving spawnable component list: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					var components = (JObject)response.response?["data"];
					foreach (var property in components?.Properties())
					{
						object value = components?[property.Name]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
						if ((value != null) && bool.Parse(value.ToString()))
						{
							componentList.Items.Add(property.Name);
						}
					}
				}
			});
		}

		private void handleKeyPresses(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				Dispatcher.CurrentDispatcher.Invoke((System.Windows.Forms.MethodInvoker)delegate //main thread
				{
					datavalue.Text = input.Text;
					datasource.Text = "via Text input";
					saveState();
				});
				e.Handled = true;
			}
		}

		// Example for saving component state
		private void saveState()
		{
			FSBL.WindowClient.SetComponentState(new JObject
			{
				["field"] = "symbol",
				["value"] = datavalue.Text
			}, delegate (object s, FinsembleEventArgs e) { });
		}

		// Example for publishing to RouterClient
		private void pubsub_Click(object sender, EventArgs e)
		{
			FSBL.RouterClient.Publish("Finsemble.TestWPFPubSubSymbol", new JObject
			{
				["symbol"] = input.Text
			});
		}

		// Example for starting a drag
		private void datavalue_MouseDown(object sender, MouseEventArgs e)
		{
			FSBL.DragAndDropClient.DragStartWithData(sender);
		}

		private void linker_Click(object sender, EventArgs e)
		{
			FSBL.LinkerClient.ShowLinkerWindow(linker.Location.X, linker.Location.Y + linker.Height);
		}

		// Example for Publishing to LinkerClient
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

		private void groupCb_CheckedChanged(object sender, EventArgs e)
		{
			String currentWindowName = FSBL.WindowClient.windowIdentifier["windowName"].ToString();
			if (groupCb.Checked)
			{
				FSBL.RouterClient.Transmit("DockingService.formGroup", new JObject { ["windowName"] = currentWindowName });
			}
			else
			{
				FSBL.RouterClient.Query("DockingService.leaveGroup", new JObject { ["name"] = currentWindowName }, delegate (object s, FinsembleEventArgs res) {
					groupCb.Enabled = false;
				});
			}
		}
	}
}
