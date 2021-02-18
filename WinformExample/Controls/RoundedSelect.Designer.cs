using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace WinformExample.Controls
{
	partial class RoundedSelect
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SelectOptionsPanel = new System.Windows.Forms.FlowLayoutPanel();
			this.SelectOpenButton = new RoundedButton();
			this.SuspendLayout();
			// 
			// SelectOpenButton
			// 
			this.SelectOpenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.SelectOpenButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.SelectOpenButton.FlatAppearance.BorderSize = 0;
			this.SelectOpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.SelectOpenButton.ForeColor = System.Drawing.Color.White;
			this.SelectOpenButton.Image = global::WinformExample.Properties.Resources.chevron_arrow_down;
			this.SelectOpenButton.ImageMargins = new Margins(45, 0, 3, 0);
			this.SelectOpenButton.CornerRadius = 25;
			this.SelectOpenButton.Location = new System.Drawing.Point(0, 0);
			this.SelectOpenButton.MinimumSize = new System.Drawing.Size(50, 50);
			this.SelectOpenButton.Name = "SelectOpenButton";
			this.SelectOpenButton.Size = new System.Drawing.Size(150, 50);
			this.SelectOpenButton.TabIndex = 3;
			this.SelectOpenButton.Text = "Select";
			this.SelectOpenButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.SelectOpenButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.SelectOpenButton.UseVisualStyleBackColor = false;
			this.SelectOpenButton.Click += new System.EventHandler(this.SelectOpenButton_Click);
			// 
			// SelectOptionsPanel
			// 
			this.SelectOptionsPanel.AutoScroll = true;
			this.SelectOptionsPanel.Location = new System.Drawing.Point(0, 75);
			this.SelectOptionsPanel.MaximumSize = new System.Drawing.Size(300, 250);
			this.SelectOptionsPanel.MinimumSize = new System.Drawing.Size(50, 0);
			this.SelectOptionsPanel.Name = "SelectOptionsPanel";
			this.SelectOptionsPanel.Size = new System.Drawing.Size(250, 0);
			this.SelectOptionsPanel.Location = new Point(SelectOpenButton.Location.X, SelectOpenButton.Location.Y + SelectOpenButton.Height);
			this.SelectOptionsPanel.TabIndex = 4;
			// 
			// RoundedSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.SelectOptionsPanel);
			this.Controls.Add(this.SelectOpenButton);
			this.Name = "RoundedSelect";
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.FlowLayoutPanel SelectOptionsPanel;
		private RoundedButton SelectOpenButton;
	}
}
