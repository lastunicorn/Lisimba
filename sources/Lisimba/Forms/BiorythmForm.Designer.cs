namespace DustInTheWind.Lisimba.Forms
{
    partial class BiorythmForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BiorythmForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.biorythm1 = new DustInTheWind.Lisimba.UserControls.Biorythm();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonClose.Location = new System.Drawing.Point(568, 246);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // biorythm1
            // 
            this.biorythm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.biorythm1.BackColor = System.Drawing.SystemColors.Control;
            this.biorythm1.Birthday = new System.DateTime(1980, 6, 13, 0, 0, 0, 0);
            this.biorythm1.CurrentDay = new System.DateTime(2007, 9, 13, 0, 0, 0, 0);
            this.biorythm1.Location = new System.Drawing.Point(12, 12);
            this.biorythm1.Name = "biorythm1";
            this.biorythm1.Size = new System.Drawing.Size(631, 228);
            this.biorythm1.TabIndex = 0;
            // 
            // FormBiorythm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(655, 279);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.biorythm1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormBiorythm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Biorythm";
            this.ResumeLayout(false);

        }

        #endregion

        private DustInTheWind.Lisimba.UserControls.Biorythm biorythm1;
        private System.Windows.Forms.Button buttonClose;


    }
}