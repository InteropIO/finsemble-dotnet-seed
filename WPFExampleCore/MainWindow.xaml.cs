using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Finsemble.Core.Clients.FDC3.Types;
using Finsemble.Core.Clients.Router;
using Finsemble.WPF.Core;
using Finsemble.WPF.Core.Clients;
using Finsemble.WPF.Core.TitlebarService.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace WPFExampleCore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private FinsembleWPF FSBL;
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

		public MainWindow(string[] args)
		{
			//Disconnect Finsemble properly if Unhandled Exception happens. Otherwise you can disrupt your workspace.
			Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;

			//Example how to handle window closing event
			Closing += MainWindow_Closing;

			//Ensure that your window has been created (so that its window handle exists) before connecting to Finsemble.
			var handle = new WindowInteropHelper(this).EnsureHandle();
			FSBL = new FinsembleWPF(this, handle.ToString("X"), args); // Finsemble needs the command line arguments to connect and also this Window handle to manage snapping, docking etc.
			FSBL.Connected += Finsemble_Connected;
			FSBL.Disconnected += FSBL_Disconnected;
			// For authentication in the InteropService use the appId from appd.json and static auth token
			var connectTask = FSBL.Connect("Finsemble WPF Demo Core", JWK);
		}

		private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (FSBL?.IsConnected == true)
			{
				FSBL.Clients.Logger.Log($"Main window is closing.");
			}
		}

		private void FSBL_Disconnected(object sender, EventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Finsemble was disconnected. This application will be close");
			Close();
		}

		private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
		{
			if (FSBL?.IsConnected == true)
			{
				FSBL.Clients.Logger.Error($"Application is going to shut down. \nUnhandledException: {e.Exception.Message}");
			}

			FSBL?.Dispose();
			Close();
		}

		#region Finsemble settings

		private void Finsemble_Connected(object sender, EventArgs e)
		{
			Application.Current.Dispatcher.Invoke(async delegate //main thread
			{
				// Initialize after Finsemble is connected
				InitializeComponent();

				// The Header Control needs a connected finsemble instance
				FinsembleHeader.SetBridge(FSBL);

				#region Styling the Finsemble Header

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
				FinsembleHeader.GetHandlingService().Title = "WPF Example Core Component";

				#endregion

				#region Drag And Drop
				FSBL.Clients.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				// Receivers for dropped data.
				FSBL.Clients.DragAndDropClient.AddReceivers(new List<KeyValuePair<string, EventHandler<FinsembleEventArgs>>>()
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
				FSBL.Clients.DragAndDropClient.SetEmitters(new List<KeyValuePair<string, DragAndDropClient.emitter>>()
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
				#endregion

				//Subscribe to a PubSub topic
				//N.B. You must add a PubSub responder before publishing or subscribing to any topic that doesn't start with 'Finsemble'
				//     This is not currently supported in the .Net RouterClient implementation and will need to done in a Finsemble HTML5 service
				SubscribeToPubSub();

				SetUpFinsembleComponents();
				await UpdateDisplayData();
				this.Show();
			});

			#region communication through channels
			if (FSBL.Clients.Fdc3Client is object)
			{
				FDC3Label.Visibility = Visibility.Visible;
				ContextHandler contextHandler = (context) =>
				{
					FSBL.Clients.Logger.Log(new JToken[] { "WPF Core FDC3 Usage Example, context received by contextHandler.", context.Value });
					if (context.Type.Equals("fdc3.instrument"))
					{
						Application.Current.Dispatcher.Invoke(async delegate //main thread
						{
							DataToSend.TextBox.Text = context.Id?["ticker"]?.ToString();
							DroppedData.Content = context.Id?["ticker"]?.ToString();
							DroppedDataSource.Content = "context shared via FDC3";
							await SaveStateAsync();
						});
					}
				};
				FSBL.Clients.Fdc3Client.DesktopAgentClient.AddContextListener("fdc3.instrument", contextHandler);

				ContextHandler intentHandler = (context) =>
				{
					FSBL.Clients.Logger.Log(new JToken[] { "WPF Core FDC3 Usage Example: context received by intentHandler.", context.Value });
					if (context.Type != null && context.Type.Equals("fdc3.instrument"))
					{
						Application.Current.Dispatcher.Invoke(async delegate //main thread
						{
							DataToSend.TextBox.Text = context.Id?["ticker"]?.ToString();
							DroppedData.Content = context.Id?["ticker"]?.ToString();
							DroppedDataSource.Content = "context shared via FDC3 intent";
							await SaveStateAsync();
						});
					}
				};
				FSBL.Clients.Fdc3Client.DesktopAgentClient.AddIntentListener("ViewChart", intentHandler);
			}
			else
			{
				//Use Default linker
				//Subscribe to Finsemble Linker Channels
				FSBL.Clients.LinkerClient?.Subscribe("symbol", (error, response) =>
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
			#endregion

			#region Logging
			FSBL.Clients.Logger.OnLog += Logger_OnLog;
			FSBL.Clients.Logger.System.OnLog += Logger_OnLog;
			FSBL.Clients.Logger.Perf.OnLog += Logger_OnLog;

			// Example: Logging to the Finsemble Central Console
			/*
			FSBL.Logger.Error(new JToken[] {"Error Test"});

			FSBL.Logger.Log(new JToken[] {"Log Test"});

			FSBL.Logger.Debug(new JToken[] {"Debug Test"});

			FSBL.Logger.Info(new JToken[] {"Info Test"});

			FSBL.Logger.Verbose(new JToken[] {"Verbose Test"});
			*/
			#endregion

		}

		private async Task SaveStateAsync()
		{
			try
			{
				await FSBL.Clients.WindowClient.SetComponentState(new JObject
				{
					["field"] = "symbol",
					["value"] = DataToSend.TextBox.Text
				});
			}
			catch (ApplicationException e)
			{
				FSBL.Clients.Logger.Warn(new JToken[] { "WPFExampleCore SaveState Warn", e.Message, e.StackTrace });
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "WPFExampleCore SaveState Error", e.Message, e.StackTrace });
			}
		}

		private void SubscribeToPubSub()
		{
			FSBL.Clients.RouterClient.Subscribe("Finsemble.TestWPFPubSubSymbol", delegate (object s, FinsembleEventArgs state)
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
					FSBL.Clients.Logger.Error(new JToken[] { "WPFExampleCore Subscribe_to_Publish Error", ex.Message, ex.StackTrace });
				}
			});
		}

		private async void SetUpFinsembleComponents()
		{
			var response = await FSBL.Clients.ConfigClient.GetValue(new JObject { ["field"] = "finsemble.components" });
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
		}

		private async Task UpdateDisplayData()
		{
			var state = await FSBL.Clients.WindowClient.GetComponentState(new JObject { ["field"] = "symbol" });
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
					FSBL.Clients.WindowClient.GetSpawnData((sender, r) =>
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
						});
					});
				}
				await SaveStateAsync();
			}
			catch (Exception e)
			{
				FSBL.Clients.Logger.Error(new JToken[] { "WPFExample UpdateDisplayData Error", e.Message, e.StackTrace });
			}
		}

		private void Logger_OnLog(object sender, JObject e)
		{
			Application.Current?.Dispatcher.Invoke(() =>
			{
				LogsTextBox.Text += e + "\n";
			});
		}
		#endregion

		#region UI Events

		private async void SpawnComponent_Click(object sender, RoutedEventArgs e)
		{
			object selected = ComponentSelect.ItemsComboBox.SelectedValue;
			if (selected != null)
			{
				string componentName = selected.ToString();

				if (FSBL.Clients.Fdc3Client is object)
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

					var targetApp = new TargetApp() { Name = componentName };
					await FSBL.Clients.Fdc3Client.DesktopAgentClient.Open(targetApp, context);
				}
				else
				{
					FSBL.Clients.LauncherClient.Spawn(componentName, new JObject { ["addToWorkspace"] = true }, (s, a) => { });
				}
			}
		}

		private void Send_Click(object sender, RoutedEventArgs e)
		{
			if (FSBL.Clients.Fdc3Client is object)
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

				FSBL.Clients.Fdc3Client.DesktopAgentClient.Broadcast(context);
			}
			else
			{
				// Use Default Linker
				FSBL.Clients.LinkerClient?.Publish(new JObject
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

		private void DataToSend_EnterKeyPressed(object sender, EventArgs e)
		{
			Application.Current.Dispatcher.Invoke(delegate //main thread
			{
				DroppedData.Content = DataToSend.TextBox.Text;
				DroppedDataSource.Content = "via Text input";
				return Task.CompletedTask;
			});
		}

		#endregion

	}
}
