using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Net.Mime;
using System.Windows.Forms;

namespace WinformExample.Controls
{
	public class RoundedButton : Button
	{
		private Color _buttonColor = Color.FromArgb(34, 38, 47);
		private Color _onHoverButtonColor = Color.FromArgb(40, 45, 56);
		private Color _textColor = Color.White;
		private Color _onHoverTextColor = Color.White;
		private int _cornerRadius = 1;
		private Margins _imageMargins = new Margins(0, 0, 0, 0);

		private bool _isHovering;
		private bool _useEllipse;

		public RoundedButton()
		{
			DoubleBuffered = true;
			MouseEnter += (sender, e) =>
			{
				_isHovering = true;
				BackColor = Color.Transparent;
				Invalidate();
			};
			MouseLeave += (sender, e) =>
			{
				_isHovering = false;
				BackColor = Color.Transparent;
				Invalidate();
			};
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (Visible)
			{
				var rect = new RectangleF(0, 0, this.Width, this.Height);
				using (var graphPath = GetRoundPath(rect, _cornerRadius))
				{
					this.Region = new Region(graphPath);
					using (var pen = new Pen(Color.Transparent, 0))
					{
						pen.Alignment = PenAlignment.Inset;
						e.Graphics.DrawPath(pen, graphPath);

						var buttonColor = new SolidBrush(_isHovering ? _onHoverButtonColor : _buttonColor);
						e.Graphics.FillPath(buttonColor, graphPath);

						var stringSize = e.Graphics.MeasureString(Text, Font);
						var stringColor = new SolidBrush(_isHovering ? _onHoverTextColor : _textColor);

						e.Graphics.DrawString(Text, Font, stringColor, (Width - stringSize.Width) / 2, (Height - stringSize.Height) / 2);

						if (Image != null)
						{
							e.Graphics.DrawImage(Image, (Width - stringSize.Width) / 2 + stringSize.Width + _imageMargins.Left, (Height - Image.Height) / 2 + _imageMargins.Top);
						}
					}
				}
			}
		}

		GraphicsPath GetRoundPath(RectangleF Rect, int radius)
		{
			var r2 = radius / 2f;

			var graphPath = new GraphicsPath();
			if (_useEllipse)
			{
				graphPath.AddEllipse(Rect);
			}
			else
			{
				graphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
				graphPath.AddLine(Rect.X + r2, Rect.Y, Rect.Width - r2, Rect.Y);
				graphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
				graphPath.AddLine(Rect.Width, Rect.Y + r2, Rect.Width, Rect.Height - r2);
				graphPath.AddArc(Rect.X + Rect.Width - radius,
					Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
				graphPath.AddLine(Rect.Width - r2, Rect.Height, Rect.X + r2, Rect.Height);
				graphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
				graphPath.AddLine(Rect.X, Rect.Height - r2, Rect.X, Rect.Y + r2);
			}

			graphPath.CloseFigure();
			return graphPath;
		}

		public Color ButtonColor
		{
			get => _buttonColor;
			set
			{
				_buttonColor = value;
				Invalidate();
			}
		}

		public int CornerRadius
		{
			get => _cornerRadius;
			set
			{
				_cornerRadius = value;
				Invalidate();
			}
		}

		public Margins ImageMargins
		{
			get => _imageMargins;
			set
			{
				_imageMargins = value;
				Invalidate();
			}
		}

		public Color OnHoverButtonColor
		{
			get => _onHoverButtonColor;
			set
			{
				_onHoverButtonColor = value;
				Invalidate();
			}
		}

		public Color TextColor
		{
			get => _textColor;
			set
			{
				_textColor = value;
				Invalidate();
			}
		}

		public Color OnHoverTextColor
		{
			get => _onHoverTextColor;
			set
			{
				_onHoverTextColor = value;
				Invalidate();
			}
		}

		public bool UseEllipse
		{
			get => _useEllipse;
			set
			{
				_useEllipse = value;
				Invalidate();
			}
		}
	}
}
