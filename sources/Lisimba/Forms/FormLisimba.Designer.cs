using DustInTheWind.Lisimba.ContactEdit;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Forms
{
    partial class FormLisimba
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLisimba));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelNoContact = new System.Windows.Forms.Label();
            this.labelNoAddressBook = new System.Windows.Forms.Label();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.panelAddressBookView = new System.Windows.Forms.Panel();
            this.panelFormContent = new System.Windows.Forms.Panel();
            this.tableLayoutPanelNoAddressBook = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonNewAddressBook = new DustInTheWind.Lisimba.UserControls.CustomButton();
            this.buttonOpenAddressBook = new DustInTheWind.Lisimba.UserControls.CustomButton();
            this.contactListView1 = new DustInTheWind.Lisimba.UserControls.ContactListView();
            this.contactView1 = new DustInTheWind.Lisimba.ContactEdit.ContactEditor();
            this.toolStripStatusLabel1 = new DustInTheWind.Lisimba.UserControls.BindableToolStripStatusLabel();
            this.menuStripMain = new DustInTheWind.Lisimba.UserControls.LisimbaMainMenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.panelAddressBookView.SuspendLayout();
            this.panelFormContent.SuspendLayout();
            this.tableLayoutPanelNoAddressBook.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
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
            this.splitContainer1.Panel2.Controls.Add(this.contactView1);
            this.splitContainer1.Panel2.Controls.Add(this.labelNoContact);
            this.splitContainer1.Size = new System.Drawing.Size(727, 490);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // labelNoContact
            // 
            this.labelNoContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNoContact.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNoContact.Location = new System.Drawing.Point(0, 0);
            this.labelNoContact.Name = "labelNoContact";
            this.labelNoContact.Size = new System.Drawing.Size(525, 490);
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
            this.labelNoAddressBook.Location = new System.Drawing.Point(218, 186);
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
            this.toolStripStatusLabel1});
            this.statusStripMain.Location = new System.Drawing.Point(0, 530);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(743, 22);
            this.statusStripMain.TabIndex = 8;
            // 
            // panelAddressBookView
            // 
            this.panelAddressBookView.Controls.Add(this.splitContainer1);
            this.panelAddressBookView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddressBookView.Location = new System.Drawing.Point(0, 0);
            this.panelAddressBookView.Name = "panelAddressBookView";
            this.panelAddressBookView.Padding = new System.Windows.Forms.Padding(8);
            this.panelAddressBookView.Size = new System.Drawing.Size(743, 506);
            this.panelAddressBookView.TabIndex = 9;
            // 
            // panelFormContent
            // 
            this.panelFormContent.Controls.Add(this.panelAddressBookView);
            this.panelFormContent.Controls.Add(this.tableLayoutPanelNoAddressBook);
            this.panelFormContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormContent.Location = new System.Drawing.Point(0, 24);
            this.panelFormContent.Name = "panelFormContent";
            this.panelFormContent.Size = new System.Drawing.Size(743, 506);
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
            this.tableLayoutPanelNoAddressBook.Size = new System.Drawing.Size(743, 506);
            this.tableLayoutPanelNoAddressBook.TabIndex = 10;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this.buttonNewAddressBook);
            this.flowLayoutPanel1.Controls.Add(this.buttonOpenAddressBook);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(228, 230);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 70);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // buttonNewAddressBook
            // 
            this.buttonNewAddressBook.ApplicationStatus = null;
            this.buttonNewAddressBook.AutoSize = true;
            this.buttonNewAddressBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonNewAddressBook.Image = global::DustInTheWind.Lisimba.Properties.Resources.new_24;
            this.buttonNewAddressBook.Location = new System.Drawing.Point(10, 10);
            this.buttonNewAddressBook.Margin = new System.Windows.Forms.Padding(10);
            this.buttonNewAddressBook.MinimumSize = new System.Drawing.Size(120, 50);
            this.buttonNewAddressBook.Name = "buttonNewAddressBook";
            this.buttonNewAddressBook.Opertion = null;
            this.buttonNewAddressBook.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.buttonNewAddressBook.Size = new System.Drawing.Size(120, 50);
            this.buttonNewAddressBook.TabIndex = 8;
            this.buttonNewAddressBook.Text = "New";
            this.buttonNewAddressBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonNewAddressBook.UseVisualStyleBackColor = true;
            // 
            // buttonOpenAddressBook
            // 
            this.buttonOpenAddressBook.ApplicationStatus = null;
            this.buttonOpenAddressBook.AutoSize = true;
            this.buttonOpenAddressBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenAddressBook.Image = global::DustInTheWind.Lisimba.Properties.Resources.open_24;
            this.buttonOpenAddressBook.Location = new System.Drawing.Point(150, 10);
            this.buttonOpenAddressBook.Margin = new System.Windows.Forms.Padding(10);
            this.buttonOpenAddressBook.MinimumSize = new System.Drawing.Size(120, 50);
            this.buttonOpenAddressBook.Name = "buttonOpenAddressBook";
            this.buttonOpenAddressBook.Opertion = null;
            this.buttonOpenAddressBook.Padding = new System.Windows.Forms.Padding(10, 4, 10, 4);
            this.buttonOpenAddressBook.Size = new System.Drawing.Size(126, 50);
            this.buttonOpenAddressBook.TabIndex = 7;
            this.buttonOpenAddressBook.Text = "Open ...";
            this.buttonOpenAddressBook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonOpenAddressBook.UseVisualStyleBackColor = true;
            // 
            // contactListView1
            // 
            this.contactListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactListView1.Location = new System.Drawing.Point(0, 0);
            this.contactListView1.Name = "contactListView1";
            this.contactListView1.Size = new System.Drawing.Size(194, 490);
            this.contactListView1.TabIndex = 9;
            this.contactListView1.ViewModel = null;
            // 
            // contactView1
            // 
            this.contactView1.BackColor = System.Drawing.SystemColors.Control;
            this.contactView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactView1.Enabled = false;
            this.contactView1.ForeColor = System.Drawing.Color.Black;
            this.contactView1.Location = new System.Drawing.Point(0, 0);
            this.contactView1.MinimumSize = new System.Drawing.Size(516, 300);
            this.contactView1.Name = "contactView1";
            this.contactView1.Size = new System.Drawing.Size(525, 490);
            this.contactView1.TabIndex = 5;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 15);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // menuStripMain
            // 
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(743, 24);
            this.menuStripMain.TabIndex = 10;
            this.menuStripMain.Text = "menuStrip2";
            // 
            // FormLisimba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 552);
            this.Controls.Add(this.panelFormContent);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.menuStripMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLisimba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lisimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.Shown += new System.EventHandler(this.HandleFormShown);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private ContactEditor contactView1;
        private BindableToolStripStatusLabel toolStripStatusLabel1;
        private ContactListView contactListView1;
        private System.Windows.Forms.Panel panelAddressBookView;
        private LisimbaMainMenuStrip menuStripMain;
        private System.Windows.Forms.Label labelNoAddressBook;
        private System.Windows.Forms.Label labelNoContact;
        private System.Windows.Forms.Panel panelFormContent;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelNoAddressBook;
        private DustInTheWind.Lisimba.UserControls.CustomButton buttonOpenAddressBook;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private DustInTheWind.Lisimba.UserControls.CustomButton buttonNewAddressBook;
    }
}