
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
			AlwaysOnTopButton = new Controls.RoundedButton();
			DockingButton = new Controls.RoundedButton();
			LinkerButton = new Controls.RoundedButton();
			panel1 = new Panel();
			DataLabel = new Label();
			SourceLabel = new Label();
			SendASymbolLabel = new Label();
			SendButton = new Controls.RoundedButton();
			DataToSendInput = new Controls.FinsembleInput();
			SpawnLabel = new Label();
			LaunchButton = new Controls.RoundedButton();
			ComponentDropDown = new Controls.DropDown();
			MessagesLabel = new Label();
			MessagesRichBox = new RichTextBox();
			panel2 = new Panel();
			panel2.SuspendLayout();
			SuspendLayout();
			// 
			// AlwaysOnTopButton
			// 
			AlwaysOnTopButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			AlwaysOnTopButton.ButtonColor = System.Drawing.Color.FromArgb(34, 38, 47);
			AlwaysOnTopButton.CornerRadius = 20;
			AlwaysOnTopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			AlwaysOnTopButton.ForeColor = System.Drawing.SystemColors.ControlText;
			AlwaysOnTopButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			AlwaysOnTopButton.Location = new System.Drawing.Point(517, 23);
			AlwaysOnTopButton.Margin = new Padding(6);
			AlwaysOnTopButton.Name = "AlwaysOnTopButton";
			AlwaysOnTopButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(40, 45, 56);
			AlwaysOnTopButton.OnHoverTextColor = System.Drawing.Color.White;
			AlwaysOnTopButton.Size = new System.Drawing.Size(15, 24);
			AlwaysOnTopButton.TabIndex = 25;
			AlwaysOnTopButton.Text = "9";
			AlwaysOnTopButton.TextColor = System.Drawing.Color.White;
			AlwaysOnTopButton.UseEllipse = true;
			AlwaysOnTopButton.UseVisualStyleBackColor = true;
			AlwaysOnTopButton.Click += AlwaysOnTopButton_Click;
			// 
			// DockingButton
			// 
			DockingButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			DockingButton.ButtonColor = System.Drawing.Color.FromArgb(34, 38, 47);
			DockingButton.CornerRadius = 20;
			DockingButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			DockingButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			DockingButton.Location = new System.Drawing.Point(490, 23);
			DockingButton.Margin = new Padding(6);
			DockingButton.Name = "DockingButton";
			DockingButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(40, 45, 56);
			DockingButton.OnHoverTextColor = System.Drawing.Color.White;
			DockingButton.Size = new System.Drawing.Size(15, 24);
			DockingButton.TabIndex = 26;
			DockingButton.Text = ">";
			DockingButton.TextColor = System.Drawing.Color.White;
			DockingButton.UseEllipse = true;
			DockingButton.UseVisualStyleBackColor = true;
			DockingButton.Visible = false;
			DockingButton.Click += DockingButton_Click;
			// 
			// LinkerButton
			// 
			LinkerButton.ButtonColor = System.Drawing.Color.FromArgb(34, 38, 47);
			LinkerButton.CornerRadius = 20;
			LinkerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			LinkerButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			LinkerButton.Location = new System.Drawing.Point(20, 19);
			LinkerButton.Margin = new Padding(4);
			LinkerButton.Name = "LinkerButton";
			LinkerButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(40, 45, 56);
			LinkerButton.OnHoverTextColor = System.Drawing.Color.White;
			LinkerButton.Size = new System.Drawing.Size(15, 24);
			LinkerButton.TabIndex = 24;
			LinkerButton.Text = "1";
			LinkerButton.TextColor = System.Drawing.Color.White;
			LinkerButton.UseEllipse = true;
			LinkerButton.UseVisualStyleBackColor = true;
			LinkerButton.Click += LinkerButton_Click;
			// 
			// panel1
			// 
			panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			panel1.BackgroundImage = Resource.winformsLogo;
			panel1.BackgroundImageLayout = ImageLayout.Zoom;
			panel1.Location = new System.Drawing.Point(198, 11);
			panel1.Margin = new Padding(2);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(123, 98);
			panel1.TabIndex = 13;
			// 
			// DataLabel
			// 
			DataLabel.Dock = DockStyle.Bottom;
			DataLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			DataLabel.ForeColor = System.Drawing.Color.White;
			DataLabel.Location = new System.Drawing.Point(0, 143);
			DataLabel.Margin = new Padding(2, 0, 2, 0);
			DataLabel.Name = "DataLabel";
			DataLabel.Size = new System.Drawing.Size(512, 28);
			DataLabel.TabIndex = 14;
			DataLabel.Text = "MSFT";
			DataLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SourceLabel
			// 
			SourceLabel.Dock = DockStyle.Bottom;
			SourceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			SourceLabel.ForeColor = System.Drawing.Color.White;
			SourceLabel.Location = new System.Drawing.Point(0, 171);
			SourceLabel.Margin = new Padding(2, 0, 2, 0);
			SourceLabel.Name = "SourceLabel";
			SourceLabel.Size = new System.Drawing.Size(512, 30);
			SourceLabel.TabIndex = 34;
			SourceLabel.Text = "via default value";
			SourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SendASymbolLabel
			// 
			SendASymbolLabel.AutoSize = true;
			SendASymbolLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			SendASymbolLabel.ForeColor = System.Drawing.Color.FromArgb(3, 155, 255);
			SendASymbolLabel.Location = new System.Drawing.Point(17, 277);
			SendASymbolLabel.Margin = new Padding(2, 0, 2, 0);
			SendASymbolLabel.Name = "SendASymbolLabel";
			SendASymbolLabel.Size = new System.Drawing.Size(116, 17);
			SendASymbolLabel.TabIndex = 10;
			SendASymbolLabel.Text = "Send a Symbol";
			// 
			// SendButton
			// 
			SendButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			SendButton.ButtonColor = System.Drawing.Color.FromArgb(34, 38, 47);
			SendButton.CornerRadius = 25;
			SendButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			SendButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			SendButton.Location = new System.Drawing.Point(440, 277);
			SendButton.Margin = new Padding(2);
			SendButton.Name = "SendButton";
			SendButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(40, 45, 56);
			SendButton.OnHoverTextColor = System.Drawing.Color.White;
			SendButton.Size = new System.Drawing.Size(92, 24);
			SendButton.TabIndex = 8;
			SendButton.Text = "Send";
			SendButton.TextColor = System.Drawing.Color.White;
			SendButton.UseEllipse = false;
			SendButton.UseVisualStyleBackColor = true;
			SendButton.Click += SendButton_Click;
			// 
			// DataToSendInput
			// 
			DataToSendInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			DataToSendInput.AutoSize = true;
			DataToSendInput.BackColor = System.Drawing.Color.FromArgb(23, 26, 32);
			DataToSendInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			DataToSendInput.Location = new System.Drawing.Point(211, 275);
			DataToSendInput.Margin = new Padding(2);
			DataToSendInput.Name = "DataToSendInput";
			DataToSendInput.Size = new System.Drawing.Size(203, 26);
			DataToSendInput.TabIndex = 7;
			// 
			// SpawnLabel
			// 
			SpawnLabel.AutoSize = true;
			SpawnLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			SpawnLabel.ForeColor = System.Drawing.Color.FromArgb(3, 155, 255);
			SpawnLabel.Location = new System.Drawing.Point(20, 355);
			SpawnLabel.Margin = new Padding(2, 0, 2, 0);
			SpawnLabel.Name = "SpawnLabel";
			SpawnLabel.Size = new System.Drawing.Size(155, 17);
			SpawnLabel.TabIndex = 35;
			SpawnLabel.Text = "Spawn a Component";
			// 
			// LaunchButton
			// 
			LaunchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LaunchButton.ButtonColor = System.Drawing.Color.FromArgb(34, 38, 47);
			LaunchButton.CornerRadius = 25;
			LaunchButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			LaunchButton.ImageMargins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
			LaunchButton.Location = new System.Drawing.Point(440, 345);
			LaunchButton.Margin = new Padding(2);
			LaunchButton.Name = "LaunchButton";
			LaunchButton.OnHoverButtonColor = System.Drawing.Color.FromArgb(40, 45, 56);
			LaunchButton.OnHoverTextColor = System.Drawing.Color.White;
			LaunchButton.Size = new System.Drawing.Size(92, 27);
			LaunchButton.TabIndex = 36;
			LaunchButton.Text = "Launch";
			LaunchButton.TextColor = System.Drawing.Color.White;
			LaunchButton.UseEllipse = false;
			LaunchButton.UseVisualStyleBackColor = true;
			LaunchButton.Click += LaunchButton_Click;
			// 
			// ComponentDropDown
			// 
			ComponentDropDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			ComponentDropDown.BackColor = System.Drawing.Color.FromArgb(34, 38, 47);
			ComponentDropDown.DrawMode = DrawMode.OwnerDrawVariable;
			ComponentDropDown.DropDownStyle = ComboBoxStyle.DropDownList;
			ComponentDropDown.FlatStyle = FlatStyle.Popup;
			ComponentDropDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			ComponentDropDown.Location = new System.Drawing.Point(211, 345);
			ComponentDropDown.Margin = new Padding(2);
			ComponentDropDown.MaxDropDownItems = 10;
			ComponentDropDown.Name = "ComponentDropDown";
			ComponentDropDown.Size = new System.Drawing.Size(203, 27);
			ComponentDropDown.TabIndex = 35;
			// 
			// MessagesLabel
			// 
			MessagesLabel.AutoSize = true;
			MessagesLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			MessagesLabel.ForeColor = System.Drawing.Color.FromArgb(3, 155, 255);
			MessagesLabel.Location = new System.Drawing.Point(20, 402);
			MessagesLabel.Margin = new Padding(2, 0, 2, 0);
			MessagesLabel.Name = "MessagesLabel";
			MessagesLabel.Size = new System.Drawing.Size(80, 17);
			MessagesLabel.TabIndex = 37;
			MessagesLabel.Text = "Messages";
			// 
			// MessagesRichBox
			// 
			MessagesRichBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			MessagesRichBox.BackColor = System.Drawing.Color.FromArgb(23, 26, 32);
			MessagesRichBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			MessagesRichBox.ForeColor = System.Drawing.Color.White;
			MessagesRichBox.Location = new System.Drawing.Point(20, 421);
			MessagesRichBox.Margin = new Padding(2);
			MessagesRichBox.Name = "MessagesRichBox";
			MessagesRichBox.Size = new System.Drawing.Size(512, 152);
			MessagesRichBox.TabIndex = 22;
			MessagesRichBox.Text = "";
			// 
			// panel2
			// 
			panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			panel2.Controls.Add(DataLabel);
			panel2.Controls.Add(panel1);
			panel2.Controls.Add(SourceLabel);
			panel2.Location = new System.Drawing.Point(20, 62);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(512, 201);
			panel2.TabIndex = 39;
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = System.Drawing.Color.FromArgb(23, 26, 32);
			ClientSize = new System.Drawing.Size(552, 584);
			Controls.Add(panel2);
			Controls.Add(ComponentDropDown);
			Controls.Add(MessagesRichBox);
			Controls.Add(DataToSendInput);
			Controls.Add(MessagesLabel);
			Controls.Add(LaunchButton);
			Controls.Add(SpawnLabel);
			Controls.Add(SendButton);
			Controls.Add(SendASymbolLabel);
			Controls.Add(LinkerButton);
			Controls.Add(DockingButton);
			Controls.Add(AlwaysOnTopButton);
			Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			Name = "MainForm";
			Text = "Winform Example Core";
			FormClosed += MainForm_FormClosed;
			panel2.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
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

