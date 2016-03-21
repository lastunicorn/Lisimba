namespace DustInTheWind.Lisimba.WinForms.Forms
{
    partial class Form2
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
            this.contactEditor21 = new DustInTheWind.Lisimba.WinForms.ContactEdit.ContactEditor2();
            this.SuspendLayout();
            // 
            // contactEditor21
            // 
            this.contactEditor21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactEditor21.Location = new System.Drawing.Point(0, 0);
            this.contactEditor21.Name = "contactEditor21";
            this.contactEditor21.Size = new System.Drawing.Size(830, 530);
            this.contactEditor21.TabIndex = 0;
            this.contactEditor21.ViewModel = null;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 530);
            this.Controls.Add(this.contactEditor21);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private ContactEdit.ContactEditor2 contactEditor21;
    }
}