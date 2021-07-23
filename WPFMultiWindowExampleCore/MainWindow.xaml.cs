using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Finsemble.WPF.Core;
using Finsemble.WPF.Core.TitlebarService.Models;

namespace WPFMultiWindowExampleCore
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml with Finsemble instance
	/// </summary>
	public partial class MainWindow : Window
	{
		private FinsembleWPF FSBL { get; set; }

		public MainWindow(string[] args)
		{
			InitializeComponent();

			InitHwnd();
			ConnectToFinsemble(args);

			this.Closed += MainWindow_Closed; ;
		}

		/// <summary>
		/// Dispose of Finsemble object when window is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MainWindow_Closed(object sender, EventArgs e)
		{
			FSBL.Dispose();
		}

		/// <summary>
		/// Method needed to retrieve window handler without showing the window itself
		/// </summary>
		private void InitHwnd()
		{
			var helper = new WindowInteropHelper(this);
			helper.EnsureHandle();
		}

		/// <summary>
		/// The ConnectToFinsemble.
		/// </summary>
		/// <param name="args">Command line parameters <see cref="string"/>.</param>
		private async void ConnectToFinsemble(string[] args)
		{
			FSBL = new FinsembleWPF(this, new WindowInteropHelper(this).Handle.ToString("X"), args);
			FSBL.Connected += Finsemble_Connected;
			await FSBL.Connect();
		}

		private void Finsemble_Connected(object sender, EventArgs e)
		{
			Debug.WriteLine("FSBL connected");
			
			Application.Current.Dispatcher.Invoke(() =>
			{
				FinsembleHeader.SetBridge(FSBL); // The Header Control needs a connected finsemble instance

				////Set window title
				FinsembleHeader.GetHandlingService().Title = this.LabelWindowName.Content.ToString();

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

				FSBL.Clients.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				Show();
			});

			Debug.Print($"The window \"{this.LabelWindowName.Content}\" is connected to Finsemble.");
		}
	}
}
