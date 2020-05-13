using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble;
using log4net;
using Newtonsoft.Json.Linq;

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
		private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		private Finsemble FSBL;

		private void SpawnComponent_Click(object sender, RoutedEventArgs e)
		{
			object selected = ComponentSelect.SelectedValue;
			if (selected != null)
			{
				string componentName = selected.ToString();
				FSBL.RPC("LauncherClient.spawn", new List<JToken> {
					componentName,
					new JObject { ["addToWorkspace"] = true }
				}, (s, a) => { });
			}
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
			//set state on click
			Application.Current.Dispatcher.Invoke(delegate //main thread
			{
				DroppedData.Content = DataToSend.Text;
				DroppedDataSource.Content = "via Linker";
				SaveState();
			});

			FSBL.RPC("LinkerClient.publish", new List<JToken>
			{
				new JObject {
					["dataType"] = "symbol",
					["data"] = DataToSend.Text
		}
			}, (s, a) => { });
		}

		/// <summary>
		/// The MainWindow is created by the App so that we can get command line arguments passed from Finsemble.
		/// </summary>
		/// <param name="args"></param>
		public MainWindow(string[] args)
		{

			// Trigger actions on close when requested by Finsemble, e.g.:
			this.Closing += MainWindow_Closing;

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
				FinsembleHeader.SetActiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F")));
				FinsembleHeader.SetInactiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F")));
				FinsembleHeader.SetButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetInactiveButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666")));
				FinsembleHeader.SetInactiveCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666")));
				FinsembleHeader.SetDockingButtonDockedBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetTitleForeground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0")));
                FinsembleHeader.SetButtonForeground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0")));

                FinsembleHeader.SetButtonFont(null, 8, FontStyles.Normal, FontWeights.Normal);
				FinsembleHeader.SetTitleFont(null, 12, FontStyles.Normal, FontWeights.SemiBold);

				//Set window title
				FinsembleHeader.SetTitle("WPF Example Component");

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
                                Application.Current.Dispatcher.Invoke((Action)delegate //main thread
							    {
                                    DroppedData.Content = data.ToString();
                                    DataToSend.Text = data.ToString();
                                    DroppedDataSource.Content = "via Drag and Drop";
                                    SaveState();
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
						Application.Current.Dispatcher.Invoke(delegate //main thread
						{
							DroppedData.Content = DataToSend.Text;
							SaveState();
						});
						return new JObject
						{
							["symbol"] = DataToSend.Text,
							["description"] = "Symbol " + DataToSend.Text
						};
					})
				});


				FSBL.ConfigClient.GetValue(new JObject { ["field"] = "finsemble.components" }, (routerClient, response) =>
				{
					if (response.error != null)
					{
						Logger.Error(response.error);
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
								ComponentSelect.Items.Add(property.Name);
							});
						}
					}
				});

				//FSBL.LinkerClient.LinkToChannel("group2", null, (s, a) => { });
				//restore state if one exists
				UpdateDisplayData();
				this.Show();
			});

			// Subscribe to Finsemble Linker Channels
			FSBL.RPC("LinkerClient.subscribe", new List<JToken>
			{
				"symbol"
			}, (error, response) =>
			{
				Application.Current.Dispatcher.Invoke(delegate //main thread
				{
					DataToSend.Text = response?["data"]?.ToString();
					DroppedData.Content = response?["data"]?.ToString();
					DroppedDataSource.Content = "via Linker";
					SaveState();
				});
			});

			//Subscribe to a PubSub topic
			//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
			//     This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
			Subscribe_to_pubsub();

			// Logging to the Finsemble Central Console
			/*FSBL.RPC("Logger.error", new List<JToken> {
				"Error Test"
			});

			FSBL.RPC("Logger.log", new List<JToken> {
				"Log Test"
			});

			FSBL.RPC("Logger.debug", new List<JToken> {
				"Debug Test"
			});

			FSBL.RPC("Logger.info", new List<JToken> {
				"Info Test"
			});

			FSBL.RPC("Logger.verbose", new List<JToken> {
				"Verbose Test"
			});
			*/

			////Sample code to execute LauncherClient.getActiveDescriptors()
			//FSBL.LauncherClient.getActiveDescriptors((s, args) =>
			//{
			//	System.Diagnostics.Debug.Write(args.response.ToString());
			//});
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
			FSBL.LinkerClient.LinkToChannel("group1", null, (s, r) => { });
		}

		private void SaveState()
		{
			try
			{
				FSBL.WindowClient.SetComponentState(new JObject
				{
					["field"] = "symbol",
					["value"] = DataToSend.Text
				}, delegate (object s, FinsembleEventArgs e) { });
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
			}

		}

		private void UpdateDisplayData()
		{
			FSBL.WindowClient.GetComponentState(
				new JObject { ["field"] = "symbol" },
				delegate (object s, FinsembleEventArgs state)
				{
					try {
						string symbolTxt = state.response == null ? null : state.response?.ToString();
						if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
						{
							Application.Current.Dispatcher.Invoke(delegate //main thread
							{
								DataToSend.Text = symbolTxt;
								DroppedData.Content = symbolTxt;
								DroppedDataSource.Content = "via component state";
							});
						}
						else
						{
							//Get SpawnData if no previous state
							FSBL.WindowClient.getSpawnData((sender, r) => {
								Application.Current.Dispatcher.Invoke(delegate //main thread
								{
									symbolTxt = r.response == null ? null : r.response?["symbol"]?.ToString();
									if (!string.IsNullOrEmpty(symbolTxt) && !symbolTxt.Equals("{}"))
									{
										DataToSend.Text = symbolTxt;
										DroppedData.Content = symbolTxt;
										DroppedDataSource.Content = "via SpawnData";
									}
									else
									{
										DataToSend.Text = "MSFT";
										DroppedData.Content = "MSFT";
										DroppedDataSource.Content = "via default value";
									}
									SaveState();
								});
							});
						}
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message);
					}
				}
			);
		}

		private void Publish_Click(object sender, RoutedEventArgs e)
		{
			//set state on click
			Application.Current.Dispatcher.Invoke(delegate //main thread
			{
				DroppedData.Content = DataToSend.Text;
				SaveState();
			});

			//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
			// This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
			FSBL.RouterClient.Publish("Finsemble.TestWPFPubSubSymbol", new JObject {
					["symbol"] = DataToSend.Text
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
						Application.Current.Dispatcher.Invoke(delegate //main thread
						{
							// The initial publish will always be an empty object.
							// Therefore, we need these null operators to handle that case.
							var theData = ((JValue)pubSubData?["data"]?["symbol"])?.ToString();
							if (theData != null) {
								DataToSend.Text = theData;
								DroppedData.Content = theData;
								DroppedDataSource.Content = "via PubSub";

								SaveState();
							}
						});
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
