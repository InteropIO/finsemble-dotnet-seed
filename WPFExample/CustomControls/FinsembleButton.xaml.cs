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

namespace WPFExample.CustomControls
{
	/// <summary>
	/// Interaction logic for FinsembleButton.xaml
	/// </summary>
	public partial class FinsembleButton : UserControl
	{
		public event EventHandler<RoutedEventArgs> Click;

		public string ButtonContent { get; set; }
		public FinsembleButton()
		{
			InitializeComponent();
			this.DataContext = this;
		}

		private void Button_OnClick(object sender, RoutedEventArgs e)
		{
			var eventHandler = this.Click;

			eventHandler?.Invoke(this, e);
		}
	}
}
