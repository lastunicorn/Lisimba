﻿namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class EditBaseForm
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
            this.SuspendLayout();
            // 
            // FormEditBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 217);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormEditBase";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit";
            this.Deactivate += new System.EventHandler(this.FormEditBase_Deactivate);
            this.Activated += new System.EventHandler(this.FormEditBase_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditBase_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormEditBase_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}