using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble.Models;

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

		private Finsemble FSBL;

		private void SpawnComponent_Click(object sender, RoutedEventArgs e)
		{
			object selected = ComponentSelect.ItemsComboBox.SelectedValue;
			if (selected != null)
			{
				string componentName = selected.ToString();

				if (FSBL.FDC3Client is object)
				{
					//FDC3 Usage example 
					//open
					FSBL.FDC3Client.fdc3.open(componentName, new JObject
					{
						["type"] = "fdc3.instrument",
						["name"] = DataToSend.TextBox.Text,
						["id"] = new JObject
						{
							["ticker"] = DataToSend.TextBox.Text
						}
					}, (s, args) => { });

					//Intent 
					//FSBL.FDC3Client.fdc3.raiseIntent("ViewChart", new JObject
					//{
					//	["type"] = "fdc3.instrument",
					//	["name"] = DataToSend.TextBox.Text,
					//	["id"] = new JObject
					//	{
					//		["ticker"] = DataToSend.TextBox.Text
					//	}
					//}, (s, args) => { });

					//FSBL.FDC3Client.fdc3.findIntent("ViewChart", (s, intent) =>
					//{
					//	FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntent.", intent });
					//});

					//FSBL.FDC3Client.fdc3.findIntent("ViewChart", new JObject
					//	{
					//		["type"] = "fdc3.instrument"
					//	}, (s, intent) =>
					//	{
					//		FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntent.", intent });
					//	}
					//);

					//FSBL.FDC3Client.fdc3.findIntentsByContext(new JObject
					//	{
					//		["type"] = "fdc3.instrument"
					//	}, (s, intents) =>
					//	{
					//		FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, findIntentsByContext.", intents });
					//	}
					//);

					//Context
					//FSBL.FDC3Client.fdc3.getCurrentChannel((s, channel) => {
					//	channel.getCurrentContext("fdc3.instrument", (sender, context) => {
					//		FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentContext.", context });
					//	});
					//});

					//Channels
					//FSBL.FDC3Client.fdc3.getOrCreateChannel("test", (s, args) => { });

					//FSBL.FDC3Client.fdc3.getSystemChannels((s, channelList) =>
					//{
					//	foreach (IChannel channel in channelList)
					//	{
					//		FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getSystemChannels.", channel.id });
					//	}
					//});

					//FSBL.FDC3Client.fdc3.getCurrentChannel((s, channel) => {
					//	FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: getCurrentChannel.", channel.id });
					//});
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
				FSBL.FDC3Client.fdc3.broadcast(new JObject
				{
					["type"] = "fdc3.instrument",
					["name"] = DataToSend.TextBox.Text,
					["id"] = new JObject
					{
						["ticker"] = DataToSend.TextBox.Text
					}
				});
			}
			else
			{
				// Use Default Linker
				FSBL.LinkerClient.Publish(new JObject
				{
					["dataType"] = "symbol",
					["data"] = DataToSend.TextBox.Text
				});
			}

			Application.Current.Dispatcher.Invoke(async delegate //main thread
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

			//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			FSBL = new Finsemble(args, this); // Finsemble needs the command line arguments to connect and also this Window to manage snapping, docking etc.
			FSBL.Connected += Finsemble_Connected;
			FSBL.Connect();
		}

		private void Finsemble_Connected(object sender, EventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate //main thread
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
							Application.Current.Dispatcher.Invoke((Action)delegate //main thread
							{
								Application.Current.Dispatcher.Invoke((Action)async delegate //main thread
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
						Application.Current.Dispatcher.Invoke(async delegate //main thread
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
							Application.Current.Dispatcher.Invoke(delegate //main thread
							{
								ComponentSelect.ItemsComboBox.Items.Add(property.Name);
							});
						}
					}
				});

				//FSBL.LinkerClient.LinkToChannel("group2", null, (s, a) => { });
				//restore state if one exists
				UpdateDisplayData();
				this.Show();
			});

			if (FSBL.FDC3Client is object)
			{
				//FDC3 Usage example	
				Application.Current.Dispatcher.Invoke(delegate //main thread	
				{
					//	FDC3Label.Visibility = Visibility.Visible;
				});

				//Context handler
				EventHandler<JObject> contextHandler = (s, context) =>
				{
					FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example, context received by contextHandler.", context });
					if (context["type"].ToString().Equals("fdc3.instrument"))
					{
						Application.Current.Dispatcher.Invoke(async delegate //main thread
						{
							DataToSend.TextBox.Text = context?["id"]?["ticker"]?.ToString();
							DroppedData.Content = context?["id"]?["ticker"]?.ToString();
							DroppedDataSource.Content = "context shared via FDC3";
							await SaveStateAsync();
						});
					}
				};
				FSBL.FDC3Client.fdc3.addContextListener(contextHandler);
				//FSBL.FDC3Client.fdc3.addContextListener("fdc3.instrument", contextHandler);


				EventHandler<JObject> intentHandler = (s, context) =>
				{
					FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: context received by intentHandler.", context });
					if (context["type"]!=null && context["type"].ToString().Equals("fdc3.instrument"))
					{
						Application.Current.Dispatcher.Invoke(async delegate //main thread
						{
							DataToSend.TextBox.Text = context?["id"]?["ticker"]?.ToString();
							DroppedData.Content = context?["id"]?["ticker"]?.ToString();
							DroppedDataSource.Content = "context shared via FDC3 intent";
							await SaveStateAsync();
						});
					}
				};
				FSBL.FDC3Client.fdc3.addIntentListener("ViewChart", intentHandler);
			}
			else
			{
				//Use Default linker
				//Subscribe to Finsemble Linker Channels
				FSBL.LinkerClient.Subscribe("symbol", (error, response) =>
				{
					Application.Current.Dispatcher.Invoke(async delegate //main thread
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
		}

		private void Logger_OnLog(object sender, JObject e)
		{
			Application.Current.Dispatcher.Invoke(() =>
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

		private void LinkToGroup_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.FDC3Client is null)
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
				FSBL.FDC3Client.fdc3.joinChannel("group1");
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

		private void UnLinkFromGroup_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.FDC3Client is null)
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
				FSBL.FDC3Client.fdc3.leaveCurrentChannel((s, args) =>
				{
					FSBL.Logger.Log(new JToken[] { "WPF FDC3 Usage Example: leaveCurrentChannel.", args });
				});
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
							Application.Current.Dispatcher.Invoke(delegate //main thread
							{
								DataToSend.TextBox.Text = symbolTxt;
								DroppedData.Content = symbolTxt;
								DroppedDataSource.Content = "via component state";
							});
						}
						else
						{
							//Get SpawnData if no previous state
							FSBL.WindowClient.GetSpawnData((sender, r) =>
							{
								Application.Current.Dispatcher.Invoke(async delegate //main thread
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
			Application.Current.Dispatcher.Invoke(async delegate //main thread
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
						Application.Current.Dispatcher.Invoke(async delegate //main thread
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
