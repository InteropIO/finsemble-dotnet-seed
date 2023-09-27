using ChartIQ.Finsemble;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble.Models;
using ChartIQ.Finsemble.TitlebarService.Models;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window1()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;

			Application.Current.Dispatcher.Invoke(() =>
			{
				FinsembleHeader.SetBridge(fsbl); // The Header Control needs a connected finsemble instance

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

				fsbl.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.
			});
		}
	}
}
