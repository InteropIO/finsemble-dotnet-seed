using InteropIO;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using InteropIO.Router;
using InteropIO.FDC3.Types;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Interop;
using InteropIO.FDC3.Interfaces;
using System.Diagnostics;
using System.Linq;

namespace WPFExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// The logger
		/// </summary>

		public Finsemble FSBL;
		public event EventHandler WindowReady;

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
		private async void SpawnComponent_Click(object sender, RoutedEventArgs e)
		{
			object selected = ComponentSelect.ItemsComboBox.SelectedValue;
			if (selected != null)
			{
				string componentName = selected.ToString();

				if (FSBL.Clients.FDC3Client is object)
				{
					//FDC3 Usage example 
					//open

					var context = new Context(new JObject
					{
						["type"] = "fdc3.instrument",
						["name"] = DataToSend.TextBox.Text,
						["id"] = new JObject
						{
							["ticker"] = DataToSend.TextBox.Text
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

					//Intent 
					//var context = new Context(new JObject
					//{
					//	["type"] = "fdc3.instrument",
					//	["name"] = DataToSend.TextBox.Text,
					//	["id"] = new JObject
					//	{
					//		["ticker"] = DataToSend.TextBox.Text
					//	}
					//});

					//await FSBL.FDC3Client.DesktopAgentClient.RaiseIntent("ViewChart", context, null);

					//var intent = await FSBL.FDC3Client.DesktopAgentClient.FindIntent("ViewChart", null); 
					//FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntent.", JsonConvert.SerializeObject(intent, Formatting.Indented)});

					//var intent = await FSBL.FDC3Client.DesktopAgentClient.FindIntent("ViewChart", new Context(new JObject
					//{
					//	["type"] = "fdc3.instrument"
					//}));
					//FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntent.", JsonConvert.SerializeObject(intent, Formatting.Indented) });

					//var context = new Context(new JObject
					//{
					//	["type"] = "fdc3.instrument"
					//});

					//var intents = await FSBL.FDC3Client.DesktopAgentClient.FindIntentsByContext(context);
					//FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntentsByContext.", JsonConvert.SerializeObject(intents, Formatting.Indented)});

					//Context
					//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetCurrentChannel();
					//var context = await channel.GetCurrentContext("fdc3.instrument");, 
					//FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentContext.", context.Value.ToString() });

					//Channels
					//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetOrCreateChannel("test");

					//var channels = await FSBL.FDC3Client.DesktopAgentClient.GetSystemChannels();
					//foreach (IChannel channel in channels)
					//{
					//	FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getSystemChannels.", channel.Id });
					//}

					//var channel = await FSBL.FDC3Client.DesktopAgentClient.GetCurrentChannel();
					//FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentChannel.", channel.Id });
				}
				else
				{
					FSBL.Clients.AppsClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
				}
			}
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
			//FDC3 Usage example 
			//Broadcast
			var context = new Context(new JObject
			{
				["type"] = "fdc3.instrument",
				["name"] = DataToSend.TextBox.Text,
				["id"] = new JObject
				{
					["ticker"] = DataToSend.TextBox.Text
				}
			});

			FSBL.Clients.FDC3Client.DesktopAgentClient.Broadcast(context);

			FSBL.getDispatcher().Invoke(async delegate //main thread
			{
				DroppedData.Content = DataToSend.TextBox.Text;
				DroppedDataSource.Content = "via Text entry";
				await SaveStateAsync();
			});
		}

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
			// For authentication in the InteropService use the appId from appd.json and static auth token
			FSBL.Connect("Finsemble WPF Demo", JWK);
		}

		private async void Finsemble_Connected(object sender, EventArgs e)
		{
			this.Dispatcher.Invoke(delegate //main thread
			{
				// Initialize this Window and show it
				InitializeComponent(); // Initialize after Finsemble is connected

				//Subscribe to a PubSub topic
				//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
				//     This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
				Subscribe_to_pubsub();

				//FSBL.LinkerClient?.LinkToChannel("group2", null, (s, a) => { });
				//restore state if one exists
				UpdateDisplayData();
				this.Show();
			});

			// load available component list
			var result = await FSBL.Clients.ConfigClient.Get(new[] { "finsemble", "apps" });
			if (result?.response is JArray apps)
			{
				foreach (var app in apps)
				{
					object value = app?["hostManifests"]?["Finsemble"]?["foreign"]?["components"]?["App Launcher"]?["launchableByUser"];
					if ((value != null) && bool.Parse(value.ToString()))
					{
						FSBL.getDispatcher().Invoke(delegate //main thread
						{
							ComponentSelect.ItemsComboBox.Items.Add(app?["appId"]?.ToString());
						});
					}
				}
			}

			//FDC3 Usage example	
			FSBL.getDispatcher().Invoke(delegate //main thread	
			{
				//	FDC3Label.Visibility = Visibility.Visible;
			});

			//Context handler
			ContextHandler contextHandler = (context, metadata) =>
			{
				FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, context received by contextHandler.", context.Value });
				if (context.Type.Equals("fdc3.instrument"))
				{
					FSBL.getDispatcher().Invoke(async delegate //main thread
					{
						DataToSend.TextBox.Text = context.Id?["ticker"]?.ToString();
						DroppedData.Content = context.Id?["ticker"]?.ToString();
						DroppedDataSource.Content = "context shared via FDC3";
						await SaveStateAsync();
					});
				}
			};
			//FSBL.FDC3Client.DesktopAgentClient.AddContextListener(contextHandler);
			FSBL.Clients.FDC3Client.DesktopAgentClient.AddContextListener("fdc3.instrument", contextHandler);

			IntentHandler intentHandler = async (context, metadata) =>
			{
				FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: context received by intentHandler.", context.Value });
				if (context.Type != null && context.Type.Equals("fdc3.instrument"))
				{
					await FSBL.getDispatcher().Invoke(async delegate //main thread
					{
						DataToSend.TextBox.Text = context.Id?["ticker"]?.ToString();
						DroppedData.Content = context.Id?["ticker"]?.ToString();
						DroppedDataSource.Content = "context shared via FDC3 intent";
						await SaveStateAsync();
					});
				}

				return new ContextIntentResult() { Context = context };
			};
			FSBL.Clients.FDC3Client.DesktopAgentClient.AddIntentListener("ViewChart", intentHandler);

			if (FSBL.Clients.Logger != null)
			{
				FSBL.Clients.Logger.OnLog += Logger_OnLog;
				FSBL.Clients.Logger.System.OnLog += Logger_OnLog;
				FSBL.Clients.Logger.Perf.OnLog += Logger_OnLog;
			}


			// Logging to the Finsemble Central Console
			/*
			FSBL.Logger.Error(new JToken[] {"Error Test"});

			FSBL.Logger.Log(new JToken[] {"Log Test"});

			FSBL.Logger.Debug(new JToken[] {"Debug Test"});

			FSBL.Logger.Info(new JToken[] {"Info Test"});

			FSBL.Logger.Verbose(new JToken[] {"Verbose Test"});
			*/

			////Sample code to execute LauncherClient.getActiveDescriptors()
			//FSBL.LauncherClient.getActiveDescriptors((s, args) =>
			//{
			//	System.Diagnostics.Debug.Write(args.response.ToString());
			//});

			WindowReady?.Invoke(this, EventArgs.Empty);
		}

		private void Logger_OnLog(object sender, JObject e)
		{
			Application.Current?.Dispatcher.Invoke(() =>
			{
				LogsTextBox.Text += e + "\n";
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

		private async void LinkToGroup_Click(object sender, RoutedEventArgs e)
		{
			//FDC3 Usage example
			//joinChannel
			await FSBL.Clients.FDC3Client.DesktopAgentClient.JoinUserChannel("group1");
		}

		private async Task SaveStateAsync()
		{
			try
			{
				await FSBL.Clients.WindowClient?.SetComponentState(new JObject
				{
					["field"] = "symbol",
					["value"] = DataToSend.TextBox.Text
				});
			}
			catch (ApplicationException e)
			{
				FSBL.Clients.Logger.Warn(new JToken[] { "WPFExample SaveState Warn", e.Message, e.StackTrace });
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "WPFExample SaveState Error", e.Message, e.StackTrace });
			}
		}

		private async void UnLinkFromGroup_Click(object sender, RoutedEventArgs e)
		{
			//FDC3 Usage Example
			//leaveCurrentChannel
			await FSBL.Clients.FDC3Client.DesktopAgentClient.LeaveCurrentChannel();
			FSBL.Clients.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: leaveCurrentChannel.", "" });
		}

		private async void UpdateDisplayData()
		{
			try
			{
				var state = await FSBL.Clients.WindowClient?.GetComponentState(
					new JObject { ["field"] = "symbol" }
				);
				string symbolTxt = state?["response"] == null ? null : state?["response"]?.ToString();
				if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
				{
					_ = FSBL.getDispatcher().Invoke(async delegate //main thread
					{
						DataToSend.TextBox.Text = symbolTxt;
						DroppedData.Content = symbolTxt;
						DroppedDataSource.Content = "via component state";
						await SaveStateAsync();
					});
				}
				else
				{
					//Get SpawnData if no previous state
					var spawnData = await FSBL.Clients.WindowClient?.GetSpawnData();
					_ = FSBL.getDispatcher().Invoke(async delegate //main thread
					{
						symbolTxt = spawnData == null ? null : spawnData?["symbol"]?.ToString();
						if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
						{
							DataToSend.TextBox.Text = symbolTxt;
							DroppedData.Content = symbolTxt;
							DroppedDataSource.Content = "via SpawnData";
						}
						else
						{
							DataToSend.TextBox.Text = "MSFT";
							DroppedData.Content = "MSFT";
							DroppedDataSource.Content = "via default value";
						}
						await SaveStateAsync();
					});
				}
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger?.Error(new JToken[] { "WPFExample UpdateDisplayData Error", e.Message, e.StackTrace });
			}
		}

		private void Publish_Click(object sender, RoutedEventArgs e)
		{
			//set state on click
			FSBL.getDispatcher().Invoke(async delegate //main thread
			{
				DroppedData.Content = DataToSend.TextBox.Text;
				await SaveStateAsync();
			});

			//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
			// This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
			FSBL.Clients.RouterClient.Publish("Finsemble.TestWPFPubSubSymbol", new JObject
			{
				["symbol"] = DataToSend.TextBox.Text
			});
		}


		private void Subscribe_to_pubsub()
		{
			FSBL.Clients.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
			{
				try
				{
					if (state.response != null)
					{
						var pubSubData = (JObject)state.response;
						FSBL.getDispatcher().Invoke(async delegate //main thread
						{
							// The initial publish will always be an empty object.
							// Therefore, we need these null operators to handle that case.
							var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
							if (theData != null)
							{
								DataToSend.TextBox.Text = theData;
								DroppedData.Content = theData;
								DroppedDataSource.Content = "via PubSub";

								await SaveStateAsync();
							}
						});
					}
				}
				catch (Exception ex)
				{
					FSBL.Clients.Logger.Error(new JToken[] { "WPFExample Subscribe_to_Publish Error", ex.Message, ex.StackTrace });
				}
			});
		}

		/// <summary>
		/// check if the component able to receive the context
		/// </summary>
		private async Task<bool> ShouldSendContextToComponent(IContext context, string componentName)
		{
			var apps = (await FSBL.Clients.ConfigClient.Get(new[] { "finsemble", "apps" }))?.response as JArray;
			var appConfig = apps.FirstOrDefault(x => x is JObject app && app?["appId"]?.ToString() == componentName) as JObject;
			var intents = appConfig?["interop"]?["intents"]?["listensFor"]?.Children();

			if (intents == null) return false;

			foreach (var intent in intents)
			{
				if (intent?.First?["contexts"]?.ToString().Contains(context.Type) == true)
				{
					return true;
				}
			}

			return false;
		}
	}
}
