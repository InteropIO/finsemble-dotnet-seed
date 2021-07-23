using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFExampleCore.CustomControls
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
