using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WinformExampleCore.Controls
{
	public partial class FinsembleInput : UserControl
	{
		public FinsembleInput()
		{
			InitializeComponent();

			textBox1.GotFocus += TextBox1_GotFocus;
			textBox1.LostFocus += TextBox1OnLostFocus;
			textBox1.KeyDown += TextBox1_KeyDown;
		}

		private void TextBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				EnterKeyPressed?.Invoke(this, EventArgs.Empty);
			}
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

		public event EventHandler EnterKeyPressed;
	}
}
