namespace DustInTheWind.Lisimba.WinForms.Forms
{
    partial class GateSelectorForm
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
            this.gateSelector1 = new GateSelector();
            this.SuspendLayout();
            // 
            // gateSelector1
            // 
            this.gateSelector1.Gates = null;
            this.gateSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gateSelector1.Location = new System.Drawing.Point(0, 0);
            this.gateSelector1.Name = "gateSelector1";
            this.gateSelector1.Size = new System.Drawing.Size(213, 279);
            this.gateSelector1.TabIndex = 0;
            this.gateSelector1.GateSelected += new System.EventHandler(this.gateSelector1_GateSelected);
            // 
            // GateSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(213, 279);
            this.ControlBox = false;
            this.Controls.Add(this.gateSelector1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GateSelectorForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Select Gate";
            this.Deactivate += new System.EventHandler(this.GateSelectorForm_Deactivate);
            this.ResumeLayout(false);

        }

        #endregion

        private GateSelector gateSelector1;

    }
}