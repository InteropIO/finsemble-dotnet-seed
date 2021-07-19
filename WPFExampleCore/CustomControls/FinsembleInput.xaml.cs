using System;
using System.Windows.Controls;

namespace WPFExampleCore.CustomControls
{
	/// <summary>
	/// Interaction logic for FinsembleInput.xaml
	/// </summary>
	public partial class FinsembleInput : UserControl
	{
		public FinsembleInput()
		{
			InitializeComponent();
			TextBox.KeyDown += TextBox_KeyDown;
		}

		private void TextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
		{
			if (e.Key == System.Windows.Input.Key.Enter)
			{
				EnterKeyPressed?.Invoke(this, EventArgs.Empty);
			}
		}

		public event EventHandler EnterKeyPressed;
	}
}
