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
                            SaveState();
                        });
					};
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

                    SaveState();
                });
			});

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
                        if (state.response != null)
                        {
                            var symbol = (JValue)state.response;//?["data"];
                            if (symbol != null)
                            {
                                var symbolTxt = symbol?.ToString();
                                if (!string.IsNullOrEmpty(symbolTxt))
                                {
                                    Application.Current.Dispatcher.Invoke(delegate //main thread
                                    {
                                        DataToSend.Text = symbolTxt;
                                        DroppedData.Content = symbolTxt;
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
    }
}
