using DotNetBrowser.Browser;
using DotNetBrowser.Engine;
using DotNetBrowser.Logging;
using System;
using System.Diagnostics;
using System.Windows;
using DotNetBrowser.Handlers;
using DotNetBrowser.Browser.Handlers;
using DotNetBrowser.Js;
using System.Text;
using Wpf;
using DotNetBrowser.Dom;

namespace WpfTestProject
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		#region constants

		private const string BROWSERVIEW_LICENSE_KEY = "YOUR_KEY_HERE";
		private const int DEBUGGING_PORT = 9222;
		#endregion

		#region JS scripts
		private string finsembleJsAdapterSrc = "http://localhost:3375/build/finsemble/finsemble-javascript-adapter.js";
		private string fsblStartApp = "FSBLJSAdapter.startApp({appName: \"MyApp\", windowName: \"MyApp-window\"})";
		#endregion

		private IBrowser browser;
		private IEngine engine;


		public MainWindow()
		{
			this.Loaded += MainWindow_Initialized;

			InitializeComponent();
			InitializeBrowserView();
		}

		private async void MainWindow_Initialized(object sender, EventArgs e)
		{
			try
			{
				// Inject the JavaScript adapter
				browser.InjectJsHandler = new Handler<InjectJsParameters>(async args =>
				{
					// import and run Finsemble for every web page
					await args.Frame.ExecuteJavaScript($@"
						import('{finsembleJsAdapterSrc}')
						.then(module => {{ {fsblStartApp} }})
					");
				});

				var base64EncodedHtml = Convert.ToBase64String(Encoding.UTF8.GetBytes(StaticHtmlPage.Html));
				browser.Navigation.LoadUrl("data:text/html;base64," + base64EncodedHtml).Wait();

				// Display in a Browserview
				browserView.InitializeFrom(browser);

				// Call custom Finsemble functions
				IDocument document = browser.MainFrame.ExecuteJavaScript<IDocument>("document").Result;
			}
			catch (Exception exception)
			{

			}
		}

		private void Window_Closed(object sender, EventArgs e)
		{
			browser.Dispose();
			engine.Dispose();
		}

		private void InitializeBrowserView()
		{
			try
			{
				engine = EngineFactory.Create(new EngineOptions.Builder
				{
					RenderingMode = RenderingMode.HardwareAccelerated,
					LicenseKey = BROWSERVIEW_LICENSE_KEY,
					RemoteDebuggingPort = DEBUGGING_PORT,
					WebSecurityDisabled = true,
					FileAccessFromFilesAllowed = true,
				}
				.Build());
				browser = engine.CreateBrowser();

#if DEBUG
				browser.ConsoleMessageReceived += Browser_ConsoleMessageReceived;
#endif
			}
			catch (Exception exception)
			{

			}
		}

		private void Browser_ConsoleMessageReceived(object sender, DotNetBrowser.Browser.Events.ConsoleMessageReceivedEventArgs e)
		{
			Debug.Print($"{e.Level}\t{e.Message}");
		}
	}
}
