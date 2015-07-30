namespace DustInTheWind.Lisimba.ContactEdit
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
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panelOuterContainer = new System.Windows.Forms.Panel();
            this.panelInnerContainer = new System.Windows.Forms.Panel();
            this.flowLayoutPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.panelOuterContainer.SuspendLayout();
            this.flowLayoutPanelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.AutoSize = true;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOk.Location = new System.Drawing.Point(196, 0);
            this.buttonOk.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 0;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.HandleButtonOkClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(274, 0);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.HandleButtonCancelClick);
            // 
            // panelOuterContainer
            // 
            this.panelOuterContainer.BackColor = System.Drawing.SystemColors.Control;
            this.panelOuterContainer.Controls.Add(this.panelInnerContainer);
            this.panelOuterContainer.Controls.Add(this.flowLayoutPanelButtons);
            this.panelOuterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOuterContainer.Location = new System.Drawing.Point(0, 0);
            this.panelOuterContainer.Name = "panelOuterContainer";
            this.panelOuterContainer.Padding = new System.Windows.Forms.Padding(6);
            this.panelOuterContainer.Size = new System.Drawing.Size(361, 120);
            this.panelOuterContainer.TabIndex = 9;
            // 
            // panelInnerContainer
            // 
            this.panelInnerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelInnerContainer.Location = new System.Drawing.Point(6, 6);
            this.panelInnerContainer.Name = "panelInnerContainer";
            this.panelInnerContainer.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelInnerContainer.Size = new System.Drawing.Size(349, 85);
            this.panelInnerContainer.TabIndex = 9;
            // 
            // flowLayoutPanelButtons
            // 
            this.flowLayoutPanelButtons.AutoSize = true;
            this.flowLayoutPanelButtons.Controls.Add(this.buttonCancel);
            this.flowLayoutPanelButtons.Controls.Add(this.buttonOk);
            this.flowLayoutPanelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanelButtons.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelButtons.Location = new System.Drawing.Point(6, 91);
            this.flowLayoutPanelButtons.Name = "flowLayoutPanelButtons";
            this.flowLayoutPanelButtons.Size = new System.Drawing.Size(349, 23);
            this.flowLayoutPanelButtons.TabIndex = 8;
            // 
            // EditBaseForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(361, 120);
            this.Controls.Add(this.panelOuterContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditBaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit";
            this.Activated += new System.EventHandler(this.FormEditBase_Activated);
            this.Deactivate += new System.EventHandler(this.FormEditBase_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditBase_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HandleFormKeyDown);
            this.panelOuterContainer.ResumeLayout(false);
            this.panelOuterContainer.PerformLayout();
            this.flowLayoutPanelButtons.ResumeLayout(false);
            this.flowLayoutPanelButtons.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelOuterContainer;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelButtons;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        protected System.Windows.Forms.Panel panelInnerContainer;
    }
}