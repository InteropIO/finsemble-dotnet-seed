
using System;
using System.Windows.Forms;

namespace WinformExampleCore
{
	partial class MainForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.AlwaysOnTopButton = new WinformExampleCore.Controls.RoundedButton();
			this.DockingButton = new WinformExampleCore.Controls.RoundedButton();
			this.LinkerButton = new WinformExampleCore.Controls.RoundedButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DataLabel = new System.Windows.Forms.Label();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.SendASymbolLabel = new System.Windows.Forms.Label();
			this.SendButton = new WinformExampleCore.Controls.RoundedButton();
			this.DataToSendInput = new WinformExampleCore.Controls.FinsembleInput();
			this.SpawnLabel = new System.Windows.Forms.Label();
			this.LaunchButton = new WinformExampleCore.Controls.RoundedButton();
			this.ComponentDropDown = new WinformExampleCore.Controls.DropDown();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.MessagesRichBox = new System.Windows.Forms.RichTextBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// AlwaysOnTopButton
			// 
			this.AlwaysOnTopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.AlwaysOnTopButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.AlwaysOnTopButton.CornerRadius = 20;
			this.AlwaysOnTopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.AlwaysOnTopButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.AlwaysOnTopButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.AlwaysOnTopButton.Location = new System.Drawing.Point(517, 23);
			this.AlwaysOnTopButton.Margin = new System.Windows.Forms.Padding(6);
			this.AlwaysOnTopButton.Name = "AlwaysOnTopButton";
			this.AlwaysOnTopButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.AlwaysOnTopButton.OnHoverTextColor = System.Drawing.Color.White;
			this.AlwaysOnTopButton.Size = new System.Drawing.Size(15, 24);
			this.AlwaysOnTopButton.TabIndex = 25;
			this.AlwaysOnTopButton.Text = "9";
			this.AlwaysOnTopButton.TextColor = System.Drawing.Color.White;
			this.AlwaysOnTopButton.UseEllipse = true;
			this.AlwaysOnTopButton.UseVisualStyleBackColor = true;
			this.AlwaysOnTopButton.Click += new System.EventHandler(this.AlwaysOnTopButton_Click);
			// 
			// DockingButton
			// 
			this.DockingButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DockingButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.DockingButton.CornerRadius = 20;
			this.DockingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DockingButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.DockingButton.Location = new System.Drawing.Point(490, 23);
			this.DockingButton.Margin = new System.Windows.Forms.Padding(6);
			this.DockingButton.Name = "DockingButton";
			this.DockingButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.DockingButton.OnHoverTextColor = System.Drawing.Color.White;
			this.DockingButton.Size = new System.Drawing.Size(15, 24);
			this.DockingButton.TabIndex = 26;
			this.DockingButton.Text = ">";
			this.DockingButton.TextColor = System.Drawing.Color.White;
			this.DockingButton.UseEllipse = true;
			this.DockingButton.UseVisualStyleBackColor = true;
			this.DockingButton.Visible = false;
			this.DockingButton.Click += new System.EventHandler(this.DockingButton_Click);
			// 
			// LinkerButton
			// 
			this.LinkerButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.LinkerButton.CornerRadius = 20;
			this.LinkerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LinkerButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.LinkerButton.Location = new System.Drawing.Point(20, 19);
			this.LinkerButton.Margin = new System.Windows.Forms.Padding(4);
			this.LinkerButton.Name = "LinkerButton";
			this.LinkerButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.LinkerButton.OnHoverTextColor = System.Drawing.Color.White;
			this.LinkerButton.Size = new System.Drawing.Size(15, 24);
			this.LinkerButton.TabIndex = 24;
			this.LinkerButton.Text = "1";
			this.LinkerButton.TextColor = System.Drawing.Color.White;
			this.LinkerButton.UseEllipse = true;
			this.LinkerButton.UseVisualStyleBackColor = true;
			this.LinkerButton.Click += new System.EventHandler(this.LinkerButton_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackgroundImage = global::WinformExampleCore.Resource.winformsLogo;
			this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.panel1.Location = new System.Drawing.Point(198, 11);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(123, 98);
			this.panel1.TabIndex = 13;
			// 
			// DataLabel
			// 
			this.DataLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.DataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.DataLabel.ForeColor = System.Drawing.Color.White;
			this.DataLabel.Location = new System.Drawing.Point(0, 143);
			this.DataLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.DataLabel.Name = "DataLabel";
			this.DataLabel.Size = new System.Drawing.Size(512, 28);
			this.DataLabel.TabIndex = 14;
			this.DataLabel.Text = "MSFT";
			this.DataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SourceLabel
			// 
			this.SourceLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.SourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.SourceLabel.ForeColor = System.Drawing.Color.White;
			this.SourceLabel.Location = new System.Drawing.Point(0, 171);
			this.SourceLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SourceLabel.Name = "SourceLabel";
			this.SourceLabel.Size = new System.Drawing.Size(512, 30);
			this.SourceLabel.TabIndex = 34;
			this.SourceLabel.Text = "via default value";
			this.SourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SendASymbolLabel
			// 
			this.SendASymbolLabel.AutoSize = true;
			this.SendASymbolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.SendASymbolLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.SendASymbolLabel.Location = new System.Drawing.Point(17, 277);
			this.SendASymbolLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SendASymbolLabel.Name = "SendASymbolLabel";
			this.SendASymbolLabel.Size = new System.Drawing.Size(116, 17);
			this.SendASymbolLabel.TabIndex = 10;
			this.SendASymbolLabel.Text = "Send a Symbol";
			// 
			// SendButton
			// 
			this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.SendButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.SendButton.CornerRadius = 25;
			this.SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.SendButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.SendButton.Location = new System.Drawing.Point(440, 277);
			this.SendButton.Margin = new System.Windows.Forms.Padding(2);
			this.SendButton.Name = "SendButton";
			this.SendButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.SendButton.OnHoverTextColor = System.Drawing.Color.White;
			this.SendButton.Size = new System.Drawing.Size(92, 24);
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
			this.DataToSendInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DataToSendInput.Location = new System.Drawing.Point(211, 275);
			this.DataToSendInput.Margin = new System.Windows.Forms.Padding(2);
			this.DataToSendInput.Name = "DataToSendInput";
			this.DataToSendInput.Size = new System.Drawing.Size(203, 26);
			this.DataToSendInput.TabIndex = 7;
			// 
			// SpawnLabel
			// 
			this.SpawnLabel.AutoSize = true;
			this.SpawnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.SpawnLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.SpawnLabel.Location = new System.Drawing.Point(20, 355);
			this.SpawnLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.SpawnLabel.Name = "SpawnLabel";
			this.SpawnLabel.Size = new System.Drawing.Size(155, 17);
			this.SpawnLabel.TabIndex = 35;
			this.SpawnLabel.Text = "Spawn a Component";
			// 
			// LaunchButton
			// 
			this.LaunchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LaunchButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.LaunchButton.CornerRadius = 25;
			this.LaunchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.LaunchButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.LaunchButton.Location = new System.Drawing.Point(440, 345);
			this.LaunchButton.Margin = new System.Windows.Forms.Padding(2);
			this.LaunchButton.Name = "LaunchButton";
			this.LaunchButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.LaunchButton.OnHoverTextColor = System.Drawing.Color.White;
			this.LaunchButton.Size = new System.Drawing.Size(92, 27);
			this.LaunchButton.TabIndex = 36;
			this.LaunchButton.Text = "Launch";
			this.LaunchButton.TextColor = System.Drawing.Color.White;
			this.LaunchButton.UseEllipse = false;
			this.LaunchButton.UseVisualStyleBackColor = true;
			this.LaunchButton.Click += new System.EventHandler(this.LaunchButton_Click);
			// 
			// ComponentDropDown
			// 
			this.ComponentDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.ComponentDropDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.ComponentDropDown.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.ComponentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ComponentDropDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.ComponentDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ComponentDropDown.Location = new System.Drawing.Point(211, 345);
			this.ComponentDropDown.Margin = new System.Windows.Forms.Padding(2);
			this.ComponentDropDown.MaxDropDownItems = 10;
			this.ComponentDropDown.Name = "ComponentDropDown";
			this.ComponentDropDown.Size = new System.Drawing.Size(203, 27);
			this.ComponentDropDown.TabIndex = 35;
			// 
			// MessagesLabel
			// 
			this.MessagesLabel.AutoSize = true;
			this.MessagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.MessagesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(3)))), ((int)(((byte)(155)))), ((int)(((byte)(255)))));
			this.MessagesLabel.Location = new System.Drawing.Point(20, 402);
			this.MessagesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.MessagesLabel.Name = "MessagesLabel";
			this.MessagesLabel.Size = new System.Drawing.Size(80, 17);
			this.MessagesLabel.TabIndex = 37;
			this.MessagesLabel.Text = "Messages";
			// 
			// MessagesRichBox
			// 
			this.MessagesRichBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MessagesRichBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.MessagesRichBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.MessagesRichBox.ForeColor = System.Drawing.Color.White;
			this.MessagesRichBox.Location = new System.Drawing.Point(20, 421);
			this.MessagesRichBox.Margin = new System.Windows.Forms.Padding(2);
			this.MessagesRichBox.Name = "MessagesRichBox";
			this.MessagesRichBox.Size = new System.Drawing.Size(512, 152);
			this.MessagesRichBox.TabIndex = 22;
			this.MessagesRichBox.Text = "";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.Controls.Add(this.DataLabel);
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.SourceLabel);
			this.panel2.Location = new System.Drawing.Point(20, 62);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(512, 201);
			this.panel2.TabIndex = 39;
			// 
			// MainForm
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.ClientSize = new System.Drawing.Size(552, 584);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.ComponentDropDown);
			this.Controls.Add(this.MessagesRichBox);
			this.Controls.Add(this.DataToSendInput);
			this.Controls.Add(this.MessagesLabel);
			this.Controls.Add(this.LaunchButton);
			this.Controls.Add(this.SpawnLabel);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.SendASymbolLabel);
			this.Controls.Add(this.LinkerButton);
			this.Controls.Add(this.DockingButton);
			this.Controls.Add(this.AlwaysOnTopButton);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.Name = "MainForm";
			this.Text = "Winform Example Core";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Controls.RoundedButton AlwaysOnTopButton;
		private Controls.RoundedButton DockingButton;
		private Controls.RoundedButton LinkerButton;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label DataLabel;
		private System.Windows.Forms.Label SourceLabel;
		private System.Windows.Forms.Label MessagesLabel;
		private System.Windows.Forms.Label SendASymbolLabel;
		private Controls.RoundedButton SendButton;
		private Controls.FinsembleInput DataToSendInput;
		private System.Windows.Forms.Label SpawnLabel;
		private Controls.RoundedButton LaunchButton;
		private System.Windows.Forms.RichTextBox MessagesRichBox;
		private Controls.DropDown ComponentDropDown;
		private Panel panel2;
	}
}

