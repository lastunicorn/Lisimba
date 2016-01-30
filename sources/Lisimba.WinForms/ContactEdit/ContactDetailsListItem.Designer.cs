namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class ContactDetailsListItem
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
            this.tableLayoutPanelHeader = new System.Windows.Forms.TableLayoutPanel();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.panelItems = new System.Windows.Forms.Panel();
            this.tableLayoutPanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanelHeader
            // 
            this.tableLayoutPanelHeader.AutoSize = true;
            this.tableLayoutPanelHeader.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.tableLayoutPanelHeader.ColumnCount = 3;
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelHeader.Controls.Add(this.buttonAdd, 2, 0);
            this.tableLayoutPanelHeader.Controls.Add(this.labelTitle, 1, 0);
            this.tableLayoutPanelHeader.Controls.Add(this.pictureBoxIcon, 0, 0);
            this.tableLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanelHeader.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelHeader.Name = "tableLayoutPanelHeader";
            this.tableLayoutPanelHeader.RowCount = 2;
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelHeader.Size = new System.Drawing.Size(300, 34);
            this.tableLayoutPanelHeader.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonAdd.Location = new System.Drawing.Point(269, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(28, 28);
            this.buttonAdd.TabIndex = 0;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTitle.AutoEllipsis = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(42, 3);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(3);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(221, 28);
            this.labelTitle.TabIndex = 1;
            this.labelTitle.Text = "Title";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelTitle.Resize += new System.EventHandler(this.labelTitle_Resize);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBoxIcon.Location = new System.Drawing.Point(8, 3);
            this.pictureBoxIcon.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(28, 28);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxIcon.TabIndex = 2;
            this.pictureBoxIcon.TabStop = false;
            // 
            // panelItems
            // 
            this.panelItems.AutoSize = true;
            this.panelItems.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelItems.Location = new System.Drawing.Point(0, 34);
            this.panelItems.Name = "panelItems";
            this.panelItems.Size = new System.Drawing.Size(300, 0);
            this.panelItems.TabIndex = 1;
            // 
            // ContactDetailsListItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.panelItems);
            this.Controls.Add(this.tableLayoutPanelHeader);
            this.MinimumSize = new System.Drawing.Size(300, 34);
            this.Name = "ContactDetailsListItem";
            this.Size = new System.Drawing.Size(300, 34);
            this.Resize += new System.EventHandler(this.ContactDetailsListItem_Resize);
            this.tableLayoutPanelHeader.ResumeLayout(false);
            this.tableLayoutPanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelHeader;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Panel panelItems;
    }
}
