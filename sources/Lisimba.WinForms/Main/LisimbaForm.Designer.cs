namespace DustInTheWind.Lisimba.Main
{
    partial class LisimbaForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LisimbaForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contactListView1 = new DustInTheWind.Lisimba.ContactList.ContactListView();
            this.contactEditor1 = new DustInTheWind.Lisimba.ContactEdit.ContactEditor();
            this.labelNoContact = new System.Windows.Forms.Label();
            this.labelNoAddressBook = new System.Windows.Forms.Label();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new DustInTheWind.Lisimba.Utils.BindableToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDefaultGate = new DustInTheWind.Lisimba.Utils.BindableToolStripStatusLabel();
            this.panelAddressBookView = new System.Windows.Forms.Panel();
            this.panelFormContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelNoAddressBook = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonNewAddressBook = new DustInTheWind.Lisimba.Utils.CustomButton();
            this.buttonOpenAddressBook = new DustInTheWind.Lisimba.Utils.CustomButton();
            this.menuStripMain = new DustInTheWind.Lisimba.MainMenu.LisimbaMainMenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonNew = new DustInTheWind.Lisimba.Utils.CustomToolStripButton(this.components);
            this.toolStripButtonOpen = new DustInTheWind.Lisimba.Utils.CustomToolStripButton(this.components);
            this.toolStripButtonSave = new DustInTheWind.Lisimba.Utils.CustomToolStripButton(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonAbout = new DustInTheWind.Lisimba.Utils.CustomToolStripButton(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.panelAddressBookView.SuspendLayout();
            this.panelFormContent.SuspendLayout();
            this.tableLayoutPanelNoAddressBook.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(8, 8);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.contactListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.contactEditor1);
            this.splitContainer1.Panel2.Controls.Add(this.labelNoContact);
            this.splitContainer1.Size = new System.Drawing.Size(727, 463);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // contactListView1
            // 
            this.contactListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactListView1.Location = new System.Drawing.Point(0, 0);
            this.contactListView1.Name = "contactListView1";
            this.contactListView1.Size = new System.Drawing.Size(194, 463);
            this.contactListView1.TabIndex = 9;
            this.contactListView1.ViewModel = null;
            // 
            // contactEditor1
            // 
            this.contactEditor1.BackColor = System.Drawing.SystemColors.Control;
            this.contactEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactEditor1.Enabled = false;
            this.contactEditor1.ForeColor = System.Drawing.Color.Black;
            this.contactEditor1.Location = new System.Drawing.Point(0, 0);
            this.contactEditor1.MinimumSize = new System.Drawing.Size(516, 300);
            this.contactEditor1.Name = "contactEditor1";
            this.contactEditor1.Size = new System.Drawing.Size(525, 463);
            this.contactEditor1.TabIndex = 5;
            this.contactEditor1.ViewModel = null;
            // 
            // labelNoContact
            // 
            this.labelNoContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNoContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNoContact.Location = new System.Drawing.Point(0, 0);
            this.labelNoContact.Name = "labelNoContact";
            this.labelNoContact.Size = new System.Drawing.Size(525, 463);
            this.labelNoContact.TabIndex = 7;
            this.labelNoContact.Text = "Select a contact";
            this.labelNoContact.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNoAddressBook
            // 
            this.labelNoAddressBook.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.labelNoAddressBook.AutoSize = true;
            this.labelNoAddressBook.BackColor = System.Drawing.Color.Transparent;
            this.labelNoAddressBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNoAddressBook.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelNoAddressBook.Location = new System.Drawing.Point(218, 174);
            this.labelNoAddressBook.Margin = new System.Windows.Forms.Padding(10);
            this.labelNoAddressBook.Name = "labelNoAddressBook";
            this.labelNoAddressBook.Size = new System.Drawing.Size(307, 31);
            this.labelNoAddressBook.TabIndex = 6;
            this.labelNoAddressBook.Text = "No address book loaded";
            this.labelNoAddressBook.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus,
            this.toolStripStatusLabel2,
            this.toolStripDefaultGate});
            this.statusStripMain.Location = new System.Drawing.Point(0, 528);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(743, 24);
            this.statusStripMain.TabIndex = 8;
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(84, 19);
            this.toolStripStatus.Text = "toolStripStatus";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(568, 19);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripDefaultGate
            // 
            this.toolStripDefaultGate.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.toolStripDefaultGate.Name = "toolStripDefaultGate";
            this.toolStripDefaultGate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripDefaultGate.Size = new System.Drawing.Size(76, 19);
            this.toolStripDefaultGate.Text = "Default Gate";
            this.toolStripDefaultGate.Click += new System.EventHandler(this.toolStripDefaultGate_Click);
            // 
            // panelAddressBookView
            // 
            this.panelAddressBookView.Controls.Add(this.splitContainer1);
            this.panelAddressBookView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddressBookView.Location = new System.Drawing.Point(0, 0);
            this.panelAddressBookView.Name = "panelAddressBookView";
            this.panelAddressBookView.Padding = new System.Windows.Forms.Padding(8);
            this.panelAddressBookView.Size = new System.Drawing.Size(743, 479);
            this.panelAddressBookView.TabIndex = 9;
            // 
            // panelFormContent
            // 
            this.panelFormContent.Controls.Add(this.panelAddressBookView);
            this.panelFormContent.Controls.Add(this.tableLayoutPanelNoAddressBook);
            this.panelFormContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormContent.Location = new System.Drawing.Point(0, 49);
            this.panelFormContent.Name = "panelFormContent";
            this.panelFormContent.Size = new System.Drawing.Size(743, 479);
            this.panelFormContent.TabIndex = 11;
            // 
            // tableLayoutPanelNoAddressBook
            // 
            this.tableLayoutPanelNoAddressBook.ColumnCount = 1;
            this.tableLayoutPanelNoAddressBook.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelNoAddressBook.Controls.Add(this.labelNoAddressBook, 0, 0);
            this.tableLayoutPanelNoAddressBook.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanelNoAddressBook.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelNoAddressBook.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelNoAddressBook.Name = "tableLayoutPanelNoAddressBook";
            this.tableLayoutPanelNoAddressBook.RowCount = 2;
            this.tableLayoutPanelNoAddressBook.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanelNoAddressBook.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutPanelNoAddressBook.Size = new System.Drawing.Size(743, 479);
            this.tableLayoutPanelNoAddressBook.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.buttonNewAddressBook);
            this.flowLayoutPanel1.Controls.Add(this.buttonOpenAddressBook);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(228, 218);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 70);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // buttonNewAddressBook
            // 
            this.buttonNewAddressBook.AutoSize = true;
            this.buttonNewAddressBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewAddressBook.Image = global::DustInTheWind.Lisimba.Properties.Resources.new_24;
            this.buttonNewAddressBook.Location = new System.Drawing.Point(10, 10);
            this.buttonNewAddressBook.Margin = new System.Windows.Forms.Padding(10);
            this.buttonNewAddressBook.MinimumSize = new System.Drawing.Size(120, 50);
            this.buttonNewAddressBook.Name = "buttonNewAddressBook";
            this.buttonNewAddressBook.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.buttonNewAddressBook.Size = new System.Drawing.Size(120, 50);
            this.buttonNewAddressBook.TabIndex = 8;
            this.buttonNewAddressBook.Text = "New";
            this.buttonNewAddressBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNewAddressBook.UseVisualStyleBackColor = true;
            this.buttonNewAddressBook.ViewModel = null;
            // 
            // buttonOpenAddressBook
            // 
            this.buttonOpenAddressBook.AutoSize = true;
            this.buttonOpenAddressBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenAddressBook.Image = global::DustInTheWind.Lisimba.Properties.Resources.open_24;
            this.buttonOpenAddressBook.Location = new System.Drawing.Point(150, 10);
            this.buttonOpenAddressBook.Margin = new System.Windows.Forms.Padding(10);
            this.buttonOpenAddressBook.MinimumSize = new System.Drawing.Size(120, 50);
            this.buttonOpenAddressBook.Name = "buttonOpenAddressBook";
            this.buttonOpenAddressBook.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.buttonOpenAddressBook.Size = new System.Drawing.Size(126, 50);
            this.buttonOpenAddressBook.TabIndex = 7;
            this.buttonOpenAddressBook.Text = "Open ...";
            this.buttonOpenAddressBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOpenAddressBook.UseVisualStyleBackColor = true;
            this.buttonOpenAddressBook.ViewModel = null;
            // 
            // menuStripMain
            // 
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(743, 24);
            this.menuStripMain.TabIndex = 10;
            this.menuStripMain.Text = "menuStrip2";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonNew,
            this.toolStripButtonOpen,
            this.toolStripButtonSave,
            this.toolStripSeparator1,
            this.toolStripButtonAbout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(743, 25);
            this.toolStrip1.TabIndex = 12;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonNew
            // 
            this.toolStripButtonNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonNew.Image = global::DustInTheWind.Lisimba.Properties.Resources.new_16;
            this.toolStripButtonNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonNew.Name = "toolStripButtonNew";
            this.toolStripButtonNew.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonNew.ViewModel = null;
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonOpen.Image = global::DustInTheWind.Lisimba.Properties.Resources.open_16;
            this.toolStripButtonOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonOpen.ViewModel = null;
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSave.Image = global::DustInTheWind.Lisimba.Properties.Resources.save_16;
            this.toolStripButtonSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonSave.ViewModel = null;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonAbout
            // 
            this.toolStripButtonAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAbout.Image = global::DustInTheWind.Lisimba.Properties.Resources.about_16;
            this.toolStripButtonAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAbout.Name = "toolStripButtonAbout";
            this.toolStripButtonAbout.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonAbout.ViewModel = null;
            // 
            // LisimbaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 552);
            this.Controls.Add(this.panelFormContent);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LisimbaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lisimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LisimbaForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LisimbaForm_FormClosed);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.panelAddressBookView.ResumeLayout(false);
            this.panelFormContent.ResumeLayout(false);
            this.tableLayoutPanelNoAddressBook.ResumeLayout(false);
            this.tableLayoutPanelNoAddressBook.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private DustInTheWind.Lisimba.ContactEdit.ContactEditor contactEditor1;
        private DustInTheWind.Lisimba.Utils.BindableToolStripStatusLabel toolStripStatus;
        private DustInTheWind.Lisimba.ContactList.ContactListView contactListView1;
        private System.Windows.Forms.Panel panelAddressBookView;
        private DustInTheWind.Lisimba.MainMenu.LisimbaMainMenuStrip menuStripMain;
        private System.Windows.Forms.Label labelNoAddressBook;
        private System.Windows.Forms.Label labelNoContact;
        private System.Windows.Forms.Panel panelFormContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelNoAddressBook;
        private DustInTheWind.Lisimba.Utils.CustomButton buttonOpenAddressBook;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DustInTheWind.Lisimba.Utils.CustomButton buttonNewAddressBook;
        private DustInTheWind.Lisimba.Utils.BindableToolStripStatusLabel toolStripDefaultGate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DustInTheWind.Lisimba.Utils.CustomToolStripButton toolStripButtonNew;
        private DustInTheWind.Lisimba.Utils.CustomToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DustInTheWind.Lisimba.Utils.CustomToolStripButton toolStripButtonAbout;
        private DustInTheWind.Lisimba.Utils.CustomToolStripButton toolStripButtonOpen;
    }
}