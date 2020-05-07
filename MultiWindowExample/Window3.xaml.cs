using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window3.xaml
	/// </summary>
	public partial class Window3 : Window, IIntegratable
	{
		private Finsemble fsbl;

		public Window3()
		{
			InitializeComponent();
		}

		public void SetFinsemble(Finsemble fsbl)
		{
			this.fsbl = fsbl;

			Application.Current.Dispatcher.Invoke(() =>
			{
				FinsembleHeader.SetBridge(fsbl); // The Header Control needs a connected finsemble instance

				// Styling the Finsemble Header
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

				fsbl.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				Show();
			});
		}
	}
}
