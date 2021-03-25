using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinformExample.Properties;

namespace WinformExample.Controls
{
	public partial class RoundedSelect : UserControl
	{
		private bool _isCollapsed;
		private Timer _timer = new Timer();

		private string _selectText = "Select";

		public RoundedSelect()
		{
			InitializeComponent();

			_timer.Tick += OnTimerTick;
		}

		private void OnTimerTick(object sender, EventArgs e)
		{
			if (_isCollapsed)
			{
				SelectOpenButton.Image = Resources.chevron_arrow_up;
				SelectOptionsPanel.Height += 20;
				if (SelectOptionsPanel.Size.Height == SelectOptionsPanel.MaximumSize.Height)
				{
					_timer.Stop();
					_isCollapsed = false;
				}
			}
			else
			{
				SelectOpenButton.Image = Resources.chevron_arrow_down;
				SelectOptionsPanel.Height -= 20;
				if (SelectOptionsPanel.Size.Height == SelectOptionsPanel.MinimumSize.Height)
				{
					_timer.Stop();
					_isCollapsed = true;
				}
			}
		}

		private void SelectOpenButton_Click(object sender, EventArgs e)
		{
			_timer.Start();
		}

		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);

			_selectText = Text;
			SelectOpenButton.Text = _selectText;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			SelectOpenButton.Text = _selectText;
		}

		public void AddElementToList(string elementName)
		{
			var btn = new RoundedButton()
			{
				Height = 50,
				Width = SelectOpenButton.Width - 40,
				Margin = new Padding(5, 0, 5, 0),
				ForeColor = Color.White,
				Text = elementName
			};
			btn.Click += Btn_Click;
			if (SelectOptionsPanel.Controls.Count > 0)
			{
				btn.Top = SelectOptionsPanel.Controls[SelectOptionsPanel.Controls.Count - 1].Top + 50;
			}
			else
			{
				btn.Top = 0;
			}
			SelectOptionsPanel.Controls.Add(btn);

		}

		private void Btn_Click(object sender, EventArgs e)
		{
			Text = (sender as Button).Text;
			SelectOpenButton_Click(this, null);
		}
	}
}
