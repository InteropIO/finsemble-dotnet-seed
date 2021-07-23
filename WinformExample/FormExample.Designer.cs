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

		private static Finsemble FSBL = null;

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
			this.scrim = new System.Windows.Forms.Label();
			this.SendASymbolLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.DataLabel = new System.Windows.Forms.Label();
			this.SourceLabel = new System.Windows.Forms.Label();
			this.MessagesRichBox = new System.Windows.Forms.RichTextBox();
			this.MessagesLabel = new System.Windows.Forms.Label();
			this.ComponentDropDown = new WinformExample.Controls.DropDown();
			this.GroupButton6 = new WinformExample.Controls.RoundedButton();
			this.GroupButton5 = new WinformExample.Controls.RoundedButton();
			this.GroupButton4 = new WinformExample.Controls.RoundedButton();
			this.GroupButton3 = new WinformExample.Controls.RoundedButton();
			this.GroupButton2 = new WinformExample.Controls.RoundedButton();
			this.GroupButton1 = new WinformExample.Controls.RoundedButton();
			this.DragNDropEmittingButton = new WinformExample.Controls.RoundedButton();
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
			// scrim
			// 
			this.scrim.AccessibleName = "MainBackground";
			this.scrim.AllowDrop = true;
			this.scrim.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(26)))), ((int)(((byte)(32)))));
			this.scrim.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrim.Font = new System.Drawing.Font("font-finance", 100F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scrim.ForeColor = System.Drawing.Color.Transparent;
			this.scrim.Location = new System.Drawing.Point(0, 0);
			this.scrim.Name = "scrim";
			this.scrim.Size = new System.Drawing.Size(550, 576);
			this.scrim.TabIndex = 4;
			this.scrim.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.scrim.Visible = false;
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
			// GroupButton6
			// 
			this.GroupButton6.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(162)))), ((int)(((byte)(0)))));
			this.GroupButton6.CornerRadius = 15;
			this.GroupButton6.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton6.Location = new System.Drawing.Point(134, 10);
			this.GroupButton6.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton6.Name = "GroupButton6";
			this.GroupButton6.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(220)))), ((int)(((byte)(59)))));
			this.GroupButton6.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton6.Size = new System.Drawing.Size(22, 24);
			this.GroupButton6.TabIndex = 33;
			this.GroupButton6.Text = "6";
			this.GroupButton6.TextColor = System.Drawing.Color.White;
			this.GroupButton6.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.GroupButton6.UseEllipse = true;
			this.GroupButton6.UseVisualStyleBackColor = true;
			this.GroupButton6.Visible = false;
			// 
			// GroupButton5
			// 
			this.GroupButton5.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
			this.GroupButton5.CornerRadius = 15;
			this.GroupButton5.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton5.Location = new System.Drawing.Point(114, 10);
			this.GroupButton5.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton5.Name = "GroupButton5";
			this.GroupButton5.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(228)))), ((int)(((byte)(243)))));
			this.GroupButton5.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton5.Size = new System.Drawing.Size(22, 24);
			this.GroupButton5.TabIndex = 32;
			this.GroupButton5.Text = "5";
			this.GroupButton5.TextColor = System.Drawing.Color.White;
			this.GroupButton5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.GroupButton5.UseEllipse = true;
			this.GroupButton5.UseVisualStyleBackColor = true;
			this.GroupButton5.Visible = false;
			// 
			// GroupButton4
			// 
			this.GroupButton4.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(98)))), ((int)(((byte)(98)))));
			this.GroupButton4.CornerRadius = 15;
			this.GroupButton4.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton4.Location = new System.Drawing.Point(94, 10);
			this.GroupButton4.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton4.Name = "GroupButton4";
			this.GroupButton4.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
			this.GroupButton4.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton4.Size = new System.Drawing.Size(22, 24);
			this.GroupButton4.TabIndex = 31;
			this.GroupButton4.Text = "4";
			this.GroupButton4.TextColor = System.Drawing.Color.White;
			this.GroupButton4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.GroupButton4.UseEllipse = true;
			this.GroupButton4.UseVisualStyleBackColor = true;
			this.GroupButton4.Visible = false;
			// 
			// GroupButton3
			// 
			this.GroupButton3.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(216)))), ((int)(((byte)(3)))));
			this.GroupButton3.CornerRadius = 15;
			this.GroupButton3.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton3.Location = new System.Drawing.Point(75, 10);
			this.GroupButton3.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton3.Name = "GroupButton3";
			this.GroupButton3.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(255)))), ((int)(((byte)(55)))));
			this.GroupButton3.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton3.Size = new System.Drawing.Size(22, 24);
			this.GroupButton3.TabIndex = 30;
			this.GroupButton3.Text = "3";
			this.GroupButton3.TextColor = System.Drawing.Color.White;
			this.GroupButton3.UseEllipse = true;
			this.GroupButton3.UseVisualStyleBackColor = true;
			this.GroupButton3.Visible = false;
			// 
			// GroupButton2
			// 
			this.GroupButton2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(223)))), ((int)(((byte)(53)))));
			this.GroupButton2.CornerRadius = 15;
			this.GroupButton2.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton2.Location = new System.Drawing.Point(56, 10);
			this.GroupButton2.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton2.Name = "GroupButton2";
			this.GroupButton2.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(123)))));
			this.GroupButton2.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton2.Size = new System.Drawing.Size(22, 24);
			this.GroupButton2.TabIndex = 29;
			this.GroupButton2.Text = "2";
			this.GroupButton2.TextColor = System.Drawing.Color.White;
			this.GroupButton2.UseEllipse = true;
			this.GroupButton2.UseVisualStyleBackColor = true;
			this.GroupButton2.Visible = false;
			// 
			// GroupButton1
			// 
			this.GroupButton1.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(119)))), ((int)(((byte)(174)))));
			this.GroupButton1.CornerRadius = 15;
			this.GroupButton1.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.GroupButton1.Location = new System.Drawing.Point(36, 10);
			this.GroupButton1.Margin = new System.Windows.Forms.Padding(2);
			this.GroupButton1.Name = "GroupButton1";
			this.GroupButton1.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(185)))), ((int)(((byte)(240)))));
			this.GroupButton1.OnHoverTextColor = System.Drawing.Color.Black;
			this.GroupButton1.Size = new System.Drawing.Size(22, 24);
			this.GroupButton1.TabIndex = 28;
			this.GroupButton1.Text = "1";
			this.GroupButton1.TextColor = System.Drawing.Color.White;
			this.GroupButton1.UseEllipse = true;
			this.GroupButton1.UseVisualStyleBackColor = true;
			this.GroupButton1.Visible = false;
			// 
			// DragNDropEmittingButton
			// 
			this.DragNDropEmittingButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(38)))), ((int)(((byte)(47)))));
			this.DragNDropEmittingButton.CornerRadius = 20;
			this.DragNDropEmittingButton.Font = new System.Drawing.Font("font-finance", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DragNDropEmittingButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			this.DragNDropEmittingButton.Location = new System.Drawing.Point(36, 10);
			this.DragNDropEmittingButton.Margin = new System.Windows.Forms.Padding(2);
			this.DragNDropEmittingButton.Name = "DragNDropEmittingButton";
			this.DragNDropEmittingButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(45)))), ((int)(((byte)(56)))));
			this.DragNDropEmittingButton.OnHoverTextColor = System.Drawing.Color.White;
			this.DragNDropEmittingButton.Size = new System.Drawing.Size(22, 24);
			this.DragNDropEmittingButton.TabIndex = 27;
			this.DragNDropEmittingButton.Text = "*";
			this.DragNDropEmittingButton.TextColor = System.Drawing.Color.White;
			this.DragNDropEmittingButton.UseEllipse = true;
			this.DragNDropEmittingButton.UseVisualStyleBackColor = true;
			this.DragNDropEmittingButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DragNDropEmittingButton_MouseDown);
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
			this.Controls.Add(this.GroupButton6);
			this.Controls.Add(this.GroupButton5);
			this.Controls.Add(this.GroupButton4);
			this.Controls.Add(this.GroupButton3);
			this.Controls.Add(this.GroupButton2);
			this.Controls.Add(this.GroupButton1);
			this.Controls.Add(this.DragNDropEmittingButton);
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
			this.Controls.Add(this.scrim);
			this.Name = "FormExample";
			this.Text = "Winform Example";
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}



		#endregion
		private System.Windows.Forms.Label scrim;
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
		private Controls.RoundedButton DragNDropEmittingButton;
		private Controls.RoundedButton GroupButton1;
		private Controls.RoundedButton GroupButton2;
		private Controls.RoundedButton GroupButton3;
		private Controls.RoundedButton GroupButton4;
		private Controls.RoundedButton GroupButton5;
		private Controls.RoundedButton GroupButton6;
		private Controls.DropDown ComponentDropDown;
		private Panel panel2;
	}
}

