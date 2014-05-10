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
            this.contactListView1 = new DustInTheWind.Lisimba.UserControls.ContactListView();
            this.contactView1 = new DustInTheWind.Lisimba.UserControls.ContactView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem_File = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_File_New = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_File_Open = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_File_Save = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_File_SaveAs = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Export = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_ExportToYahooCSV = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_File_Import = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_ImportFromYahooCSV = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_RecentFiles = new DustInTheWind.Lisimba.UserControls.MenuItemWithChildren();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Exit = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_Agenda = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_Agenda_AddContact = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_Agenda_DeleteContact = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_Agenda_Properties = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_Options = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_Help = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.toolStripMenuItem_Help_About = new DustInTheWind.Lisimba.UserControls.CustomMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(12, 36);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.contactListView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.contactView1);
            this.splitContainer1.Size = new System.Drawing.Size(719, 407);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // contactListView1
            // 
            this.contactListView1.AllowSort = true;
            this.contactListView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contactListView1.Contacts = null;
            this.contactListView1.Location = new System.Drawing.Point(0, 0);
            this.contactListView1.Name = "contactListView1";
            this.contactListView1.SearchText = "";
            this.contactListView1.Size = new System.Drawing.Size(191, 407);
            this.contactListView1.SortField = DustInTheWind.Lisimba.Egg.Enums.ContactsSortingType.NicknameOrName;
            this.contactListView1.TabIndex = 9;
            this.contactListView1.SelectedContactChanged += new DustInTheWind.Lisimba.UserControls.ContactListView.SelectedContactChangedHandler(this.contactListView1_SelectedContactChanged);
            // 
            // contactView1
            // 
            this.contactView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contactView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(233)))), ((int)(((byte)(216)))));
            this.contactView1.Birthday = "";
            this.contactView1.CheckMandatoryFields = true;
            this.contactView1.Contact = null;
            this.contactView1.Enabled = false;
            this.contactView1.FirstName = "";
            this.contactView1.ForeColor = System.Drawing.Color.Black;
            this.contactView1.LastName = "";
            this.contactView1.Location = new System.Drawing.Point(3, 0);
            this.contactView1.MiddleName = "";
            this.contactView1.MinimumSize = new System.Drawing.Size(400, 300);
            this.contactView1.Name = "contactView1";
            this.contactView1.Nickname = "";
            this.contactView1.Notes = "";
            this.contactView1.Size = new System.Drawing.Size(494, 407);
            this.contactView1.TabIndex = 5;
            this.contactView1.ContactChanged += new System.EventHandler(this.contactView1_ContactChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Agenda,
            this.toolStripMenuItem_Options,
            this.toolStripMenuItem_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(743, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.Command = null;
            this.toolStripMenuItem_File.CommandParameter = null;
            this.toolStripMenuItem_File.CommandParameterProvider = null;
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File_New,
            this.toolStripMenuItem_File_Open,
            this.toolStripMenuItem_File_Save,
            this.toolStripMenuItem_File_SaveAs,
            this.toolStripMenuItem1,
            this.toolStripMenuItem_File_Export,
            this.toolStripMenuItem_File_Import,
            this.toolStripMenuItem2,
            this.toolStripMenuItem_File_RecentFiles,
            this.toolStripSeparator2,
            this.toolStripMenuItem_File_Exit});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.ShortDescription = null;
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem_File.StatusService = null;
            this.toolStripMenuItem_File.Text = "&File";
            // 
            // toolStripMenuItem_File_New
            // 
            this.toolStripMenuItem_File_New.Command = null;
            this.toolStripMenuItem_File_New.CommandParameter = null;
            this.toolStripMenuItem_File_New.CommandParameterProvider = null;
            this.toolStripMenuItem_File_New.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_New.Image")));
            this.toolStripMenuItem_File_New.Name = "toolStripMenuItem_File_New";
            this.toolStripMenuItem_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItem_File_New.ShortDescription = null;
            this.toolStripMenuItem_File_New.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_New.StatusService = null;
            this.toolStripMenuItem_File_New.Text = "&New";
            // 
            // toolStripMenuItem_File_Open
            // 
            this.toolStripMenuItem_File_Open.Command = null;
            this.toolStripMenuItem_File_Open.CommandParameter = null;
            this.toolStripMenuItem_File_Open.CommandParameterProvider = null;
            this.toolStripMenuItem_File_Open.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_Open.Image")));
            this.toolStripMenuItem_File_Open.Name = "toolStripMenuItem_File_Open";
            this.toolStripMenuItem_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem_File_Open.ShortDescription = null;
            this.toolStripMenuItem_File_Open.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Open.StatusService = null;
            this.toolStripMenuItem_File_Open.Text = "&Open ...";
            this.toolStripMenuItem_File_Open.Click += new System.EventHandler(this.toolStripMenuItem_File_Open_Click);
            // 
            // toolStripMenuItem_File_Save
            // 
            this.toolStripMenuItem_File_Save.Command = null;
            this.toolStripMenuItem_File_Save.CommandParameter = null;
            this.toolStripMenuItem_File_Save.CommandParameterProvider = null;
            this.toolStripMenuItem_File_Save.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_Save.Image")));
            this.toolStripMenuItem_File_Save.Name = "toolStripMenuItem_File_Save";
            this.toolStripMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem_File_Save.ShortDescription = null;
            this.toolStripMenuItem_File_Save.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Save.StatusService = null;
            this.toolStripMenuItem_File_Save.Text = "&Save";
            // 
            // toolStripMenuItem_File_SaveAs
            // 
            this.toolStripMenuItem_File_SaveAs.Command = null;
            this.toolStripMenuItem_File_SaveAs.CommandParameter = null;
            this.toolStripMenuItem_File_SaveAs.CommandParameterProvider = null;
            this.toolStripMenuItem_File_SaveAs.Name = "toolStripMenuItem_File_SaveAs";
            this.toolStripMenuItem_File_SaveAs.ShortDescription = null;
            this.toolStripMenuItem_File_SaveAs.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_SaveAs.StatusService = null;
            this.toolStripMenuItem_File_SaveAs.Text = "Save &As ...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_Export
            // 
            this.toolStripMenuItem_File_Export.Command = null;
            this.toolStripMenuItem_File_Export.CommandParameter = null;
            this.toolStripMenuItem_File_Export.CommandParameterProvider = null;
            this.toolStripMenuItem_File_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ExportToYahooCSV});
            this.toolStripMenuItem_File_Export.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_Export.Image")));
            this.toolStripMenuItem_File_Export.Name = "toolStripMenuItem_File_Export";
            this.toolStripMenuItem_File_Export.ShortDescription = null;
            this.toolStripMenuItem_File_Export.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Export.StatusService = null;
            this.toolStripMenuItem_File_Export.Text = "&Export";
            // 
            // toolStripMenuItem_ExportToYahooCSV
            // 
            this.toolStripMenuItem_ExportToYahooCSV.Command = null;
            this.toolStripMenuItem_ExportToYahooCSV.CommandParameter = null;
            this.toolStripMenuItem_ExportToYahooCSV.CommandParameterProvider = null;
            this.toolStripMenuItem_ExportToYahooCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_ExportToYahooCSV.Image")));
            this.toolStripMenuItem_ExportToYahooCSV.Name = "toolStripMenuItem_ExportToYahooCSV";
            this.toolStripMenuItem_ExportToYahooCSV.ShortDescription = null;
            this.toolStripMenuItem_ExportToYahooCSV.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem_ExportToYahooCSV.StatusService = null;
            this.toolStripMenuItem_ExportToYahooCSV.Text = "To Yahoo CSV";
            // 
            // toolStripMenuItem_File_Import
            // 
            this.toolStripMenuItem_File_Import.Command = null;
            this.toolStripMenuItem_File_Import.CommandParameter = null;
            this.toolStripMenuItem_File_Import.CommandParameterProvider = null;
            this.toolStripMenuItem_File_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ImportFromYahooCSV});
            this.toolStripMenuItem_File_Import.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_Import.Image")));
            this.toolStripMenuItem_File_Import.Name = "toolStripMenuItem_File_Import";
            this.toolStripMenuItem_File_Import.ShortDescription = null;
            this.toolStripMenuItem_File_Import.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Import.StatusService = null;
            this.toolStripMenuItem_File_Import.Text = "&Import";
            // 
            // toolStripMenuItem_ImportFromYahooCSV
            // 
            this.toolStripMenuItem_ImportFromYahooCSV.Command = null;
            this.toolStripMenuItem_ImportFromYahooCSV.CommandParameter = null;
            this.toolStripMenuItem_ImportFromYahooCSV.CommandParameterProvider = null;
            this.toolStripMenuItem_ImportFromYahooCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_ImportFromYahooCSV.Image")));
            this.toolStripMenuItem_ImportFromYahooCSV.Name = "toolStripMenuItem_ImportFromYahooCSV";
            this.toolStripMenuItem_ImportFromYahooCSV.ShortDescription = null;
            this.toolStripMenuItem_ImportFromYahooCSV.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItem_ImportFromYahooCSV.StatusService = null;
            this.toolStripMenuItem_ImportFromYahooCSV.Text = "From Yahoo CSV";
            this.toolStripMenuItem_ImportFromYahooCSV.Click += new System.EventHandler(this.toolStripMenuItem_ImportFromYahooCSV_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_RecentFiles
            // 
            this.toolStripMenuItem_File_RecentFiles.Name = "toolStripMenuItem_File_RecentFiles";
            this.toolStripMenuItem_File_RecentFiles.RecentFilesService = null;
            this.toolStripMenuItem_File_RecentFiles.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_RecentFiles.Text = "Recent &Files";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_Exit
            // 
            this.toolStripMenuItem_File_Exit.Command = null;
            this.toolStripMenuItem_File_Exit.CommandParameter = null;
            this.toolStripMenuItem_File_Exit.CommandParameterProvider = null;
            this.toolStripMenuItem_File_Exit.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_Exit.Image")));
            this.toolStripMenuItem_File_Exit.Name = "toolStripMenuItem_File_Exit";
            this.toolStripMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.toolStripMenuItem_File_Exit.ShortDescription = null;
            this.toolStripMenuItem_File_Exit.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Exit.StatusService = null;
            this.toolStripMenuItem_File_Exit.Text = "E&xit";
            this.toolStripMenuItem_File_Exit.Click += new System.EventHandler(this.toolStripMenuItem_File_Exit_Click);
            // 
            // toolStripMenuItem_Agenda
            // 
            this.toolStripMenuItem_Agenda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Agenda_AddContact,
            this.toolStripMenuItem_Agenda_DeleteContact,
            this.toolStripMenuItem4,
            this.toolStripMenuItem_Agenda_Properties});
            this.toolStripMenuItem_Agenda.Name = "toolStripMenuItem_Agenda";
            this.toolStripMenuItem_Agenda.Size = new System.Drawing.Size(91, 20);
            this.toolStripMenuItem_Agenda.Text = "&Address Book";
            this.toolStripMenuItem_Agenda.DropDownOpening += new System.EventHandler(this.toolStripMenuItem_Agenda_DropDownOpening);
            // 
            // toolStripMenuItem_Agenda_AddContact
            // 
            this.toolStripMenuItem_Agenda_AddContact.Command = null;
            this.toolStripMenuItem_Agenda_AddContact.CommandParameter = null;
            this.toolStripMenuItem_Agenda_AddContact.CommandParameterProvider = null;
            this.toolStripMenuItem_Agenda_AddContact.Name = "toolStripMenuItem_Agenda_AddContact";
            this.toolStripMenuItem_Agenda_AddContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)));
            this.toolStripMenuItem_Agenda_AddContact.ShortDescription = null;
            this.toolStripMenuItem_Agenda_AddContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_Agenda_AddContact.StatusService = null;
            this.toolStripMenuItem_Agenda_AddContact.Text = "&Add Contact";
            this.toolStripMenuItem_Agenda_AddContact.Click += new System.EventHandler(this.toolStripMenuItem_Agenda_AddContact_Click);
            // 
            // toolStripMenuItem_Agenda_DeleteContact
            // 
            this.toolStripMenuItem_Agenda_DeleteContact.Command = null;
            this.toolStripMenuItem_Agenda_DeleteContact.CommandParameter = null;
            this.toolStripMenuItem_Agenda_DeleteContact.CommandParameterProvider = null;
            this.toolStripMenuItem_Agenda_DeleteContact.Name = "toolStripMenuItem_Agenda_DeleteContact";
            this.toolStripMenuItem_Agenda_DeleteContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.toolStripMenuItem_Agenda_DeleteContact.ShortDescription = null;
            this.toolStripMenuItem_Agenda_DeleteContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_Agenda_DeleteContact.StatusService = null;
            this.toolStripMenuItem_Agenda_DeleteContact.Text = "Delete Contact";
            this.toolStripMenuItem_Agenda_DeleteContact.Click += new System.EventHandler(this.toolStripMenuItem_Agenda_DeleteContact_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem_Agenda_Properties
            // 
            this.toolStripMenuItem_Agenda_Properties.Command = null;
            this.toolStripMenuItem_Agenda_Properties.CommandParameter = null;
            this.toolStripMenuItem_Agenda_Properties.CommandParameterProvider = null;
            this.toolStripMenuItem_Agenda_Properties.Name = "toolStripMenuItem_Agenda_Properties";
            this.toolStripMenuItem_Agenda_Properties.ShortDescription = null;
            this.toolStripMenuItem_Agenda_Properties.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_Agenda_Properties.StatusService = null;
            this.toolStripMenuItem_Agenda_Properties.Text = "Properties";
            // 
            // toolStripMenuItem_Options
            // 
            this.toolStripMenuItem_Options.Command = null;
            this.toolStripMenuItem_Options.CommandParameter = null;
            this.toolStripMenuItem_Options.CommandParameterProvider = null;
            this.toolStripMenuItem_Options.Name = "toolStripMenuItem_Options";
            this.toolStripMenuItem_Options.ShortDescription = null;
            this.toolStripMenuItem_Options.Size = new System.Drawing.Size(61, 20);
            this.toolStripMenuItem_Options.StatusService = null;
            this.toolStripMenuItem_Options.Text = "&Options";
            this.toolStripMenuItem_Options.Visible = false;
            // 
            // toolStripMenuItem_Help
            // 
            this.toolStripMenuItem_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem_Help.Command = null;
            this.toolStripMenuItem_Help.CommandParameter = null;
            this.toolStripMenuItem_Help.CommandParameterProvider = null;
            this.toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Help_About});
            this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            this.toolStripMenuItem_Help.ShortDescription = null;
            this.toolStripMenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_Help.StatusService = null;
            this.toolStripMenuItem_Help.Text = "&Help";
            // 
            // toolStripMenuItem_Help_About
            // 
            this.toolStripMenuItem_Help_About.Command = null;
            this.toolStripMenuItem_Help_About.CommandParameter = null;
            this.toolStripMenuItem_Help_About.CommandParameterProvider = null;
            this.toolStripMenuItem_Help_About.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_Help_About.Image")));
            this.toolStripMenuItem_Help_About.Name = "toolStripMenuItem_Help_About";
            this.toolStripMenuItem_Help_About.ShortDescription = null;
            this.toolStripMenuItem_Help_About.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem_Help_About.StatusService = null;
            this.toolStripMenuItem_Help_About.Text = "&About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 455);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(743, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "lsb";
            this.saveFileDialog1.Filter = "Agenda Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Agenda Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
            // 
            // FormLisimba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 477);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormLisimba";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lisimba";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormLisimba_FormClosing);
            this.Shown += new System.EventHandler(this.FormLisimba_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_New;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_Open;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_Save;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_Export;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_ExportToYahooCSV;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_Import;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_ImportFromYahooCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_File_Exit;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Options;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Help;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Help_About;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private ContactView contactView1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Agenda;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Agenda_AddContact;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Agenda_DeleteContact;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private DustInTheWind.Lisimba.UserControls.CustomMenuItem toolStripMenuItem_Agenda_Properties;
        private ContactListView contactListView1;
        private DustInTheWind.Lisimba.UserControls.MenuItemWithChildren toolStripMenuItem_File_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}