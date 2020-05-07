using ChartIQ.Finsemble;
using System;
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
		private DateTime finsembleRequestedCloseAt = DateTime.MinValue;


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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(123, 30);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(180, 20);
			this.textBox1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(23, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Spawn Data";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(26, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "PubSub Data";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(123, 72);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(180, 20);
			this.textBox2.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Name = "FormExample";
			this.Text = "FormExample";
			this.ResumeLayout(false);
			this.PerformLayout();

			//setup FormClosing event handler
			this.FormClosing += FormExample_FormClosing;
		}

		private void FormExample_FormClosing(object sender, FormClosingEventArgs e)
		{
			//did user close close in the last second
			TimeSpan timeDiff = DateTime.UtcNow - finsembleRequestedCloseAt;
			
			//check if Finsemble requested a window close in the last few seconds
			if (timeDiff.TotalMilliseconds < 3000.0) {
				//jsut cleanup and don't show a dialog

			} else
			{
				const string message =
				"Are you sure that you would like to close the form?";
				const string caption = "Form Closing";
				var result = MessageBox.Show(message, caption,
											 MessageBoxButtons.YesNo,
											 MessageBoxIcon.Question);

				// If the no button was pressed ...
				if (result == DialogResult.No)
				{
					// cancel the closure of the form.
					e.Cancel = true;
				}
			}
		}


		#endregion

		private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
    }
}

