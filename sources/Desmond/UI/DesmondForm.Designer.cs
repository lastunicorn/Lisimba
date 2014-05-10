namespace DustInTheWind.Desmond.UI
{
    partial class DesmondForm
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
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelStartedLed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelStartedInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Location = new System.Drawing.Point(117, 12);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(99, 23);
            this.buttonStop.TabIndex = 3;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 12);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(99, 23);
            this.buttonStart.TabIndex = 2;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelStartedLed,
            this.toolStripStatusLabelStartedInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 48);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(227, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelStartedLed
            // 
            this.toolStripStatusLabelStartedLed.AutoSize = false;
            this.toolStripStatusLabelStartedLed.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusLabelStartedLed.Margin = new System.Windows.Forms.Padding(12, 5, 2, 4);
            this.toolStripStatusLabelStartedLed.Name = "toolStripStatusLabelStartedLed";
            this.toolStripStatusLabelStartedLed.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripStatusLabelStartedLed.Size = new System.Drawing.Size(13, 13);
            // 
            // toolStripStatusLabelStartedInfo
            // 
            this.toolStripStatusLabelStartedInfo.Name = "toolStripStatusLabelStartedInfo";
            this.toolStripStatusLabelStartedInfo.Size = new System.Drawing.Size(185, 17);
            this.toolStripStatusLabelStartedInfo.Spring = true;
            this.toolStripStatusLabelStartedInfo.Text = "Stopped";
            this.toolStripStatusLabelStartedInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DesmondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 70);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DesmondForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Desmond";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStartedLed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelStartedInfo;
    }
}