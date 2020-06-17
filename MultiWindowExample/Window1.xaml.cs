using System.Reflection;
using System.Windows;
using System.Windows.Media;
using ChartIQ.Finsemble;
using log4net;

namespace MultiWindowExample
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window, IIntegratable
	{
		/// <summary>
		/// The logger
		/// </summary>
		private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
				FinsembleHeader.SetActiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F")));
				FinsembleHeader.SetInactiveBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#22262F")));
				FinsembleHeader.SetButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetInactiveButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666")));
				FinsembleHeader.SetInactiveCloseButtonHoverBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F26666")));
				FinsembleHeader.SetDockingButtonDockedBackground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0A8CF4")));
				FinsembleHeader.SetTitleForeground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0")));
				FinsembleHeader.SetButtonForeground(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ACB2C0")));

				FinsembleHeader.SetButtonFont(null, 8, FontStyles.Normal, FontWeights.Normal);
				FinsembleHeader.SetTitleFont(null, 12, FontStyles.Normal, FontWeights.SemiBold);

				fsbl.DragAndDropClient.SetScrim(Scrim); // The Scrim Label Control is used for drag and drop.

				Show();
			});
		}
	}
}
