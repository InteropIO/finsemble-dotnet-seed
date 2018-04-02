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

namespace FinsembleWPFDemo
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
                "Advanced Chart",
                new JObject { }
            }, (s, a) => { });
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            FSBL.RPC("LinkerClient.publish", new List<JToken>
            {
                new JObject {
                    ["dataType"] = "symbol",
                    ["data"] = DataToSend.Text
                }
            }, (s, a) => { });
        }

        public MainWindow(string[] args)
        {
            FSBL = new Finsemble(args, this);
            FSBL.Connect();
            FSBL.Connected += Bridge_Connected;
        }

        private void Bridge_Connected(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(delegate //main thread
            {
                // Initialize this Window and show it
                InitializeComponent();
                FinsembleHeader.setBridge(FSBL);

                FSBL.dragAndDropClient.SetScrim(Scrim);

                FSBL.dragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
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
                        });
                    };
                })
                });

                FSBL.dragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
                {
                new KeyValuePair<string, DragAndDropClient.emitter>("symbol", () =>
                {
                    return new JObject
                    {
                        ["symbol"] = DataToSend.Text,
                        ["description"] = "Symbol " + DataToSend.Text
                    };
                })
                });

                this.Show();

            });

            FSBL.RPC("LinkerClient.subscribe", new List<JToken>
            {
                "symbol"
            }, (error, response) =>
            {
                Application.Current.Dispatcher.Invoke(delegate //main thread
                {
                    DataToSend.Text = response?["data"]?.ToString();
                    DroppedData.Content = response?["data"]?.ToString();
                });
            });
        }
    }
}
