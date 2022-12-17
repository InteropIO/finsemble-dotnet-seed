using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using ChartIQ.Finsemble.DragAndDrop;
using ChartIQ.Finsemble.Router;
using WinformExample.Controls;
using Color = System.Drawing.Color;
using ChartIQ.Finsemble.FDC3.Types;
using Microsoft.IdentityModel.Tokens;
using ChartIQ.Finsemble.FDC3.Interfaces;

namespace WinformExample
{
	public partial class FormExample : Form
	{
		private SortedDictionary<string, RoundedButton> LinkerGroups = new SortedDictionary<string, RoundedButton>();
		const int LinkerPillWidth = 22;
		const int LinkerPillHeight = 24;

		private System.Drawing.Text.PrivateFontCollection finfont = new System.Drawing.Text.PrivateFontCollection();
		private JsonWebKey JWK = new JsonWebKey()
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

		public FormExample(String[] args)
		{
#if DEBUG
			System.Diagnostics.Debugger.Launch();
#endif
			InitializeComponent();

			//this.input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(handleKeyPresses);

			//connect to Finsemble
			//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			FSBL = new Finsemble(args, this);

			// Use handle
			// FSBL = new Finsemble(args, this.Handle);
			//----

			ArrangeComponents();

			FSBL.Connected += FinsembleConnected;
			FSBL.Connect("WinformExample", JWK);
		}

		private void ArrangeComponents()
		{
			Activate();
			MessagesRichBox.SendToBack();
		}

		//If you encounter issues with the initial window placement (due to the handling of window borders in Windows 10) SetBoundsCore can be overridden to implement custom logic 
		//protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		//{
		//	base.SetBoundsCore(x, y, width, height, specified);
		//}

		private async void FinsembleConnected(object sender, EventArgs e)
		{
			FSBL.Logger.OnLog += Logger_OnLog;

			FSBL.Logger.Log(new JToken[] { "Winform example connected to Finsemble." });
			System.Diagnostics.Debug.WriteLine("FSBL Ready.");

			// Handle Window grouping
			FSBL.RouterClient.Subscribe("Finsemble.WorkspaceService.groupUpdate", HandleWindowGrouping);

			if (FSBL.LinkerClient != null)
			{
				// Listen to Linker state change to render connected channels
				FSBL.LinkerClient.OnStateChange(HandleLinkerStateChange);

				// Example for LinkerClient subscribe
				FSBL.LinkerClient.Subscribe("symbol", HandleLinkerData);
			}
			else //use default FDC3 client
			{
				FSBL.FDC3Client.StateChanged += HandleLinkerStateChange;

				//setting initial state of linked channels
				HandleLinkerStateChange(null, FSBL.FDC3Client.LastStateChangedArgs);

				// Example for Fdc3Client subscribe to specific context. The "*" for subscription to all contexts.
				var listener = FSBL.FDC3Client.DesktopAgentClient.AddContextListener("fdc3.instrument", HandleContext);
			}

			//Example for handling component state in a workspace (and hand-off to getSpawnData if no state found)
			FSBL.WindowClient.GetComponentState(new JObject { ["field"] = "symbol" }, HandleGetComponentState);


			// Example for Handling PubSub data
			FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", HandlePubSub);

			finfont.AddFontFile(@"Resources\finfont.ttf");
			finfont.AddFontFile(@"Resources\font-finance.ttf");
			var finFont = new System.Drawing.Font(finfont.Families[0], 100);
			var fontFinance = new System.Drawing.Font(finfont.Families[1], 7);
			Invoke(new Action(() =>
			{
				scrim.Font = finFont;

				LinkerButton.Font = fontFinance;
				DragNDropEmittingButton.Font = fontFinance;
				AlwaysOnTopButton.Font = fontFinance;
				DockingButton.Font = fontFinance;
				DockingButton.UseEllipse = true;
				AlwaysOnTopButton.UseEllipse = true;
			}));

			FSBL.DragAndDropClient.SetScrim(scrim);
			FSBL.DragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
			{
				new KeyValuePair<string, EventHandler<FinsembleEventArgs>>("symbol", HandleDragAndDropReceive)
			});

