
using System;
using System.Windows.Forms;

namespace WinformExampleCore.FDC3
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
			this.ScrimLabel = new System.Windows.Forms.Label();
			this.AlwaysOnTopButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.DockingButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.LinkerButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton1 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton2 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton3 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton4 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton5 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.GroupButton6 = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DataLabel = new System.Windows.Forms.Label();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.SendASymbolLabel = new System.Windows.Forms.Label();
			this.SendButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.DataToSendInput = new WinformExampleCore.FDC3.Controls.FinsembleInput();
			this.SpawnLabel = new System.Windows.Forms.Label();
			this.LaunchButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.ComponentDropDown = new WinformExampleCore.FDC3.Controls.DropDown();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.MessagesRichBox = new System.Windows.Forms.RichTextBox();
			this.DragNDropEmittingButton = new WinformExampleCore.FDC3.Controls.RoundedButton();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// ScrimLabel
			// 
			this.ScrimLabel.AccessibleName = "MainBackground";
			this.ScrimLabel.AllowDrop = true;
			this.ScrimLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.ScrimLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScrimLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.ScrimLabel.ForeColor = System.Drawing.Color.Transparent;
			this.ScrimLabel.Location = new System.Drawing.Point(0, 0);
			this.ScrimLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
			this.ScrimLabel.Name = "ScrimLabel";
			this.ScrimLabel.Size = new System.Drawing.Size(552, 584);
			this.ScrimLabel.TabIndex = 4;
			this.ScrimLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.ScrimLabel.Visible = false;
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
			// GroupButton1
			// 
			this.GroupButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(119)))), ((int)(((byte)(174)))));
			this.GroupButton1.CornerRadius = 15;
			this.GroupButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton1.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton1.Location = new System.Drawing.Point(43, 19);
			this.GroupButton1.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton1.Name = "GroupButton1";
			this.GroupButton1.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(185)))), ((int)(((byte)(240)))));
			this.GroupButton1.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton1.Size = new System.Drawing.Size(15, 24);
			this.GroupButton1.TabIndex = 28;
			this.GroupButton1.Text = "1";
			this.GroupButton1.TextColor = System.Drawing.Color.White;
			this.GroupButton1.UseEllipse = false;
			this.GroupButton1.UseVisualStyleBackColor = true;
			this.GroupButton1.Visible = false;
			// 
			// GroupButton2
			// 
			this.GroupButton2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(223)))), ((int)(((byte)(53)))));
			this.GroupButton2.CornerRadius = 15;
			this.GroupButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton2.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton2.Location = new System.Drawing.Point(66, 19);
			this.GroupButton2.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton2.Name = "GroupButton2";
			this.GroupButton2.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(223)))), ((int)(((byte)(53)))));
			this.GroupButton2.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton2.Size = new System.Drawing.Size(15, 24);
			this.GroupButton2.TabIndex = 29;
			this.GroupButton2.Text = "2";
			this.GroupButton2.TextColor = System.Drawing.Color.White;
			this.GroupButton2.UseEllipse = true;
			this.GroupButton2.UseVisualStyleBackColor = true;
			this.GroupButton2.Visible = false;
			// 
			// GroupButton3
			// 
			this.GroupButton3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(216)))), ((int)(((byte)(3)))));
			this.GroupButton3.CornerRadius = 15;
			this.GroupButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton3.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton3.Location = new System.Drawing.Point(89, 19);
			this.GroupButton3.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton3.Name = "GroupButton3";
			this.GroupButton3.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(216)))), ((int)(((byte)(3)))));
			this.GroupButton3.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton3.Size = new System.Drawing.Size(15, 24);
			this.GroupButton3.TabIndex = 30;
			this.GroupButton3.Text = "3";
			this.GroupButton3.TextColor = System.Drawing.Color.White;
			this.GroupButton3.UseEllipse = true;
			this.GroupButton3.UseVisualStyleBackColor = true;
			this.GroupButton3.Visible = false;
			// 
			// GroupButton4
			// 
			this.GroupButton4.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
			this.GroupButton4.CornerRadius = 15;
			this.GroupButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton4.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton4.Location = new System.Drawing.Point(112, 19);
			this.GroupButton4.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton4.Name = "GroupButton4";
			this.GroupButton4.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
			this.GroupButton4.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton4.Size = new System.Drawing.Size(15, 24);
			this.GroupButton4.TabIndex = 31;
			this.GroupButton4.Text = "4";
			this.GroupButton4.TextColor = System.Drawing.Color.White;
			this.GroupButton4.UseEllipse = true;
			this.GroupButton4.UseVisualStyleBackColor = true;
			this.GroupButton4.Visible = false;
			// 
			// GroupButton5
			// 
			this.GroupButton5.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
			this.GroupButton5.CornerRadius = 15;
			this.GroupButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton5.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton5.Location = new System.Drawing.Point(135, 19);
			this.GroupButton5.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton5.Name = "GroupButton5";
			this.GroupButton5.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
			this.GroupButton5.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton5.Size = new System.Drawing.Size(15, 24);
			this.GroupButton5.TabIndex = 32;
			this.GroupButton5.Text = "5";
			this.GroupButton5.TextColor = System.Drawing.Color.White;
			this.GroupButton5.UseEllipse = true;
			this.GroupButton5.UseVisualStyleBackColor = true;
			this.GroupButton5.Visible = false;
			// 
			// GroupButton6
			// 
			this.GroupButton6.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(0)))));
			this.GroupButton6.CornerRadius = 15;
			this.GroupButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.GroupButton6.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton6.Location = new System.Drawing.Point(158, 19);
			this.GroupButton6.Margin = new System.Windows.Forms.Padding(4);
			this.GroupButton6.Name = "GroupButton6";
			this.GroupButton6.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(0)))));
			this.GroupButton6.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton6.Size = new System.Drawing.Size(15, 24);
			this.GroupButton6.TabIndex = 33;
			this.GroupButton6.Text = "6";
			this.GroupButton6.TextColor = System.Drawing.Color.White;
			this.GroupButton6.UseEllipse = true;
			this.GroupButton6.UseVisualStyleBackColor = true;
			this.GroupButton6.Visible = false;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackgroundImage = global::WinformExampleCore.FDC3.Resource.winformsLogo;
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
			// DragNDropEmittingButton
			// 
			this.DragNDropEmittingButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.DragNDropEmittingButton.CornerRadius = 20;
			this.DragNDropEmittingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.DragNDropEmittingButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.DragNDropEmittingButton.Location = new System.Drawing.Point(41, 19);
			this.DragNDropEmittingButton.Margin = new System.Windows.Forms.Padding(2);
			this.DragNDropEmittingButton.Name = "DragNDropEmittingButton";
			this.DragNDropEmittingButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.DragNDropEmittingButton.OnHoverTextColor = System.Drawing.Color.Black;
			this.DragNDropEmittingButton.Size = new System.Drawing.Size(22, 24);
			this.DragNDropEmittingButton.TabIndex = 27;
			this.DragNDropEmittingButton.Text = "*";
			this.DragNDropEmittingButton.TextColor = System.Drawing.Color.White;
			this.DragNDropEmittingButton.UseEllipse = true;
			this.DragNDropEmittingButton.UseVisualStyleBackColor = true;
			this.DragNDropEmittingButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragNDropEmittingButton_MouseDown);
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
			this.Controls.Add(this.DragNDropEmittingButton);
			this.Controls.Add(this.MessagesLabel);
			this.Controls.Add(this.LaunchButton);
			this.Controls.Add(this.SpawnLabel);
			this.Controls.Add(this.SendButton);
			this.Controls.Add(this.SendASymbolLabel);
			this.Controls.Add(this.GroupButton6);
			this.Controls.Add(this.GroupButton5);
			this.Controls.Add(this.GroupButton4);
			this.Controls.Add(this.GroupButton3);
			this.Controls.Add(this.GroupButton2);
			this.Controls.Add(this.GroupButton1);
			this.Controls.Add(this.LinkerButton);
			this.Controls.Add(this.DockingButton);
			this.Controls.Add(this.AlwaysOnTopButton);
			this.Controls.Add(this.ScrimLabel);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.Name = "MainForm";
			this.Text = "Winform Example Core";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label ScrimLabel;
		private Controls.RoundedButton AlwaysOnTopButton;
		private Controls.RoundedButton DockingButton;
		private Controls.RoundedButton LinkerButton;
		private Controls.RoundedButton GroupButton1;
		private Controls.RoundedButton GroupButton2;
		private Controls.RoundedButton GroupButton5;
		private Controls.RoundedButton GroupButton3;
		private Controls.RoundedButton GroupButton4;
		private Controls.RoundedButton GroupButton6;
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
		private Controls.RoundedButton DragNDropEmittingButton;
		private Controls.DropDown ComponentDropDown;
		private Panel panel2;
	}
}

