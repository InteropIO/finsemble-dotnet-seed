using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace WPFExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Finsemble FSBL;

        private void SpawnChart_Click(object sender, RoutedEventArgs e)
        {
            FSBL.RPC("LauncherClient.spawn", new List<JToken> {
                "Welcome Component",
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
            FSBL.Connect();
            FSBL.Connected += Finsemble_Connected;
        }

        private void Finsemble_Connected(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate //main thread
            {
                // Initialize this Window and show it
                InitializeComponent(); // Initialize after Finsemble is connected
                FinsembleHeader.SetBridge(FSBL); // The Header Control needs a connected finsemble instance

                //Styling the Finsemble Header
                /*
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
                */

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

                //Programmatically link to a colour channel
                //FSBL.LinkerClient.LinkToChannel("group2", null, (s, a) => { });

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

            //restore state if one exists
            GetState();

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

        private void GetState()
        {
            FSBL.WindowClient.GetComponentState(
                new JObject { ["field"] = "symbol" },
                delegate (object s, FinsembleEventArgs state)
                {
                    try
                    {
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