			// Emitters for data that can be dragged using the drag icon.
			FSBL.DragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
			{
				new KeyValuePair<string, DragAndDropClient.emitter>("symbol", HandleDragAndDropEmit)
			});


			// Example for getting Spawnable component list
			FSBL.ConfigClient.GetValue(new JObject { ["field"] = "finsemble.components" }, HandleComponentsList);

			SetInitialWindowGrouping(FSBL.WindowClient.GetWindowGroups());
			SetInitialAlwaysOnTop();
		}

		private async void SetInitialAlwaysOnTop()
		{
			var isAlwaysOnTop = (await FSBL.FinsembleWindow.IsAlwaysOnTop(new JObject()))?["data"]?.ToObject<bool>() == true;
			UpdateAlwaysOnTopButton(isAlwaysOnTop);
		}

		private void HandleContext(Context context, IContextMetadata metadata)
		{

			FSBL.Logger.Debug($"Context received: {context.Value}");

			if (context == null)
			{
				FSBL.Logger.Error(new JToken[] { "Error when receiving context. Context is null " });
			}
			else
			{
				this.Invoke(new Action(() =>
				{
					string value = context.Id?["ticker"]?.ToString();
					SourceLabel.Text = "via FDC3";
					DataLabel.Text = value;
					DataToSendInput.Text = value;
				}));
			}
		}

		private void Logger_OnLog(object sender, JObject log)
		{
			try
			{
				this.Invoke((MethodInvoker)delegate { MessagesRichBox.Text += log + "\n"; });
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}
		}

		private void HandleLinkerStateChange(Object sender, FinsembleEventArgs response)
		{
			this.Invoke(new Action(() =>
			{
				if (response.error != null)
				{
					FSBL.Logger.Error(new JToken[] { "Error when receiving linker state change data: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					var visiblePillsBeforeChange = LinkerGroups.Count(x => x.Value.Visible);

					var channels = response.response["channels"] as JArray;
					var allChannels = response.response["allChannels"] as JArray;

					if (LinkerGroups.Count == 0)
					{
						// setup linker channels only once
						CreateLinkerPills(allChannels);
					}

					foreach (JObject obj in allChannels)
					{
						var channelName = obj["name"].ToString();
						var channelColor = obj["color"].ToString();
						LinkerGroups[channelName].Visible = false;
						LinkerGroups[channelName].BackColor = ColorTranslator.FromHtml(channelColor);
					}

					foreach (JValue channel in channels)
					{
						var channelName = channel.Value.ToString();
						LinkerGroups[channelName].Visible = true;
					}

					AlignAllLinkerPills();

					var visiblePillsAfterChange = LinkerGroups.Count(x => x.Value.Visible);
					var marginShift = CalculateDragNDropLeftMarginShift(visiblePillsBeforeChange, visiblePillsAfterChange);
					DragNDropEmittingButton.Location = new Point(DragNDropEmittingButton.Location.X + marginShift, DragNDropEmittingButton.Location.Y);
				}
			}));
		}

		private void CreateLinkerPills(JArray allChannels)
		{
			this.Invoke(new Action(() =>
			{
				int positionX = 0;
				const int positionY = 10;
				

				var colorConverter = new ColorConverter();
				foreach (var channel in allChannels)
				{
					if (!LinkerGroups.ContainsKey(channel["name"].ToString()))
					{
						var label = channel["label"]?.ToString() ?? channel["name"].ToString();

						var button = new RoundedButton()
						{
							CornerRadius = 15,
							ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0),
							Margin = new Padding(2),
							Name = $"GroupButton{label.Replace(" ", "_")}",
							Visible = false,
							UseEllipse = true,
							UseVisualStyleBackColor = true,
							TextColor = Color.White,
							Size = new Size(LinkerPillWidth, LinkerPillHeight),
							Text = label,
							ButtonColor = (Color)colorConverter.ConvertFromString(channel["color"].ToString()),
							OnHoverTextColor = Color.Black,
							OnHoverButtonColor = Color.DarkOliveGreen,
							Location = new Point(positionX, positionY)
						};

						this.Controls.Add(button);

						positionX += LinkerPillWidth;
						LinkerGroups.Add(channel["name"].ToString(), button);
					}
				}
			}));
		}

		private void AlignAllLinkerPills()
		{
			var initialX = LinkerButton.Location.X + LinkerButton.Width + 2;
			var visiblePills = LinkerGroups.Where(x => x.Value.Visible).ToArray();
			for (int i = 0; i < visiblePills.Count(); i++)
			{
				var button = visiblePills[i].Value;
				button.Location = new Point(initialX + (i * LinkerPillWidth), button.Location.Y);
			}
		}

		private int CalculateDragNDropLeftMarginShift(int visiblePillsBeforeShift, int visiblePillsAfterShift)
		{
			return (visiblePillsAfterShift - visiblePillsBeforeShift) * (LinkerPillWidth + 2);
		}

		private void HandleWindowGrouping(object s, FinsembleEventArgs res)
		{
			this.Invoke(new Action(() =>
			{
				if (res.error != null)
				{
					return;
				}

				var groupData = res.response["data"]["groupData"] as JObject;
				var thisWindowGroups = new JObject
				{
					["dockingGroup"] = "",
					["snappingGroup"] = "",
					["topRight"] = false
				};

				foreach (var obj in groupData)
				{
					var windowgroupid = obj.Key;
					var windowgroup = groupData[windowgroupid] as JObject;
					var windownames = windowgroup["windowNames"] as JArray;

					thisWindowGroups = FormWindowGroupsTo(windowgroup, windownames, thisWindowGroups);
				}

				UpdateViewDueToWindowGroups(thisWindowGroups);
			}));
		}

		private void SetInitialWindowGrouping(JArray groupdWindowBelongsTo)
		{
			this.Invoke(new Action(() =>
			{
				var thisWindowGroups = new JObject
				{
					["dockingGroup"] = "",
					["snappingGroup"] = "",
					["topRight"] = false
				};

				foreach (var group in groupdWindowBelongsTo)
				{
					var windowNames = group["windowNames"] as JArray;
					thisWindowGroups = FormWindowGroupsTo(group, windowNames, thisWindowGroups);
				}

				UpdateViewDueToWindowGroups(thisWindowGroups);
			}));
		}

		private JObject FormWindowGroupsTo(JToken group, JArray windowNames, JObject thisWindowGroups)
		{
			var currentWindowName = FSBL.WindowClient.GetWindowIdentifier()["windowName"].ToString();

			var windowingGroup = false;
			for (int i = 0; i < windowNames.Count; i++)
			{
				var windowName = windowNames[i].ToString();
				if (windowName == currentWindowName)
				{
					windowingGroup = true;
				}
			}

			if (windowingGroup)
			{
				var isMovable = (bool)group["isMovable"];
				if (isMovable)
				{
					thisWindowGroups["dockingGroup"] = group;
					if (group["topRightWindow"].ToString() == currentWindowName)
					{
						thisWindowGroups["topRight"] = true;
					}
				}
				else
				{
					thisWindowGroups["snappingGroup"] = group;
				}
			}

			return thisWindowGroups;
		}

		private void UpdateViewDueToWindowGroups(JObject thisWindowGroups)
		{
			if (!string.IsNullOrEmpty(thisWindowGroups?["dockingGroup"]?.ToString()))
			{
				// docked
				DockingButton.Text = "@";
				DockingButton.ButtonColor = Color.FromArgb(3, 155, 255);
				DockingButton.OnHoverButtonColor = Color.FromArgb(3, 155, 255);
				DockingButton.Visible = true;
			}
			else if (!string.IsNullOrEmpty(thisWindowGroups?["snappingGroup"]?.ToString()))
			{
				// Snapped
				DockingButton.Text = ">";
				DockingButton.ButtonColor = Color.FromArgb(34, 38, 47);
				DockingButton.OnHoverButtonColor = Color.FromArgb(40, 45, 56);
				DockingButton.Visible = true;
			}
			else
			{
				// unsnapped/undocked
				DockingButton.Visible = false;
			}
		}

		private void HandleGetComponentState(object s, FinsembleEventArgs state)
		{
			this.Invoke(new Action(() =>
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
								DataLabel.Text = symbolTxt;
								DataToSendInput.Text = symbolTxt;
								SourceLabel.Text = "via component state";
							}
						}
					}
					else
					{
						// Example for Handling Spawn data
						FSBL.WindowClient.GetSpawnData(HandleSpawnData);
					}
				}
				else
				{
					// Example for Handling Spawn data
					FSBL.WindowClient.GetSpawnData(HandleSpawnData);
				}
			}));
		}

		private void HandleSpawnData(Object sender, FinsembleEventArgs res)
		{
			this.Invoke(new Action(async () =>
			{
				var receivedData = (JObject)res.response;
				if (res.error != null)
				{
					FSBL.Logger.Error(new JToken[] { "Error when retrieving spawn data: ", res.error.ToString() });
				}
				else if (res.response != null)
				{
					JToken value = receivedData.GetValue("spawndata");
					if (value != null)
					{
						DataLabel.Text = value.ToString();
						DataToSendInput.Text = value.ToString();
						SourceLabel.Text = "via Spawndata";
						await SaveStateAsync();
					}
				}
			}));
		}

		private void HandlePubSub(object sender, FinsembleEventArgs state)
		{
			this.Invoke(new Action(async () =>
			{
				try
				{
					if (state.error != null)
					{
						FSBL.Logger.Error(new JToken[] { "Error when retrieving spawn data: ", state.error.ToString() });
					}
					else if (state.response != null)
					{
						var pubSubData = (JObject)state.response;
						var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
						if (theData != null)
						{
							DataLabel.Text = theData;
							DataToSendInput.Text = theData;
							SourceLabel.Text = "via PubSub";
							await SaveStateAsync();
						}
					}
				}
				catch (Exception ex)
				{
					FSBL.Logger.Error(new JToken[] { "Error when retrieving linker channels: ", ex.Message });
				}
			}));
		}

		private void HandleDragAndDropReceive(object sender, FinsembleEventArgs args)
		{
			this.Invoke(new Action(async () =>
			{
				if (args.error != null)
				{
					FSBL.Logger.Error(new JToken[] { "Error when receiving drag and drop data: ", args.error.ToString() });
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
							DataLabel.Text = data.ToString();
							DataToSendInput.Text = data.ToString();
							SourceLabel.Text = "via Drag and Drop";
							await SaveStateAsync();
						}
					}
				}
			}));
		}

		private JObject HandleDragAndDropEmit()
		{
			return new JObject
			{
				["symbol"] = DataToSendInput.Text,
				["description"] = "Symbol " + DataToSendInput.Text
			};
		}

		private void HandleLinkerData(object sender, FinsembleEventArgs response)
		{
			this.Invoke(new Action(async () =>
			{
				if (response.error != null)
				{
					FSBL.Logger.Error(new JToken[] { "Error when receiving linker data: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					string value = response.response?["data"]?.ToString();
					DataLabel.Text = value;
					SourceLabel.Text = "via Linker";
					DataToSendInput.Text = value;
					await SaveStateAsync();
				}
			}));
		}

		private void HandleComponentsList(Object sender, FinsembleEventArgs response)
		{
			this.Invoke(new Action(() =>
			{
				if (response.error != null)
				{
					FSBL.Logger.Error(new JToken[] { "Error when receiving spawnable component list: ", response.error.ToString() });
				}
				else if (response.response != null)
				{
					var components = (JObject)response.response?["data"];
					foreach (var property in components?.Properties())
					{
						object value = components?[property.Name]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
						if ((value != null) && bool.Parse(value.ToString()))
						{
							Dispatcher.CurrentDispatcher.Invoke(() =>
							{
								//elimination of duplicate names of components after Finsemble restart
								if (!ComponentDropDown.Items.Contains(property.Name))
								{
									ComponentDropDown.Items.Add(property.Name);
									ComponentDropDown.SelectedIndex = 0;
								}
							});
						}
					}
				}
			}));
		}

		private void handleKeyPresses(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)Keys.Return)
			{
				this.Invoke(new Action(async () =>
				{
					DataLabel.Text = DataToSendInput.Text;
					SourceLabel.Text = "via Text input";
					await SaveStateAsync();
				}));
				e.Handled = true;
			}
		}

		// Example for saving component state
		private async Task SaveStateAsync()
		{
			await FSBL.WindowClient.SetComponentState(new JObject
			{
				["field"] = "symbol",
				//["value"] = datavalue.Text
			});
		}

		private async void LaunchButton_Click(object sender, EventArgs e)
		{
			object selected = ComponentDropDown.Text;
			if (selected == null) return;

			var componentName = selected.ToString();

			if (FSBL.FDC3Client != null)
			{
				var targetApp = new TargetApp() { Name = componentName };
				var context = new Context(new JObject
				{
					["type"] = "fdc3.instrument",
					["name"] = DataToSendInput.Text,
					["id"] = new JObject
					{
						["ticker"] = DataToSendInput.Text
					}
				});

				var openError = await FSBL.FDC3Client.DesktopAgentClient.Open(targetApp, context);
				if (openError.HasValue) MessageBox.Show(openError.ToString());
			}
			else
			{
				FSBL.LauncherClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
			}
		}

		private void LinkerButton_Click(object sender, EventArgs e)
		{
			if (FSBL.LinkerClient != null)
			{
				FSBL.LinkerClient.OpenLinkerWindow();
			}
			else
			{
				FSBL.Util.OpenLinkerWindow();
			}
		}

		private void UpdateAlwaysOnTopButton(bool isAlwaysOnTop)
		{
			if (isAlwaysOnTop)
			{
				AlwaysOnTopButton.ButtonColor = Color.FromArgb(3, 155, 255);
				AlwaysOnTopButton.OnHoverButtonColor = Color.FromArgb(3, 155, 255);
			}
			else
			{
				AlwaysOnTopButton.ButtonColor = Color.FromArgb(34, 38, 47);
				AlwaysOnTopButton.OnHoverButtonColor = Color.FromArgb(40, 45, 56);
			}
		}

		private async void AlwaysOnTopButton_Click(object sender, EventArgs e)
		{
			var isAlwaysOnTop = await FSBL.WindowClient.IsAlwaysOnTop();

			var newAlwaysOnTop = !isAlwaysOnTop;
			await FSBL.WindowClient.SetAlwaysOnTop(newAlwaysOnTop);

			UpdateAlwaysOnTopButton(newAlwaysOnTop);
		}

		private void DockingButton_Click(object sender, EventArgs e)
		{
			var currentWindowName = FSBL.WindowClient.GetWindowIdentifier()["windowName"].ToString();
			if (DockingButton.Text == ">")
			{
				FSBL.RouterClient.Transmit("DockingService.formGroup", new JObject { ["windowName"] = currentWindowName });
			}
			else
			{
				FSBL.RouterClient.Query("DockingService.leaveGroup", new JObject { ["name"] = currentWindowName }, delegate (object s, FinsembleEventArgs res) { });
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			if (FSBL.LinkerClient != null)
			{
				FSBL.LinkerClient.Publish(new JObject
				{
					["dataType"] = "symbol",
					["data"] = DataToSendInput.Text
				});
			}
			else
			{
				// Use Default FDC3
				//Broadcast
				var context = new Context(new JObject
				{
					["type"] = "fdc3.instrument",
					["name"] = DataToSendInput.Text,
					["id"] = new JObject
					{
						["ticker"] = DataToSendInput.Text
					}
				});

				FSBL.FDC3Client.DesktopAgentClient.Broadcast(context);
			}
		}

		private void DragNDropEmittingButton_MouseDown(object sender, MouseEventArgs e)
		{
			FSBL.DragAndDropClient.DragStartWithData(sender);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			//Dispose of the FSSBL object when the form is closed so that Finsemble is aware we've closed
			FSBL.Dispose();
			base.OnFormClosing(e);
		}
	}
}
