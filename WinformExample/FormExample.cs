using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using InteropIO.Router;
using WinformExample.Controls;
using Color = System.Drawing.Color;
using InteropIO.FDC3.Types;
using Microsoft.IdentityModel.Tokens;
using InteropIO.FDC3.Interfaces;
using System.Diagnostics;

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

		public Finsemble FSBL = null;

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
			if (FSBL.Clients.Logger != null)
			{
				FSBL.Clients.Logger.OnLog += Logger_OnLog;

				FSBL.Clients.Logger.Log(new JToken[] { "Winform example connected to Finsemble." });
			}
			Trace.TraceInformation("FSBL Ready.");

			// Update UI buttons
			if (FSBL.Clients.WindowClient != null)
			{
				// hide and disable linker and docking buttons if IOCD connected
				// IOCD window wrapper already has corresponding built in components
				if (FSBL.IsIOCDConnected)
				{
					LinkerButton.Enabled = false;
					LinkerButton.Visible = false;

					DockingButton.Enabled = false;
					DockingButton.Visible = false;
				}

				FSBL.Clients.WindowClient.WindowStateChanged += WindowClient_WindowStateChanged;
				WindowClient_WindowStateChanged(null, FSBL.Clients.WindowClient.CurrentWindowState);

				//Example for handling component state in a workspace (and hand-off to getSpawnData if no state found)
				var state = await FSBL.Clients.WindowClient?.GetComponentState(new JObject { ["field"] = "symbol" });
				HandleGetComponentState(state);
			}

			FSBL.Clients.FDC3Client.StateChanged += HandleLinkerStateChange;

			//setting initial state of linked channels
			HandleLinkerStateChange(null, FSBL.Clients.FDC3Client.LastStateChangedArgs);

			// Example for Fdc3Client subscribe to specific context. The "*" for subscription to all contexts.
			var listener = FSBL.Clients.FDC3Client.DesktopAgentClient?.AddContextListener("fdc3.instrument", HandleContext);

			// Example for Handling PubSub data
			FSBL.Clients.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", HandlePubSub);

			// Using custom fonts
			finfont.AddFontFile(@"Resources\finfont.ttf");
			finfont.AddFontFile(@"Resources\font-finance.ttf");
			var finFont = new Font(finfont.Families[0], 100);
			var fontFinance = new Font(finfont.Families[1], 7);
			Invoke(new Action(() =>
			{
				LinkerButton.Font = fontFinance;
				AlwaysOnTopButton.Font = fontFinance;
				DockingButton.Font = fontFinance;
				DockingButton.UseEllipse = true;
				AlwaysOnTopButton.UseEllipse = true;
			}));

			// Example for getting Spawnable component list
			var args = await FSBL.Clients.ConfigClient.Get(new[] { "finsemble", "apps" });
			HandleComponentsList(this, args);
		}

		private void HandleContext(Context context, IContextMetadata metadata)
		{

			FSBL.Clients.Logger.Debug($"Context received: {context.Value}");

			if (context == null)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "Error when receiving context. Context is null " });
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
			if (response?.error != null)
			{
				FSBL.Clients.Logger?.Error(new JToken[] { "Error when receiving linker state change data: ", response.error.ToString() });
			}
			else if (response?.response != null)
			{
				Invoke(new Action(() =>
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
				}));
			}
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

		private void WindowClient_WindowStateChanged(object sender, JObject state)
		{
			var isGrouped = state?["isGrouped"]?.ToObject<bool>() ?? false;
			var groupable = state?["groupable"]?.ToObject<bool>() ?? false;
			var showAlwaysOnTopButton = state?["showAlwaysOnTopButton"]?.Value<bool>() ?? true;
			var isAlwaysOnTop = state?["alwaysOnTop"]?.Value<bool>() ?? false;

			this.Invoke(new Action(() =>
			{
				// update grouping button
				if (groupable)
				{
					DockingButton.Visible = true;

					DockingButton.Text = isGrouped ? "@" : ">";
					DockingButton.ButtonColor = isGrouped ? Color.FromArgb(3, 155, 255) : Color.FromArgb(34, 38, 47);
					DockingButton.OnHoverButtonColor = isGrouped ? Color.FromArgb(3, 155, 255) : Color.FromArgb(40, 45, 56);
				}
				else
				{
					DockingButton.Visible = false;
				}

				// update alwaysOnTopButton
				if (showAlwaysOnTopButton)
				{
					AlwaysOnTopButton.Visible = true;

					AlwaysOnTopButton.ButtonColor = isAlwaysOnTop ? Color.FromArgb(3, 155, 255) : Color.FromArgb(34, 38, 47);
					AlwaysOnTopButton.OnHoverButtonColor = isAlwaysOnTop ? Color.FromArgb(3, 155, 255) : Color.FromArgb(40, 45, 56);
				}
				else
				{
					AlwaysOnTopButton.Visible = false;
				}
			}));
		}

		private async void HandleGetComponentState(JToken state)
		{
			if (state is JValue symbol)
			{
				var symbolTxt = symbol?.ToString();
				if (!string.IsNullOrEmpty(symbolTxt))
				{
					Invoke(new Action(() =>
					{
						DataLabel.Text = symbolTxt;
						DataToSendInput.Text = symbolTxt;
						SourceLabel.Text = "via component state";
					}));
				}
			}
			else
			{
				// Example for Handling Spawn data
				var spawnData = await FSBL.Clients.WindowClient.GetSpawnData();
				HandleSpawnData(spawnData);
			}
		}

		private async void HandleSpawnData(JToken spawnData)
		{
			if (spawnData != null)
			{
				Invoke(new Action(() =>
				{
					DataLabel.Text = spawnData.ToString();
					DataToSendInput.Text = spawnData.ToString();
					SourceLabel.Text = "via spawn data";
				}));
				await SaveStateAsync();
			}
		}

		private void HandlePubSub(object sender, FinsembleEventArgs state)
		{
			Invoke(new Action(async () =>
			{
				try
				{
					if (state.error != null)
					{
						FSBL.Clients.Logger.Error(new JToken[] { "Error when retrieving spawn data: ", state.error.ToString() });
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
					FSBL.Clients.Logger.Error(new JToken[] { "Error when retrieving linker channels: ", ex.Message });
				}
			}));
		}

		private void HandleComponentsList(object sender, FinsembleEventArgs response)
		{
			if (response.error != null)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "Error when receiving spawnable component list: ", response.error.ToString() });
			}
			else if (response?.response != null && response.response is JArray apps)
			{
				foreach (var app in apps)
				{
					object value = app?["hostManifests"]?["Finsemble"]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
					if ((value != null) && bool.Parse(value.ToString()))
					{
						ComponentDropDown.Invoke(new Action(() =>
						{
							var appId = app?["appId"]?.ToString();
							//elimination of duplicate names of components after Finsemble restart
							if (!ComponentDropDown.Items.Contains(appId))
							{
								ComponentDropDown.Items.Add(appId);
								ComponentDropDown.SelectedIndex = 0;
							}
						}));
					}
				}
			}
		}

		// Example for saving component state
		private async Task SaveStateAsync()
		{
			await FSBL.Clients.WindowClient.SetComponentState(new JObject
			{
				["field"] = "symbol",
				["value"] = DataToSendInput.Text
			});
		}

		private async void LaunchButton_Click(object sender, EventArgs e)
		{
			object selected = ComponentDropDown.Text;
			if (selected == null) return;

			var componentName = selected.ToString();

			if (FSBL.Clients.FDC3Client != null)
			{
				var context = new Context(new JObject
				{
					["type"] = "fdc3.instrument",
					["name"] = DataToSendInput.Text,
					["id"] = new JObject
					{
						["ticker"] = DataToSendInput.Text
					}
				});

				// Check if the component able to receive the context
				var contextToSend = await ShouldSendContextToComponent(context, componentName) ? context : null;
				try
				{
					var appId = await FSBL.Clients.FDC3Client.DesktopAgentClient.Open(new AppIdentifier(componentName), contextToSend);
				}
				catch (Exception ex)
				{
					Trace.TraceError($"Failed to open the app: {ex.Message}");
				}
			}
			else
			{
				FSBL.Clients.AppsClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
			}
		}

		private async Task<bool> ShouldSendContextToComponent(Context context, string componentName)
		{
			var apps = (await FSBL.Clients.ConfigClient.Get(new[] { "finsemble", "apps" }))?.response as JArray;
			var appConfig = apps.FirstOrDefault(x => x is JObject app && app?["appId"]?.ToString() == componentName) as JObject;
			var intents = appConfig?["interop"]?["intents"]?["listensFor"]?.Children();

			foreach (var intent in intents)
			{
				if (intent?.First?["contexts"]?.ToString().Contains(context.Type) == true)
				{
					return true;
				}
			}

			return false;
		}

		private void LinkerButton_Click(object sender, EventArgs e)
		{
			FSBL.Clients.Util.OpenLinkerWindow();
		}

		private async void AlwaysOnTopButton_Click(object sender, EventArgs e)
		{
			if(FSBL.Clients.WindowClient != null)
			{
				var result = await FSBL.Clients.FinsembleWindow.IsAlwaysOnTop(new JObject());
				var isAlwaysOnTop = result?["data"]?.ToObject<bool>();

				if (isAlwaysOnTop != null)
				{
					var newAlwaysOnTop = !isAlwaysOnTop.Value;
					await FSBL.Clients.WindowClient.SetAlwaysOnTop(newAlwaysOnTop);
				}
			}
		}

		private void DockingButton_Click(object sender, EventArgs e)
		{
			if (DockingButton.Text == ">")
			{
				FSBL.Clients.WindowClient.FormGroup();
			}
			else
			{
				FSBL.Clients.WindowClient.EjectFromGroup();
			}
		}

		private void SendButton_Click(object sender, EventArgs e)
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

			_ = FSBL.Clients.FDC3Client.DesktopAgentClient.Broadcast(context);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			//Dispose of the FSSBL object when the form is closed so that Finsemble is aware we've closed
			FSBL.Dispose();
			base.OnFormClosing(e);
		}
	}
}
