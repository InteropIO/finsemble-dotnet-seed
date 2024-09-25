using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using InteropIO.FDC3.Types;
using System.Windows.Interop;
using InteropIO.FDC3.Interfaces;
using FDC3WPFExample.Types;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;

namespace FDC3WPFExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Finsemble FSBL;
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
		private IChannel AppChannel;
		private IListener AppChannelListener;

		/// <summary>
		/// The MainWindow is created by the App so that we can get command line arguments passed from Finsemble.
		/// </summary>
		/// <param name="args"></param>
		public MainWindow(string[] args)
		{
			// Trigger actions on close when requested by Finsemble, e.g.:	
			this.Closing += MainWindow_Closing;
			// If your window should support a transparency, AllowsTransparency and WindowStyle must be setted before EnsureHandle() call
			//this.AllowsTransparency = true;
			//this.WindowStyle = WindowStyle.None;

			// Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			new WindowInteropHelper(this).EnsureHandle();
			FSBL = new Finsemble(args, this); // Finsemble needs the command line arguments to connect and also this Window to manage snapping, docking etc.
			FSBL.Connected += Finsemble_Connected;
			FSBL.Connect("FDC3WPFExample", JWK);
		}

		private async void Finsemble_Connected(object sender, EventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate //main thread
			{
				// Initialize this Window and show it
				InitializeComponent(); // Initialize after Finsemble is connected
			});

			var result = await FSBL.Clients.ConfigClient.Get(new[] { "finsemble", "apps" });
			if (result?.response is JArray apps)
			{
				foreach (var app in apps)
				{
					object value = app?["hostManifests"]?["Finsemble"]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
					if ((value != null) && bool.Parse(value.ToString()))
					{
						Application.Current.Dispatcher.Invoke(delegate //main thread
						{
							ComponentSelect.ItemsComboBox.Items.Add(app?["appId"]?.ToString());
						});
					}
				}
			}

			//restore state if one exists
			InitializeFromStateOrSpawnData();

			if (FSBL.Clients.FDC3Client is object)
			{
				//Context handler
				ContextHandler<MarketContext> contextHandler = (context, metadata) =>
				{
					FSBL.Clients.Logger.Log(new JToken[] { "context received by contextHandler.", JObject.FromObject(context) });
					if (context.Type.Equals("fdc3.instrument"))
					{
						Application.Current.Dispatcher.Invoke(delegate //main thread
						{
							DroppedData.Content = context.Id?.Ticker;
							DroppedDataSource.Content = "context shared via FDC3";
						});
						_ = SaveStateAsync(DroppedData.Content.ToString());
					}
				};
				_ = FSBL.Clients.FDC3Client.DesktopAgentClient.AddContextListener(null, contextHandler);

				//To add a filtered context listener
				//await FSBL.FDC3Client.DesktopAgentClient.AddContextListener("fdc3.instrument", contextHandler);

				//Intent handler
				IntentHandler<MarketContext> intentHandler = async (context, metadata) =>
				{
					FSBL.Clients.Logger.Log(new JToken[] { "context received by intentHandler.", JObject.FromObject(context) });
					if (context.Type != null && context.Type.Equals("fdc3.instrument"))
					{
						string ticker = context.Id?.Ticker;
						FSBL.Clients.Logger.Log(new JToken[] { "updating state to ticker:" + ticker });

						Application.Current.Dispatcher.Invoke(delegate //main thread
						{
							DroppedData.Content = ticker;
							DroppedDataSource.Content = "context shared via FDC3 intent";
						});
						await SaveStateAsync(ticker);
					}
					else if (context == null)
					{
						FSBL.Clients.Logger.Log(new JToken[] { "null context received by intentHandler." });
					}
					else
					{
						FSBL.Clients.Logger.Log(new JToken[] { "unrecognized context type received by intentHandler.", context.ToString() });
					}

					return new ContextIntentResult() { Context = context };
				};
				var addListenerTask = FSBL.Clients.FDC3Client.DesktopAgentClient.AddIntentListener("ViewChart", intentHandler);
			}
			else
			{
				FSBL.Clients.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}

			// setup default AppChannel
			AppChannelInput.TextBox.Text = "ExampleAppChannel";
			SetupAppChannel();

			Application.Current.Dispatcher.Invoke(delegate
			{
				// setup default contexts
				SetupDefaultContexts();

				// setup a list of available intents
				LoadAvailableIntents();

				this.Show();
			});

			FSBL.Clients.Logger.OnLog += Logger_OnLog;
			FSBL.Clients.Logger.System.OnLog += Logger_OnLog;
			FSBL.Clients.Logger.Perf.OnLog += Logger_OnLog;
		}

		private void SetupDefaultContexts()
		{
			// load all contexts
			foreach (var item in DefaultData.DefaultContexts)
			{
				ContextTemplatesDropDown.ItemsComboBox.Items.Add(item.Key);
			}

			ContextTemplatesDropDown.ItemsComboBox.SelectionChanged += (object s, System.Windows.Controls.SelectionChangedEventArgs selectionEvent) =>
			{
				var contextType = ContextTemplatesDropDown.ItemsComboBox.SelectedValue as string;
				var serializer = new JsonSerializer()
				{
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				};
				ContextTextBox.Text = JObject.FromObject(DefaultData.DefaultContexts[contextType], serializer).ToString();
			};

			// choose most popular
			if (DefaultData.DefaultContexts.ContainsKey("fdc3.instrument"))
			{
				ContextTemplatesDropDown.ItemsComboBox.SelectedItem = "fdc3.instrument";
			}
			else
			{
				ContextTextBox.Text = @"{ }";
			}
		}

		private async void LoadAvailableIntents()
		{
			var intents = await FSBL.Clients.FDC3Client.DesktopAgentClient.FindIntentsByContext(new InstrumentContext());
			if (intents != null && intents.Length > 0)
			{
				foreach (var intent in intents)
				{
					IntentsDropDown.ItemsComboBox.Items.Add(intent.Intent.Name);
				}
			}
		}

		private async void SetupAppChannel()
		{
			// Don't update channel if it has the same id
			if (AppChannelInput.TextBox.Text == AppChannel?.Id)
			{
				return;
			}

			if (AppChannelListener != null)
			{
				AppChannelListener.Unsubscribe();
			}
			// NOTE: it possible to listen to multiple channels simultaneously, but that we do not in this example
			AppChannel = await FSBL.Clients.FDC3Client.DesktopAgentClient.GetOrCreateChannel(AppChannelInput.TextBox.Text);
			AppChannelListener = await AppChannel.AddContextListener<MarketContext>(null, (context, metadata) =>
			{
				Application.Current.Dispatcher.Invoke(delegate //main thread
				{
					DroppedData.Content = context.Id?.Ticker;
					DroppedDataSource.Content = $"context received via App channel: {AppChannel.Id}";
				});
			});
		}

		private async void OpenComponent_Click(object sender, RoutedEventArgs e)
		{
			object selected = ComponentSelect.ItemsComboBox.SelectedValue;
			if (selected != null)
			{
				string componentName = selected.ToString();

				if (FSBL.Clients.FDC3Client is object)
				{
					//FDC3 Usage example 
					//open

					var targetApp = new AppMetadata() { AppId = componentName };
					var context = GetContext();
					if (context != null)
					{
						try
						{
							var appIdentifier = await FSBL.Clients.FDC3Client.DesktopAgentClient.Open(targetApp, context);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message.ToString());
						}
					}
				}
				else
				{
					FSBL.Clients.AppsClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
				}
			}
		}

		private void SendSystemContext_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.Clients.FDC3Client is object)
			{
				//FDC3 Usage example 
				//Broadcast via system channels
				var context = GetContext();

				if (context != null)
				{
					var broadcastTask = FSBL.Clients.FDC3Client.DesktopAgentClient.Broadcast(context);
				}
			}
			else
			{
				FSBL.Clients.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}
		}

		private void SendApp_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.Clients.FDC3Client is object)
			{
				SetupAppChannel();
				//FDC3 Usage example 
				//Broadcast via app channel
				var context = GetContext();
				if (context != null)
				{
					AppChannel.Broadcast(context);
				}
			}
			else
			{
				FSBL.Clients.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}
		}

		private async void RaiseIntent_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.Clients.FDC3Client is object)
			{
				//FDC3 Usage example 
				//RaiseIntent
				var context = GetContext();
				if (context != null)
				{
					var intent = IntentsDropDown.ItemsComboBox.SelectedItem as string;
					try
					{
						await FSBL.Clients.FDC3Client.DesktopAgentClient.RaiseIntent(intent, context);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			else
			{
				FSBL.Clients.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}
		}

		private async void RaiseIntentForContext_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.Clients.FDC3Client is object)
			{
				//FDC3 Usage example 
				//RaiseIntentForContext
				var context = GetContext();
				if (context != null)
				{
					try
					{
						await FSBL.Clients.FDC3Client.DesktopAgentClient.RaiseIntentForContext(context);
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
			else
			{
				FSBL.Clients.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}
		}

		#region Other FDC3 examples
		//Intent 
		//var context = new Context(new JObject
		//{
		//	["type"] = "fdc3.instrument",
		//	["name"] = "Microsoft",
		//	["id"] = new JObject
		//	{
		//		["ticker"] = "MSFT"
		//	}
		//});
		//var intentResolution = await FSBL.FDC3Client.DesktopAgentClient.RaiseIntent("ViewChart", context, null);
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, RaiseIntent.", JsonConvert.SerializeObject(intentResolution, Formatting.Indented) });

		//var intent = await FSBL.FDC3Client.DesktopAgentClient.FindIntent("ViewChart", null);
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, FindIntent.", JsonConvert.SerializeObject(intent, Formatting.Indented) });

		//var context = new Context(new JObject
		//{
		//	["type"] = "fdc3.instrument"
		//});
		//var intent = FSBL.FDC3Client.DesktopAgentClient.FindIntent("ViewChart", context);
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, FindIntent with context.", JsonConvert.SerializeObject(intent, Formatting.Indented) });

		//var context = new Context(new JObject
		//{
		//	["type"] = "fdc3.instrument"
		//});
		//var intents = await FSBL.FDC3Client.DesktopAgentClient.FindIntentsByContext(context);
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, FindIntentsByContext.", JsonConvert.SerializeObject(intents, Formatting.Indented) });

		//Context
		//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetCurrentChannel();
		//var context = await channel.GetCurrentContext("fdc3.instrument");
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentContext.", context.Value });

		//Channels
		//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetOrCreateChannel("test");

		//var channels = await FSBL.FDC3Client.DesktopAgentClient.GetSystemChannels();
		//foreach (var channel in channels)
		//{
		//	FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getSystemChannels.", channel.Id });
		//}

		//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetCurrentChannel();
		//FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentChannel.", channel.Id });

		#endregion


		private void Logger_OnLog(object sender, JObject e)
		{
			//clean up stringified log messages for display
			//note error handling code needs adding to this to make it robust
			if (e["logData"] != null)
			{
				e["logData"] = JArray.Parse(e["logData"].ToString());//.Replace("\\r\\n", "\n");
			}
			string logMsg = e.ToString().Replace("\\r\\n", "\n").Replace("\\\\", "\\");

			Application.Current.Dispatcher.Invoke(() =>
			{
				LogsTextBox.Text += logMsg + "\n";
			});
		}

		/// <summary>	
		/// Example window close handler	
		/// </summary>	
		/// <param name="sender"></param>	
		/// <param name="e"></param>	
		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			/*if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)	
			{	
				// Cancel Closing	
				e.Cancel = true;	
				return;	
			}*/
		}

		private async Task SaveStateAsync(string valueToSave)
		{
			try
			{
				await FSBL.Clients.WindowClient.SetComponentState(new JObject
				{
					["field"] = "symbol",
					["value"] = valueToSave
				});
			}
			catch (ApplicationException e)
			{
				FSBL.Clients.Logger.Warn(new JToken[] { "SaveState Warn", e.Message, e.StackTrace });
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "SaveState Error", e.Message, e.StackTrace });
			}
		}


		private async void InitializeFromStateOrSpawnData()
		{
			try
			{
				JToken state = await FSBL.Clients.WindowClient.GetComponentState(new JObject { ["field"] = "symbol" });
				string symbolTxt = state == null ? null : state["data"]?.ToString();
				if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
				{
					Application.Current.Dispatcher.Invoke(delegate //main thread
					{
						DroppedData.Content = symbolTxt;
						DroppedDataSource.Content = "via component state";
					});
				}
				else
				{
					//Get SpawnData if no previous state
					var spawnData = await FSBL.Clients.WindowClient.GetSpawnData();
					symbolTxt = spawnData?["symbol"]?.ToString();
					Application.Current.Dispatcher.Invoke(delegate //main thread
					{
						if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
						{
							DroppedData.Content = symbolTxt;
							DroppedDataSource.Content = "via SpawnData";
						}
						else
						{
							DroppedData.Content = "MSFT";
							DroppedDataSource.Content = "via default value";
						}
					});
					await SaveStateAsync(DroppedData.Content.ToString());
				}
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger.Warn(new JToken[] { "InitializeFromStateOrSpawnData Error, it is likely no state was found", e.Message, e.StackTrace });
			}
		}

		private Context GetContext()
		{
			try
			{
				var context = new Context(JObject.Parse(ContextTextBox.Text));
				if (string.IsNullOrEmpty(context.Type))
				{
					DroppedDataSource.Content = "Failed to parse intent context. It should have a Type property." + "\n";
					return null;
				}

				return context;
			}
			catch
			{
				DroppedDataSource.Content = "Failed to parse intent context" + "\n";
				return null;
			}
		}
	}
}
