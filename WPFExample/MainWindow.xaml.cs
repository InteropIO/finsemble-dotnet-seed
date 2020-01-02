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

		private bool finsembleRequestedClose = false;

		private void SpawnComponent_Click(object sender, RoutedEventArgs e)
		{
			string componentName = ComponentSelect.SelectedValue.ToString();
			FSBL.RPC("LauncherClient.spawn", new List<JToken> {
				componentName,
				new JObject { ["addToWorkspace"] = true }
			}, (s, a) => { });
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
            //set state on click
            Application.Current.Dispatcher.Invoke(delegate //main thread
            {
                DroppedData.Content = DataToSend.Text;
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
				FinsembleHeader.SetActiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3C4C58")));
				FinsembleHeader.SetInactiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303D47")));
				FinsembleHeader.SetButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#005BC5")));
				FinsembleHeader.SetInactiveButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#004BA3")));
				FinsembleHeader.SetCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D30E2D")));
				FinsembleHeader.SetInactiveCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#D30E2D")));
				FinsembleHeader.SetDockingButtonDockedBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#005BC5")));
				FinsembleHeader.SetTitleForeground(new SolidColorBrush(Colors.White));
				FinsembleHeader.SetButtonForeground(new SolidColorBrush(Colors.White));

				FinsembleHeader.SetButtonFont(null, 14, FontStyles.Normal, FontWeights.Normal);
				FinsembleHeader.SetTitleFont(null, 14, FontStyles.Normal, FontWeights.Normal);

				FSBL.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				// Receivers for dropped data.
				FSBL.DragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
				{
				new KeyValuePair<string, EventHandler<FinsembleEventArgs>>("symbol", (s, args) =>
				{
					var data = args.response["data"]?["symbol"]?["symbol"];
					if(data != null)
					{
                        Application.Current.Dispatcher.Invoke((Action)delegate //main thread
                        {
                            DroppedData.Content = data.ToString();
                            DataToSend.Text = data.ToString();
							DroppedDataSource.Content = "via Drag and Drop";
							SaveState();
                        });
					};
				})
				});

                string fieldName = "finsemble.components." + FSBL.componentType;
                FSBL.ConfigClient.GetValue(new JObject { ["field"] = fieldName }, (routerClient, response) =>
                {
                    if (response.error != null)
                    {
                        Logger.Error(response.error);
                        return;
                    }

                    bool? alwaysOnTopIconConfigValue = (bool?)response.response?["data"]?["foreign"]?["components"]?["Window Manager"]?["alwaysOnTopIcon"];
                    bool showAlwaysOnTopButton = false;
                    if (alwaysOnTopIconConfigValue != null)
                    {
                        showAlwaysOnTopButton = (bool) alwaysOnTopIconConfigValue;
                    }
                    FinsembleHeader.setAlwaysOnTopButton(showAlwaysOnTopButton);
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

                FSBL.LinkerClient.LinkToChannel("group2", null, (s, a) => { });
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

			//listen for window close requests from FInsemble so we can differentiate user and system close requests
			/*string closeTopic = "WindowService-Event-" + FSBL.windowName + "-close-requested";
			FSBL.RouterClient.AddListener(closeTopic, (s, args) =>
			{
				finsembleRequestedCloseAt = DateTime.UtcNow;
			});*/

			//wait for the window to come up
			FSBL.RouterClient.AddListener("WindowService-Event-" + FSBL.windowName + "-ready", (t, arguments) =>
			{
				//Register an event listener to pause Finsemble during workspace switch/shutdown/restart events until this listener responds
				var guid = DateTime.UtcNow + " " + FSBL.windowName;
				FSBL.RouterClient.Query("WindowService-Request-addEventListener", new JObject
				{
					["windowIdentifier"] = FSBL.WindowClient.windowIdentifier,
					["eventName"] = "close-requested",
					["guid"] = guid
				}, (s, args) => {

				});

				//When a close-requested event fires, log the timestamp abd then publish on the interrupt channel to let FInsemble know it can continue 
				string closeTopic = "WindowService-Event-" + FSBL.windowName + "-close-requested";
				FSBL.RouterClient.AddListener(closeTopic, (s, args) =>
				{
					finsembleRequestedClose = true;
					FSBL.RouterClient.Publish("Finsemble.Event.Interrupt." + guid, new JObject
					{
						["delayed"] = false
					});
				});
			});

		}

		/// <summary>
		/// Example window close handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			//consider yielding here to let the message arrive?
			if (finsembleRequestedClose)
			{
				finsembleRequestedClose = false;
				if (MessageBox.Show("Finsemble is requesting that this app close, proceed?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
				{
					// Cancel Closing
					e.Cancel = true;
					return;
				}
			}
			else
			{
				if (MessageBox.Show("Close Application?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
				{
					// Cancel Closing
					e.Cancel = true;
					return;
				}
			}
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
                        if (state.response != null)
                        {
                            var symbol = (JValue)state.response;
                            if (symbol != null)
                            {
                                var symbolTxt = symbol?.ToString();
                                if (!string.IsNullOrEmpty(symbolTxt))
                                {
                                    Application.Current.Dispatcher.Invoke(delegate //main thread
                                    {
                                        DataToSend.Text = symbolTxt;
                                        DroppedData.Content = symbolTxt;
										DroppedDataSource.Content = "via component state";
									});
                                }
                            }
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
