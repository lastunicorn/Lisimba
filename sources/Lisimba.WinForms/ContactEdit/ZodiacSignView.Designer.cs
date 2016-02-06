namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class ZodiacSignView
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxZodiacSign = new System.Windows.Forms.PictureBox();
            this.labelZodiacSign = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZodiacSign)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.pictureBoxZodiacSign);
            this.flowLayoutPanel1.Controls.Add(this.labelZodiacSign);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(67, 58);
            this.flowLayoutPanel1.TabIndex = 15;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pictureBoxZodiacSign
            // 
            this.pictureBoxZodiacSign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxZodiacSign.Location = new System.Drawing.Point(14, 3);
            this.pictureBoxZodiacSign.Name = "pictureBoxZodiacSign";
            this.pictureBoxZodiacSign.Size = new System.Drawing.Size(39, 39);
            this.pictureBoxZodiacSign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxZodiacSign.TabIndex = 10;
            this.pictureBoxZodiacSign.TabStop = false;
            // 
            // labelZodiacSign
            // 
            this.labelZodiacSign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelZodiacSign.AutoSize = true;
            this.labelZodiacSign.Location = new System.Drawing.Point(3, 45);
            this.labelZodiacSign.Name = "labelZodiacSign";
            this.labelZodiacSign.Size = new System.Drawing.Size(61, 13);
            this.labelZodiacSign.TabIndex = 11;
            this.labelZodiacSign.Text = "ZodiacSign";
            this.labelZodiacSign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ZodiacSignView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "ZodiacSignView";
            this.Size = new System.Drawing.Size(67, 58);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZodiacSign)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxZodiacSign;
        private System.Windows.Forms.Label labelZodiacSign;
    }
}
