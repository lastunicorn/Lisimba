namespace DustInTheWind.Lisimba.WinForms.Biorhythm
{
    partial class BiorhythmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BiorhythmForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.biorhythm1 = new BiorhythmView();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(760, 260);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // biorhythm1
            // 
            this.biorhythm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.biorhythm1.BackColor = System.Drawing.SystemColors.Control;
            this.biorhythm1.Birthday = new System.DateTime(2016, 1, 26, 1, 31, 10, 548);
            this.biorhythm1.CurrentDay = new System.DateTime(2016, 1, 26, 1, 31, 10, 549);
            this.biorhythm1.Location = new System.Drawing.Point(12, 12);
            this.biorhythm1.Name = "biorhythm1";
            this.biorhythm1.Size = new System.Drawing.Size(823, 242);
            this.biorhythm1.TabIndex = 0;
            // 
            // BiorhythmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 293);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.biorhythm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BiorhythmForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biorhythm";
            this.ResumeLayout(false);

        }

        #endregion

        private BiorhythmView biorhythm1;
        private System.Windows.Forms.Button buttonClose;


    }
}