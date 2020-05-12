using ChartIQ.Finsemble;
using System;


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
			this.datasource = new System.Windows.Forms.Label();
			this.datavalue = new System.Windows.Forms.Label();
			this.input = new System.Windows.Forms.TextBox();
			this.pubsub = new System.Windows.Forms.Button();
			this.linker = new System.Windows.Forms.Button();
			this.pubLinker = new System.Windows.Forms.Button();
			this.group1 = new System.Windows.Forms.Label();
			this.group2 = new System.Windows.Forms.Label();
			this.group3 = new System.Windows.Forms.Label();
			this.group4 = new System.Windows.Forms.Label();
			this.group5 = new System.Windows.Forms.Label();
			this.group6 = new System.Windows.Forms.Label();
			this.componentList = new System.Windows.Forms.ComboBox();
			this.spawnBtn = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// scrim
			// 
			this.scrim.AllowDrop = true;
			this.scrim.BackColor = System.Drawing.Color.Transparent;
			this.scrim.Dock = System.Windows.Forms.DockStyle.Fill;
			this.scrim.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.scrim.ForeColor = System.Drawing.Color.Transparent;
			this.scrim.Location = new System.Drawing.Point(0, 0);
			this.scrim.Name = "scrim";
			this.scrim.Size = new System.Drawing.Size(384, 393);
			this.scrim.TabIndex = 4;
			this.scrim.Visible = false;
			// 
			// datasource
			// 
			this.datasource.AutoSize = true;
			this.datasource.Location = new System.Drawing.Point(9, 377);
			this.datasource.Name = "datasource";
			this.datasource.Size = new System.Drawing.Size(60, 13);
			this.datasource.TabIndex = 6;
			this.datasource.Text = "datasource";
			// 
			// datavalue
			// 
			this.datavalue.AutoSize = true;
			this.datavalue.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.datavalue.Location = new System.Drawing.Point(7, 339);
			this.datavalue.Name = "datavalue";
			this.datavalue.Size = new System.Drawing.Size(106, 25);
			this.datavalue.TabIndex = 7;
			this.datavalue.Text = "datavalue";
			// 
			// input
			// 
			this.input.Location = new System.Drawing.Point(4, 47);
			this.input.Name = "input";
			this.input.Size = new System.Drawing.Size(115, 20);
			this.input.TabIndex = 8;
			this.input.Text = "AAPL";
			this.input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.input_MouseDown);
			// 
			// pubsub
			// 
			this.pubsub.Location = new System.Drawing.Point(4, 73);
			this.pubsub.Name = "pubsub";
			this.pubsub.Size = new System.Drawing.Size(116, 23);
			this.pubsub.TabIndex = 9;
			this.pubsub.Text = "Publish to PubSub";
			this.pubsub.UseVisualStyleBackColor = true;
			this.pubsub.Click += new System.EventHandler(this.pubsub_Click);
			// 
			// linker
			// 
			this.linker.Location = new System.Drawing.Point(0, 0);
			this.linker.Name = "linker";
			this.linker.Size = new System.Drawing.Size(44, 23);
			this.linker.TabIndex = 10;
			this.linker.Text = "Linker";
			this.linker.UseVisualStyleBackColor = true;
			this.linker.Click += new System.EventHandler(this.linker_Click);
			// 
			// pubLinker
			// 
			this.pubLinker.Location = new System.Drawing.Point(4, 102);
			this.pubLinker.Name = "pubLinker";
			this.pubLinker.Size = new System.Drawing.Size(116, 23);
			this.pubLinker.TabIndex = 11;
			this.pubLinker.Text = "Publish to Linker";
			this.pubLinker.UseVisualStyleBackColor = true;
			this.pubLinker.Click += new System.EventHandler(this.pubLinker_Click);
			// 
			// group1
			// 
			this.group1.AutoSize = true;
			this.group1.BackColor = System.Drawing.Color.Red;
			this.group1.Location = new System.Drawing.Point(50, 0);
			this.group1.Name = "group1";
			this.group1.Size = new System.Drawing.Size(13, 13);
			this.group1.TabIndex = 12;
			this.group1.Text = "1";
			// 
			// group2
			// 
			this.group2.AutoSize = true;
			this.group2.BackColor = System.Drawing.Color.Red;
			this.group2.Location = new System.Drawing.Point(69, 0);
			this.group2.Name = "group2";
			this.group2.Size = new System.Drawing.Size(13, 13);
			this.group2.TabIndex = 13;
			this.group2.Text = "2";
			// 
			// group3
			// 
			this.group3.AutoSize = true;
			this.group3.BackColor = System.Drawing.Color.Red;
			this.group3.Location = new System.Drawing.Point(88, 0);
			this.group3.Name = "group3";
			this.group3.Size = new System.Drawing.Size(13, 13);
			this.group3.TabIndex = 14;
			this.group3.Text = "3";
			// 
			// group4
			// 
			this.group4.AutoSize = true;
			this.group4.BackColor = System.Drawing.Color.Red;
			this.group4.Location = new System.Drawing.Point(107, 0);
			this.group4.Name = "group4";
			this.group4.Size = new System.Drawing.Size(13, 13);
			this.group4.TabIndex = 15;
			this.group4.Text = "4";
			// 
			// group5
			// 
			this.group5.AutoSize = true;
			this.group5.BackColor = System.Drawing.Color.Red;
			this.group5.Location = new System.Drawing.Point(126, 0);
			this.group5.Name = "group5";
			this.group5.Size = new System.Drawing.Size(13, 13);
			this.group5.TabIndex = 16;
			this.group5.Text = "5";
			// 
			// group6
			// 
			this.group6.AutoSize = true;
			this.group6.BackColor = System.Drawing.Color.Red;
			this.group6.Location = new System.Drawing.Point(145, 0);
			this.group6.Name = "group6";
			this.group6.Size = new System.Drawing.Size(13, 13);
			this.group6.TabIndex = 17;
			this.group6.Text = "6";
			// 
			// componentList
			// 
			this.componentList.FormattingEnabled = true;
			this.componentList.Location = new System.Drawing.Point(4, 132);
			this.componentList.Name = "componentList";
			this.componentList.Size = new System.Drawing.Size(116, 21);
			this.componentList.TabIndex = 18;
			// 
			// spawnBtn
			// 
			this.spawnBtn.Location = new System.Drawing.Point(4, 159);
			this.spawnBtn.Name = "spawnBtn";
			this.spawnBtn.Size = new System.Drawing.Size(116, 23);
			this.spawnBtn.TabIndex = 19;
			this.spawnBtn.Text = "Spawn";
			this.spawnBtn.UseVisualStyleBackColor = true;
			this.spawnBtn.Click += new System.EventHandler(this.spawnBtn_Click);
			// 
			// FormExample
			// 
			this.AllowDrop = true;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(384, 393);
			this.Controls.Add(this.spawnBtn);
			this.Controls.Add(this.componentList);
			this.Controls.Add(this.group6);
			this.Controls.Add(this.group5);
			this.Controls.Add(this.group4);
			this.Controls.Add(this.group3);
			this.Controls.Add(this.group2);
			this.Controls.Add(this.group1);
			this.Controls.Add(this.pubLinker);
			this.Controls.Add(this.linker);
			this.Controls.Add(this.pubsub);
			this.Controls.Add(this.input);
			this.Controls.Add(this.datavalue);
			this.Controls.Add(this.datasource);
			this.Controls.Add(this.scrim);
			this.Name = "FormExample";
			this.Text = "Winform Example";
			this.ResumeLayout(false);
			this.PerformLayout();

        }



        #endregion
		private System.Windows.Forms.Label scrim;
		private System.Windows.Forms.Label datasource;
		private System.Windows.Forms.Label datavalue;
		private System.Windows.Forms.TextBox input;
		private System.Windows.Forms.Button pubsub;
		private System.Windows.Forms.Button linker;
		private System.Windows.Forms.Button pubLinker;
		private System.Windows.Forms.Label group1;
		private System.Windows.Forms.Label group2;
		private System.Windows.Forms.Label group3;
		private System.Windows.Forms.Label group4;
		private System.Windows.Forms.Label group5;
		private System.Windows.Forms.Label group6;
		private System.Windows.Forms.ComboBox componentList;
		private System.Windows.Forms.Button spawnBtn;
	}
}

