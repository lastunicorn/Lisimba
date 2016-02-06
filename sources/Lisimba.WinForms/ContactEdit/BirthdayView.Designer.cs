namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class BirthdayView
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
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Controls.Add(this.labelBirthday);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(69, 23);
            this.flowLayoutPanel2.TabIndex = 16;
            this.flowLayoutPanel2.WrapContents = false;
            this.flowLayoutPanel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HandleFlowLayoutPanelMouseClick);
            this.flowLayoutPanel2.MouseEnter += new System.EventHandler(this.HandleFlowLayoutPanelMouseEnter);
            this.flowLayoutPanel2.MouseLeave += new System.EventHandler(this.HandleFlowLayoutPanelMouseLeave);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 5);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Birthday:";
            this.label7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HandleLabelMouseClick);
            this.label7.MouseEnter += new System.EventHandler(this.HandleLabelMouseEnter);
            this.label7.MouseLeave += new System.EventHandler(this.HandleLabelMouseLeave);
            // 
            // labelBirthday
            // 
            this.labelBirthday.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Location = new System.Drawing.Point(66, 5);
            this.labelBirthday.Margin = new System.Windows.Forms.Padding(0);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(0, 13);
            this.labelBirthday.TabIndex = 13;
            this.labelBirthday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelBirthday.MouseClick += new System.Windows.Forms.MouseEventHandler(this.HandleLabelBirthdayMouseClick);
            this.labelBirthday.MouseEnter += new System.EventHandler(this.HandleLabelBirthdayMouseEnter);
            this.labelBirthday.MouseLeave += new System.EventHandler(this.HandleLabelBirthdayMouseLeave);
            // 
            // BirthdayView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.flowLayoutPanel2);
            this.Name = "BirthdayView";
            this.Size = new System.Drawing.Size(69, 23);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelBirthday;
    }
}
