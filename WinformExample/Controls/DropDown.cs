using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformExample.Controls
{
	public class DropDown : ComboBox
	{
		private const int WM_PAINT = 0xF;
		private int buttonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
		private Color _buttonColor = Color.FromArgb(34, 38, 47);
		private Color _onHoverButtonColor = Color.FromArgb(40, 45, 56);
		private Color _textColor = Color.White;
		private Color _onHoverTextColor = Color.White;
		private Margins _imageMargins = new Margins(0, 0, 0, 0);

		public DropDown()
		{
			DoubleBuffered = true;
			DropDownStyle = ComboBoxStyle.DropDownList;
			MaxDropDownItems = 10;
			BackColor = _buttonColor;
			FlatStyle = FlatStyle.Popup;

			DrawItem += RoundedDropDown_DrawItem;
		}

		private void RoundedDropDown_DrawItem(object sender, DrawItemEventArgs e)
		{
			if (e.Index < 0)
				return;

			e.DrawBackground();

			// Draw each string in the array.
			e.Graphics.DrawString(
				Items[e.Index].ToString(),
				Font,
				new SolidBrush(_textColor),
				new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height)
			);

			e.DrawFocusRectangle();
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == WM_PAINT && DropDownStyle != ComboBoxStyle.Simple)
			{
				using (var g = Graphics.FromHwnd(Handle))
				{
					using (var p = new Pen(_onHoverButtonColor))
					{
						g.DrawRectangle(p, 0, 0, Width, Height - 1);

						var d = FlatStyle == FlatStyle.Popup ? 1 : 0;
						g.DrawLine(p, Width - d,
							0, Width - d, Height - d);
					}
				}
			}
		}
	}
}
