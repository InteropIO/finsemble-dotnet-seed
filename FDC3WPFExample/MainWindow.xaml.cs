using ChartIQ.Finsemble;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble.Models;

namespace FDC3WPFExample
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Finsemble FSBL;

		
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
				FinsembleHeader.GetHandlingService().Title = "FDC3 WPF Example Component";
				IntentToRaise.TextBox.Text = "ViewChart";

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
							//TODO: filter down to only FDC3 enabled components
							Application.Current.Dispatcher.Invoke(delegate //main thread
							{
								ComponentSelect.ItemsComboBox.Items.Add(property.Name);
							});
						}
					}
				});

				//restore state if one exists
				InitializeFromStateOrSpawnData();


				if (FSBL.FDC3Client is object)
				{
					//Context handler
					EventHandler<JObject> contextHandler = (s, context) =>
					{
						FSBL.Logger.Log(new JToken[] { "context received by contextHandler.", context });
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
					//To add a filtered context listener
					//FSBL.FDC3Client.fdc3.addContextListener("fdc3.instrument", contextHandler);

					//Intent handler
					EventHandler<JObject> intentHandler = (s, context) =>
					{
						FSBL.Logger.Log(new JToken[] { "context received by intentHandler.", context.ToString() });
						if (context["type"] != null && context["type"].ToString().Equals("fdc3.instrument"))
						{
							Application.Current.Dispatcher.Invoke(async delegate //main thread
							{
								string ticker = context?["id"]?["ticker"]?.ToString();
								FSBL.Logger.Log(new JToken[] { "updating state to ticker:" + ticker });
								DataToSend.TextBox.Text = ticker;
								DroppedData.Content = ticker;
								DroppedDataSource.Content = "context shared via FDC3 intent";
								await SaveStateAsync();
							});
						}
						else if (context == null)
						{
							FSBL.Logger.Log(new JToken[] { "null context received by intentHandler." });
						}
						else
						{
							FSBL.Logger.Log(new JToken[] { "unrecognized context type received by intentHandler.", context.ToString() });
						}
					};
					FSBL.FDC3Client.fdc3.addIntentListener("ViewChart", intentHandler);
				}
				else
				{
					FSBL.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
				}

				this.Show();
			});

			

			FSBL.Logger.OnLog += Logger_OnLog;
			FSBL.Logger.System.OnLog += Logger_OnLog;
			FSBL.Logger.Perf.OnLog += Logger_OnLog;
		}

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
				FSBL.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}

			Application.Current.Dispatcher.Invoke(async delegate //main thread
			{
				DroppedData.Content = DataToSend.TextBox.Text;
				DroppedDataSource.Content = "via Text entry";
				await SaveStateAsync();
			});
		}

		private void RaiseIntent_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.FDC3Client is object)
			{
				//FDC3 Usage example 
				//RaiseIntent

				JObject context = null;
				if (!String.IsNullOrEmpty(DataToSend.TextBox.Text))
				{
					context = new JObject
					{
						["type"] = "fdc3.instrument",
						["name"] = DataToSend.TextBox.Text,
						["id"] = new JObject
						{
							["ticker"] = DataToSend.TextBox.Text
						}
					};

				}

				FSBL.FDC3Client.fdc3.raiseIntent(IntentToRaise.TextBox.Text, context, (s, args) => { });
			}
			else
			{
				FSBL.Logger.Error(new JToken[] { "FDC3 Client is not enabled" });
			}

			Application.Current.Dispatcher.Invoke(async delegate //main thread
			{
				DroppedData.Content = DataToSend.TextBox.Text;
				DroppedDataSource.Content = "via Text entry";
				await SaveStateAsync();
			});
		}

		//Other FDC3 examples
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



		private void Logger_OnLog(object sender, JObject e)
		{
			//clean up stringified log messages for display
				//note error handling code needs adding to this to make it robust
			if (e["logData"] != null)
            {
				e["logData"] = JArray.Parse(e["logData"].ToString());//.Replace("\\r\\n", "\n");
			}
			string logMsg = e.ToString().Replace("\\r\\n", "\n").Replace("\\\\","\\");

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
				FSBL.Logger.Warn(new JToken[] { "SaveState Warn", e.Message, e.StackTrace });
			}
			catch (Exception e)
			{
				FSBL.Logger.Error(new JToken[] { "SaveState Error", e.Message, e.StackTrace });
			}
		}


		private async void InitializeFromStateOrSpawnData()
		{
			try
			{
				JToken state = await FSBL.WindowClient.GetComponentState(new JObject { ["field"] = "symbol" });
				string symbolTxt = state == null ? null : state.ToString();
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
				FSBL.Logger.Warn(new JToken[] { "InitializeFromStateOrSpawnData Error, it is likely no state was found", e.Message, e.StackTrace });
			}
		}
	}
}
