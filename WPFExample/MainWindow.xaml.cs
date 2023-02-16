using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Media;
using ChartIQ.Finsemble.DragAndDrop;
using ChartIQ.Finsemble.Router;
using ChartIQ.Finsemble.TitlebarService.Models;
using ChartIQ.Finsemble.FDC3.Types;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Interop;

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

				if (FSBL.FDC3Client is object)
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

					var appId = await FSBL.FDC3Client.DesktopAgentClient.Open(componentName, context);

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
					FSBL.LauncherClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
				}
			}
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.FDC3Client is object)
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

				FSBL.FDC3Client.DesktopAgentClient.Broadcast(context);
			}
			else
			{
				// Use Default Linker
				FSBL.LinkerClient?.Publish(new JObject
				{
					["dataType"] = "symbol",
					["data"] = DataToSend.TextBox.Text
				});
			}

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
			this.AllowsTransparency = true;
			this.WindowStyle = WindowStyle.None;

			// Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			new WindowInteropHelper(this).EnsureHandle();
			
			FSBL = new Finsemble(args, this); // Finsemble needs the command line arguments to connect and also this Window to manage snapping, docking etc.
			FSBL.Connected += Finsemble_Connected;
			// For authentication in the InteropService use the appId from appd.json and static auth token
			FSBL.Connect("Finsemble WPF Demo", JWK);
		}

		private void Finsemble_Connected(object sender, EventArgs e)
		{
			this.Dispatcher.Invoke(delegate //main thread
			{
				// Initialize this Window and show it
				InitializeComponent(); // Initialize after Finsemble is connected
				FinsembleHeader.SetBridge(FSBL); // The Header Control needs a connected finsemble instance

				//Styling the Finsemble Header

				FinsembleHeader.GetHandlingService().ActiveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F"));
				FinsembleHeader.GetHandlingService().InactiveBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F"));
				FinsembleHeader.GetHandlingService().ButtonHoverBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4"));
				FinsembleHeader.GetHandlingService().InactiveButtonHoverBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4"));
				FinsembleHeader.GetHandlingService().CloseButtonHoverBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666"));
				FinsembleHeader.GetHandlingService().InactiveCloseButtonHoverBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666"));
				FinsembleHeader.GetHandlingService().DockingButtonDockedBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4"));
				FinsembleHeader.GetHandlingService().TitleForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0"));
				FinsembleHeader.GetHandlingService().ButtonForeground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0"));

				FinsembleHeader.GetHandlingService().ButtonFont = new TitlebarFontConfiguration()
				{
					FontFamily = null,
					FontSize = 8,
					FontStyle = FontStyles.Normal,
					FontWeight = FontWeights.Normal
				};
				FinsembleHeader.GetHandlingService().TitleFont = new TitlebarFontConfiguration()
				{
					FontFamily = null,
					FontSize = 12,
					FontStyle = FontStyles.Normal,
					FontWeight = FontWeights.SemiBold
				};

				//Set window title
				FinsembleHeader.GetHandlingService().Title = "WPF Example Component";

				FSBL.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				// Receivers for dropped data.
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
							}
							FSBL.getDispatcher().Invoke((Action)delegate //main thread
							{
								FSBL.getDispatcher().Invoke((Action)async delegate //main thread
								{
									DroppedData.Content = data.ToString();
									DataToSend.TextBox.Text = data.ToString();
									DroppedDataSource.Content = "via Drag and Drop";
									await SaveStateAsync();
								});
							});
						}
					})
				});

				// Emitters for data that can be dragged using the drag icon.
				FSBL.DragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
				{
					new KeyValuePair<string, DragAndDropClient.emitter>("symbol", () =>
					{
						//set state on drag so correct symbol is displayed
						FSBL.getDispatcher().Invoke(async delegate //main thread
						{
							DroppedData.Content = DataToSend.TextBox.Text;
							await SaveStateAsync();
						});
						return new JObject
						{
							["symbol"] = DataToSend.TextBox.Text,
							["description"] = "Symbol " + DataToSend.TextBox.Text
						};
					})
				});

				//Subscribe to a PubSub topic
				//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
				//     This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
				Subscribe_to_pubsub();

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
							FSBL.getDispatcher().Invoke(delegate //main thread
							{
								ComponentSelect.ItemsComboBox.Items.Add(property.Name);
							});
						}
					}
				});

				//FSBL.LinkerClient?.LinkToChannel("group2", null, (s, a) => { });
				//restore state if one exists
				UpdateDisplayData();
				this.Show();
			});

			if (FSBL.FDC3Client is object)
			{
				//FDC3 Usage example	
				FSBL.getDispatcher().Invoke(delegate //main thread	
				{
					//	FDC3Label.Visibility = Visibility.Visible;
				});

				//Context handler
				ContextHandler contextHandler = (context, metadata) =>
				{
					FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, context received by contextHandler.", context.Value });
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
				FSBL.FDC3Client.DesktopAgentClient.AddContextListener("fdc3.instrument", contextHandler);

				IntentHandler intentHandler = async (context, metadata) =>
				{
					FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: context received by intentHandler.", context.Value });
					if (context.Type !=null && context.Type.Equals("fdc3.instrument"))
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
				FSBL.FDC3Client.DesktopAgentClient.AddIntentListener("ViewChart", intentHandler);
			}
			else
			{
				//Use Default linker
				//Subscribe to Finsemble Linker Channels
				FSBL.LinkerClient?.Subscribe("symbol", (error, response) =>
				{
					FSBL.getDispatcher().Invoke(async delegate //main thread
					{
						DataToSend.TextBox.Text = response.response?["data"]?.ToString();
						DroppedData.Content = response.response?["data"]?.ToString();
						DroppedDataSource.Content = "via Linker";
						await SaveStateAsync();
					});
				});
			}

			FSBL.Logger.OnLog += Logger_OnLog;
			FSBL.Logger.System.OnLog += Logger_OnLog;
			FSBL.Logger.Perf.OnLog += Logger_OnLog;

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
			if (FSBL.LinkerClient != null)
			{
				FSBL.LinkerClient.LinkToChannel("group1", null, (s, r) =>
				{
					FSBL.Logger.Log(new JToken[] { "Link to Group1", r.response });
				});
			}
			else
			{
				//FDC3 Usage example
				//joinChannel
				await FSBL.FDC3Client.DesktopAgentClient.JoinUserChannel("group1");
			}
		}

		private async Task SaveStateAsync()
		{
			try
			{
				await FSBL.WindowClient.SetComponentState(new JObject
				{
					["field"] = "symbol",
					["value"] = DataToSend.TextBox.Text
				});
			}
			catch (ApplicationException e)
			{
				FSBL.Logger.Warn(new JToken[] { "WPFExample SaveState Warn", e.Message, e.StackTrace });
			}
			catch (Exception e)
			{
				FSBL.Logger.Error(new JToken[] { "WPFExample SaveState Error", e.Message, e.StackTrace });
			}
		}

		private async void UnLinkFromGroup_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.LinkerClient != null)
			{
				FSBL.LinkerClient.UnlinkFromChannel("group1", null, (s, r) =>
				{
					FSBL.Logger.Log(new JToken[] { "Unlinked from Group1", r.response });
				});
			}
			else
			{
				//FDC3 Usage Example
				//leaveCurrentChannel
				await FSBL.FDC3Client.DesktopAgentClient.LeaveCurrentChannel();
				FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: leaveCurrentChannel.", "" });
			}
		}


		private void UpdateDisplayData()
		{
			FSBL.WindowClient.GetComponentState(
				new JObject { ["field"] = "symbol" },
				delegate (object s, FinsembleEventArgs state)
				{
					try
					{
						string symbolTxt = state.response == null ? null : state.response?.ToString();
						if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
						{
							FSBL.getDispatcher().Invoke(async delegate //main thread
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
							FSBL.WindowClient.GetSpawnData((sender, r) =>
							{
								FSBL.getDispatcher().Invoke(async delegate //main thread
								{
									symbolTxt = r.response == null ? null : r.response?["symbol"]?.ToString();
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
							});
						}
					}
					catch (Exception e)
					{
						FSBL.Logger.Error(new JToken[] { "WPFExample UpdateDisplayData Error", e.Message, e.StackTrace });
					}
				}
			);
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
			FSBL.RouterClient.Publish("Finsemble.TestWPFPubSubSymbol", new JObject
			{
				["symbol"] = DataToSend.TextBox.Text
			});
		}


		private void Subscribe_to_pubsub()
		{
			FSBL.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
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
					FSBL.Logger.Error(new JToken[] { "WPFExample Subscribe_to_Publish Error", ex.Message, ex.StackTrace });
				}
			});
		}
	}
}
