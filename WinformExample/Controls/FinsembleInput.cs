using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ColorConverter = System.Windows.Media.ColorConverter;

namespace WinformExample.Controls
{
	public partial class FinsembleInput : UserControl
	{
		public FinsembleInput()
		{
			InitializeComponent();

			textBox1.GotFocus += TextBox1_GotFocus;
			textBox1.LostFocus += TextBox1OnLostFocus;
		}

		private void TextBox1OnLostFocus(object sender, EventArgs e)
		{
			panel1.BackColor = Color.FromArgb(61, 68, 85); //#3D4455
		}

		private void TextBox1_GotFocus(object sender, EventArgs e)
		{
			panel1.BackColor = Color.FromArgb(3, 155, 255); //#039bff
		}

		protected override void OnResize(EventArgs e)
		{
			textBox1.Width = panel1.Width;
			base.OnResize(e);
		}

		public override string Text
		{
			get => textBox1.Text;
			set => textBox1.Text = value;
		}
	}
}
