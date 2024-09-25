using ChartIQ.Finsemble;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace WinformExample
{
	partial class FormExample
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.SendASymbolLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DataLabel = new System.Windows.Forms.Label();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.MessagesRichBox = new System.Windows.Forms.RichTextBox();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.ComponentDropDown = new WinformExample.Controls.DropDown();
			this.DockingButton = new WinformExample.Controls.RoundedButton();
			this.AlwaysOnTopButton = new WinformExample.Controls.RoundedButton();
			this.LinkerButton = new WinformExample.Controls.RoundedButton();
			this.SendButton = new WinformExample.Controls.RoundedButton();
			this.DataToSendInput = new WinformExample.Controls.FinsembleInput();
			this.LaunchButton = new WinformExample.Controls.RoundedButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// SendASymbolLabel
			// 
			this.SendASymbolLabel.AutoSize = true;
			this.SendASymbolLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.SendASymbolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SendASymbolLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.SendASymbolLabel.Location = new System.Drawing.Point(20, 240);
			this.SendASymbolLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SendASymbolLabel.Name = "SendASymbolLabel";
			this.SendASymbolLabel.Size = new System.Drawing.Size(134, 20);
			this.SendASymbolLabel.TabIndex = 10;
			this.SendASymbolLabel.Text = "Send a Symbol";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.label1.Location = new System.Drawing.Point(20, 318);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 20);
			this.label1.TabIndex = 11;
			this.label1.Text = "Spawn a Component";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackgroundImage = global::WinformExample.Properties.Resources.winformsLogo;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panel1.Location = new System.Drawing.Point(31, 11);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(493, 85);
			this.panel1.TabIndex = 13;
			// 
			// DataLabel
			// 
			this.DataLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DataLabel.ForeColor = System.Drawing.Color.White;
			this.DataLabel.Location = new System.Drawing.Point(0, 116);
			this.DataLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.DataLabel.Name = "DataLabel";
			this.DataLabel.Size = new System.Drawing.Size(551, 29);
			this.DataLabel.TabIndex = 14;
			this.DataLabel.Text = "MSFT";
			this.DataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SourceLabel
			// 
			this.SourceLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SourceLabel.ForeColor = System.Drawing.Color.White;
			this.SourceLabel.Location = new System.Drawing.Point(0, 145);
			this.SourceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SourceLabel.Name = "SourceLabel";
			this.SourceLabel.Size = new System.Drawing.Size(551, 43);
			this.SourceLabel.TabIndex = 15;
			this.SourceLabel.Text = "via PubSub";
			this.SourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MessagesRichBox
			// 
			this.MessagesRichBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesRichBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.MessagesRichBox.ForeColor = System.Drawing.Color.White;
			this.MessagesRichBox.Location = new System.Drawing.Point(15, 410);
			this.MessagesRichBox.Margin = new System.Windows.Forms.Padding(2);
			this.MessagesRichBox.Name = "MessagesRichBox";
			this.MessagesRichBox.Size = new System.Drawing.Size(521, 154);
			this.MessagesRichBox.TabIndex = 22;
			this.MessagesRichBox.Text = "";
			// 
			// MessagesLabel
			// 
			this.MessagesLabel.AutoSize = true;
			this.MessagesLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.MessagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MessagesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.MessagesLabel.Location = new System.Drawing.Point(20, 370);
			this.MessagesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.MessagesLabel.Name = "MessagesLabel";
			this.MessagesLabel.Size = new System.Drawing.Size(94, 20);
			this.MessagesLabel.TabIndex = 23;
			this.MessagesLabel.Text = "Messages";
			// 
			// ComponentDropDown
			// 
			this.ComponentDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ComponentDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.ComponentDropDown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ComponentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComponentDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ComponentDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ComponentDropDown.Location = new System.Drawing.Point(212, 318);
			this.ComponentDropDown.Margin = new System.Windows.Forms.Padding(2);
			this.ComponentDropDown.MaxDropDownItems = 10;
			this.ComponentDropDown.Name = "ComponentDropDown";
			this.ComponentDropDown.Size = new System.Drawing.Size(203, 31);
			this.ComponentDropDown.TabIndex = 35;
			// 
			// DockingButton
			// 
			this.DockingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DockingButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.DockingButton.CornerRadius = 25;
			this.DockingButton.Font = new System.Drawing.Font("font-finance", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DockingButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.DockingButton.Location = new System.Drawing.Point(482, 10);
			this.DockingButton.Margin = new System.Windows.Forms.Padding(0);
			this.DockingButton.Name = "DockingButton";
			this.DockingButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.DockingButton.OnHoverTextColor = System.Drawing.Color.White;
			this.DockingButton.Size = new System.Drawing.Size(20, 23);
			this.DockingButton.TabIndex = 26;
			this.DockingButton.Text = ">";
			this.DockingButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			this.DockingButton.TextColor = System.Drawing.Color.White;
			this.DockingButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.DockingButton.UseEllipse = true;
			this.DockingButton.UseVisualStyleBackColor = true;
			this.DockingButton.Visible = false;
			this.DockingButton.Click += new System.EventHandler(this.DockingButton_Click);
			// 
			// AlwaysOnTopButton
			// 
			this.AlwaysOnTopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AlwaysOnTopButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.AlwaysOnTopButton.CornerRadius = 25;
			this.AlwaysOnTopButton.Font = new System.Drawing.Font("font-finance", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.AlwaysOnTopButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.AlwaysOnTopButton.Location = new System.Drawing.Point(505, 10);
			this.AlwaysOnTopButton.Margin = new System.Windows.Forms.Padding(2);
			this.AlwaysOnTopButton.Name = "AlwaysOnTopButton";
			this.AlwaysOnTopButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.AlwaysOnTopButton.OnHoverTextColor = System.Drawing.Color.White;
			this.AlwaysOnTopButton.Size = new System.Drawing.Size(20, 23);
			this.AlwaysOnTopButton.TabIndex = 25;
			this.AlwaysOnTopButton.Text = "9";
			this.AlwaysOnTopButton.TextColor = System.Drawing.Color.White;
			this.AlwaysOnTopButton.UseEllipse = false;
			this.AlwaysOnTopButton.UseVisualStyleBackColor = true;
			this.AlwaysOnTopButton.Click += new System.EventHandler(this.AlwaysOnTopButton_Click);
			// 
			// LinkerButton
			// 
			this.LinkerButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.LinkerButton.CornerRadius = 20;
			this.LinkerButton.Font = new System.Drawing.Font("font-finance", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LinkerButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.LinkerButton.Location = new System.Drawing.Point(9, 10);
			this.LinkerButton.Margin = new System.Windows.Forms.Padding(2);
			this.LinkerButton.Name = "LinkerButton";
			this.LinkerButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.LinkerButton.OnHoverTextColor = System.Drawing.Color.White;
			this.LinkerButton.Size = new System.Drawing.Size(22, 24);
			this.LinkerButton.TabIndex = 24;
			this.LinkerButton.Text = "1";
			this.LinkerButton.TextColor = System.Drawing.Color.White;
			this.LinkerButton.UseEllipse = false;
			this.LinkerButton.UseVisualStyleBackColor = true;
			this.LinkerButton.Click += new System.EventHandler(this.LinkerButton_Click);
			// 
			// SendButton
			// 
			this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SendButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.SendButton.CornerRadius = 25;
			this.SendButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.SendButton.Location = new System.Drawing.Point(430, 232);
			this.SendButton.Margin = new System.Windows.Forms.Padding(2);
			this.SendButton.Name = "SendButton";
			this.SendButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.SendButton.OnHoverTextColor = System.Drawing.Color.White;
			this.SendButton.Size = new System.Drawing.Size(92, 41);
			this.SendButton.TabIndex = 8;
			this.SendButton.Text = "Send";
			this.SendButton.TextColor = System.Drawing.Color.White;
			this.SendButton.UseEllipse = false;
			this.SendButton.UseVisualStyleBackColor = true;
			this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
			// 
			// DataToSendInput
			// 
			this.DataToSendInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DataToSendInput.AutoSize = true;
			this.DataToSendInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.DataToSendInput.ForeColor = System.Drawing.Color.White;
			this.DataToSendInput.Location = new System.Drawing.Point(212, 239);
			this.DataToSendInput.Margin = new System.Windows.Forms.Padding(2);
			this.DataToSendInput.Name = "DataToSendInput";
			this.DataToSendInput.Size = new System.Drawing.Size(203, 34);
			this.DataToSendInput.TabIndex = 7;
			// 
			// LaunchButton
			// 
			this.LaunchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LaunchButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.LaunchButton.CornerRadius = 25;
			this.LaunchButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.LaunchButton.Location = new System.Drawing.Point(430, 310);
			this.LaunchButton.Margin = new System.Windows.Forms.Padding(2);
			this.LaunchButton.Name = "LaunchButton";
			this.LaunchButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.LaunchButton.OnHoverTextColor = System.Drawing.Color.White;
			this.LaunchButton.Size = new System.Drawing.Size(92, 41);
			this.LaunchButton.TabIndex = 5;
			this.LaunchButton.Text = "Launch";
			this.LaunchButton.TextColor = System.Drawing.Color.White;
			this.LaunchButton.UseEllipse = false;
			this.LaunchButton.UseVisualStyleBackColor = true;
			this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.DataLabel);
			this.panel2.Controls.Add(this.SourceLabel);
			this.panel2.Location = new System.Drawing.Point(-1, 39);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(551, 188);
			this.panel2.TabIndex = 36;
			// 
			// FormExample
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(550, 576);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.ComponentDropDown);
			this.Controls.Add(this.DockingButton);
			this.Controls.Add(this.AlwaysOnTopButton);
			this.Controls.Add(this.LinkerButton);
			this.Controls.Add(this.MessagesLabel);
			this.Controls.Add(this.MessagesRichBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.SendASymbolLabel);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.DataToSendInput);
			this.Controls.Add(this.LaunchButton);
			this.Name = "FormExample";
			this.Text = "Winform Example";
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}



		#endregion
		private Controls.RoundedButton LaunchButton;
		private Controls.FinsembleInput DataToSendInput;
		private Controls.RoundedButton SendButton;
		private System.Windows.Forms.Label SendASymbolLabel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label DataLabel;
		private System.Windows.Forms.Label SourceLabel;
		private System.Windows.Forms.RichTextBox MessagesRichBox;
		private System.Windows.Forms.Label MessagesLabel;
		private Controls.RoundedButton LinkerButton;
		private Controls.RoundedButton AlwaysOnTopButton;
		private Controls.RoundedButton DockingButton;
		private Controls.DropDown ComponentDropDown;
		private Panel panel2;
	}
}

