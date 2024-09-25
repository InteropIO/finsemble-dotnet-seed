using Finsemble.Core.Clients.FDC3.Interfaces;
using Finsemble.Core.Clients.FDC3.Types;
using Finsemble.Core.Clients.Router;
using Finsemble.Winform.Core;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformExampleCore
{
	public partial class MainForm : Form
	{
		public FinsembleWinform FSBL;
		
		private string[] _startupArgs;
		private IListener _contextListenter;
		const int LinkerPillWidth = 15;
		const int LinkerPillHeight = 24;

		private SortedDictionary<string, Button> LinkerGroups = new SortedDictionary<string, Button>();
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

		public MainForm(string[] args)
		{
			InitializeComponent();

			this.Visible = false;
			_startupArgs = args;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			this.Visible = false;

			MessagesRichBox.SendToBack();
			DataToSendInput.EnterKeyPressed += DataToSendInput_EnterKeyPressed; ;

			ConnectToFinsemble();
		}

		#region Finsemble 
		private async void ConnectToFinsemble()
		{
			FSBL = new FinsembleWinform(this, this.Handle.ToString("X"), _startupArgs);
			FSBL.Connected += Finsemble_Connected;
			await FSBL.Connect("WinformExampleCore", JWK);
		}

		private async void Finsemble_Connected(object sender, EventArgs e)
		{
			Trace.TraceInformation("FSBL Ready.");

			FSBL.Clients.Logger.OnLog += Logger_OnLog;
			FSBL.Clients.Logger.Log(new JToken[] { "Winform Example Core connected to Finsemble." });


			// Update UI buttons
			FSBL.Clients.WindowClient.WindowStateChanged += WindowClient_WindowStateChanged;
			WindowClient_WindowStateChanged(null, FSBL.Clients.WindowClient.CurrentWindowState);

			// hide and disable linker and docking buttons if IOCD connected
			// IOCD window wrapper already has corresponding built in components
			if (FSBL.IsIOCDConnected)
			{
				LinkerButton.Enabled = false;
				LinkerButton.Visible = false;

				DockingButton.Enabled = false;
				DockingButton.Visible = false;
			}

			#region FDC3
			if (FSBL.Clients.Fdc3Client != null)
			{
				// Listen to Fdc3Client state change to render connected channels
				FSBL.Clients.Fdc3Client.StateChanged += Fdc3Client_StateChanged;
				// Show joined channels
				Fdc3Client_StateChanged(null, FSBL.Clients.Fdc3Client.LastStateChangedArgs);

				// Example for Fdc3Client subscribe to specific context. The "*" for subscription to all contexts.
				_ = FSBL.Clients.Fdc3Client.DesktopAgentClient.AddContextListener<Context>("fdc3.instrument", HandleContext);
			}
			#endregion

			//Example for Handling PubSub data
			FSBL.Clients.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", HandlePubSub);

			// Example for getting Spawnable component list
			HandleComponentsList();

			LoadAndSetButtonsFont();
			this.Visible = true;
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

		private async void HandleComponentsList()
		{
			try
			{
				var response = await FSBL.Clients.ConfigClient.Get(new [] { "finsemble", "apps" });

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
							this.Invoke(new Action(() =>
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
			catch (Exception ex)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "Error when receiving spawnable component list: ", ex.ToString() });
			}
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

		private void Fdc3Client_StateChanged(object sender, FinsembleEventArgs response)
		{
			if (response == null) return;
			this.Invoke(new Action(async () =>
			{
				if (response.error != null)
				{
					FSBL.Clients.Logger.Error(new JToken[] { "Error when receiving linker state change data: ", response.error.ToString() });
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
						var key = obj["name"]?.ToString();
						if (key != null && LinkerGroups.ContainsKey(key))
						{
							LinkerGroups[key].Visible = false;
							LinkerGroups[key].BackColor = ColorTranslator.FromHtml(obj["color"].ToString());
						}
					}

					foreach (JValue channel in channels)
					{
						var key = channel.Value?.ToString();
						if (key != null && LinkerGroups.ContainsKey(key))
						{
							LinkerGroups[key].Visible = true;
						}
					}

					var visiblePillsAfterChange = LinkerGroups.Count(x => x.Value.Visible);

					AlignAllLinkerPills();
				}
			}));
		}

		private void CreateLinkerPills(JArray allChannels)
		{
			this.Invoke(new Action(() =>
			{
				int positionX = 0;
				const int positionY = 19;


				var colorConverter = new ColorConverter();
				foreach (var channel in allChannels)
				{
					if (!LinkerGroups.ContainsKey(channel["name"].ToString()))
					{
						var label = channel["label"]?.ToString() ?? channel["name"].ToString();

						var button = new Controls.RoundedButton()
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

		private void HandlePubSub(object sender, FinsembleEventArgs args)
		{
			this.Invoke(new Action(() =>
			{
				try
				{
					if (args.error != null)
					{
						FSBL.Clients.Logger.Error(new JToken[] { "Error when retrieving spawn data: ", args.error.ToString() });
					}
					else if (args.response != null)
					{
						var pubSubData = (JObject)args.response;
						var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
						if (theData != null)
						{
							DataLabel.Text = theData;
							DataToSendInput.Text = theData;
							SourceLabel.Text = "via PubSub";
						}
					}
				}
				catch (Exception ex)
				{
					FSBL.Clients.Logger.Error(new JToken[] { "Error when retrieving linker channels: ", ex.Message });
				}
			}));
		}

		private void Logger_OnLog(object sender, JObject e)
		{
			MessagesRichBox.Invoke(new Action(() =>
			{
				MessagesRichBox.Text += e + "\n\n";
			}));
		}

		#endregion

		private void LoadAndSetButtonsFont()
		{
			finfont.AddFontFile(@"Resources\finfont.ttf");
			finfont.AddFontFile(@"Resources\font-finance.ttf");
			var finFont = new System.Drawing.Font(finfont.Families[0], 100);
			var fontFinance = new System.Drawing.Font(finfont.Families[1], 7);
			Invoke(new Action(() =>
			{
				LinkerButton.Font = fontFinance;
				AlwaysOnTopButton.Font = fontFinance;
				DockingButton.Font = fontFinance;

				DockingButton.UseEllipse = true;
				AlwaysOnTopButton.UseEllipse = true;
			}));
		}


		#region Buttons click & DND

		private void LinkerButton_Click(object sender, EventArgs e)
		{
			FSBL.Clients.Util.OpenLinkerWindow();
		}

		private void SendButton_Click(object sender, EventArgs e)
		{
			var param = new JObject
			{
				["type"] = "fdc3.instrument",
				["name"] = DataToSendInput.Text,
				["id"] = new JObject
				{
					["ticker"] = DataToSendInput.Text
				}
			};
			_ = FSBL.Clients.Fdc3Client.DesktopAgentClient.Broadcast(new Context(param));

			this.Invoke(new Action(() =>
			{
				SourceLabel.Text = "via Text entry";
				DataLabel.Text = DataToSendInput.Text;
			}));
		}

		private async void AlwaysOnTopButton_Click(object sender, EventArgs e)
		{
			var r = await FSBL.Clients.FinsembleWindow.IsAlwaysOnTop(new JObject());
			var oldState = r.response?["data"]?.ToObject<bool?>();
			var newAlwaysOnTop = !(oldState == true);
			await FSBL.Clients.WindowClient.SetAlwaysOnTop(newAlwaysOnTop);
		}


		private void DockingButton_Click(object sender, EventArgs args)
		{
			var currentWindowName = FSBL.Clients.WindowClient.GetWindowIdentifier()["windowName"].ToString();
			if (DockingButton.Text == ">")
			{
				FSBL.Clients.RouterClient.Transmit("DockingService.formGroup", new JObject { ["windowName"] = currentWindowName });
			}
			else
			{
				FSBL.Clients.RouterClient.Query("DockingService.leaveGroup", new JObject { ["name"] = currentWindowName }, delegate (object s, FinsembleEventArgs res) { });
			}
		}

		private async void LaunchButton_Click(object sender, EventArgs e)
		{
			object selected = ComponentDropDown.Text;
			if (selected == null) return;

			var componentName = selected.ToString();

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
				var appId = await FSBL.Clients.Fdc3Client.DesktopAgentClient.Open(new AppIdentifier(componentName), contextToSend);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
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

		private void DataToSendInput_EnterKeyPressed(object sender, EventArgs e)
		{
			DataLabel.Text = DataToSendInput.Text;
			SourceLabel.Text = "via Text input";
		}

		#endregion
	}
}
