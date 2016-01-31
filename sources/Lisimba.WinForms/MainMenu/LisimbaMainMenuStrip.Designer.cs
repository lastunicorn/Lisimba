namespace DustInTheWind.Lisimba.MainMenu
{
    partial class LisimbaMainMenuStrip
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LisimbaMainMenuStrip));
            this.toolStripMenuItem_File = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem();
            this.toolStripMenuItem_File_New = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Open = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Save = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_File_SaveAs = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Close = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripSeparator_File_1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Export = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_ExportToYahooCSV = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Import = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_ImportFromYahooCSV = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripSeparator_File_2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripRecentFilesMenuItem_File_RecentFiles = new DustInTheWind.Lisimba.MainMenu.RecentFilesMenuItem();
            this.toolStripSeparator_File_3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Exit = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_AddContact = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_DeleteContact = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_AddressBook_Properties = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_Help = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.toolStripMenuItem_Help_About = new DustInTheWind.Lisimba.MainMenu.CustomMenuItem(this.components);
            this.SuspendLayout();
            // 
            // toolStripMenuItem_File
            // 
            this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File_New,
            this.toolStripMenuItem_File_Open,
            this.toolStripMenuItem_File_Save,
            this.toolStripMenuItem_File_SaveAs,
            this.toolStripMenuItem_File_Close,
            this.toolStripSeparator_File_1,
            this.toolStripMenuItem_File_Export,
            this.toolStripMenuItem_File_Import,
            this.toolStripSeparator_File_2,
            this.toolStripRecentFilesMenuItem_File_RecentFiles,
            this.toolStripSeparator_File_3,
            this.toolStripMenuItem_File_Exit});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem_File.Text = "&File";
            // 
            // toolStripMenuItem_File_New
            // 
            this.toolStripMenuItem_File_New.Image = global::DustInTheWind.Lisimba.Properties.Resources.new_16;
            this.toolStripMenuItem_File_New.Name = "toolStripMenuItem_File_New";
            this.toolStripMenuItem_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItem_File_New.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_New.Text = "&New";
            // 
            // toolStripMenuItem_File_Open
            // 
            this.toolStripMenuItem_File_Open.Image = global::DustInTheWind.Lisimba.Properties.Resources.open_16;
            this.toolStripMenuItem_File_Open.Name = "toolStripMenuItem_File_Open";
            this.toolStripMenuItem_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem_File_Open.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Open.Text = "&Open ...";
            // 
            // toolStripMenuItem_File_Save
            // 
            this.toolStripMenuItem_File_Save.Image = global::DustInTheWind.Lisimba.Properties.Resources.save_16;
            this.toolStripMenuItem_File_Save.Name = "toolStripMenuItem_File_Save";
            this.toolStripMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem_File_Save.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Save.Text = "&Save";
            // 
            // toolStripMenuItem_File_SaveAs
            // 
            this.toolStripMenuItem_File_SaveAs.Name = "toolStripMenuItem_File_SaveAs";
            this.toolStripMenuItem_File_SaveAs.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_SaveAs.Text = "Save &As ...";
            // 
            // toolStripMenuItem_File_Close
            // 
            this.toolStripMenuItem_File_Close.Name = "toolStripMenuItem_File_Close";
            this.toolStripMenuItem_File_Close.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Close.Text = "Close";
            // 
            // toolStripSeparator_File_1
            // 
            this.toolStripSeparator_File_1.Name = "toolStripSeparator_File_1";
            this.toolStripSeparator_File_1.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItem_File_Export
            // 
            this.toolStripMenuItem_File_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ExportToYahooCSV});
            this.toolStripMenuItem_File_Export.Image = global::DustInTheWind.Lisimba.Properties.Resources.export;
            this.toolStripMenuItem_File_Export.Name = "toolStripMenuItem_File_Export";
            this.toolStripMenuItem_File_Export.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Export.Text = "&Export";
            this.toolStripMenuItem_File_Export.Visible = false;
            // 
            // toolStripMenuItem_ExportToYahooCSV
            // 
            this.toolStripMenuItem_ExportToYahooCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_ExportToYahooCSV.Image")));
            this.toolStripMenuItem_ExportToYahooCSV.Name = "toolStripMenuItem_ExportToYahooCSV";
            this.toolStripMenuItem_ExportToYahooCSV.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItem_ExportToYahooCSV.Text = "To Yahoo CSV";
            // 
            // toolStripMenuItem_File_Import
            // 
            this.toolStripMenuItem_File_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ImportFromYahooCSV});
            this.toolStripMenuItem_File_Import.Image = global::DustInTheWind.Lisimba.Properties.Resources.import;
            this.toolStripMenuItem_File_Import.Name = "toolStripMenuItem_File_Import";
            this.toolStripMenuItem_File_Import.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Import.Text = "&Import";
            // 
            // toolStripMenuItem_ImportFromYahooCSV
            // 
            this.toolStripMenuItem_ImportFromYahooCSV.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_ImportFromYahooCSV.Image")));
            this.toolStripMenuItem_ImportFromYahooCSV.Name = "toolStripMenuItem_ImportFromYahooCSV";
            this.toolStripMenuItem_ImportFromYahooCSV.Size = new System.Drawing.Size(163, 22);
            this.toolStripMenuItem_ImportFromYahooCSV.Text = "From Yahoo CSV";
            // 
            // toolStripSeparator_File_2
            // 
            this.toolStripSeparator_File_2.Name = "toolStripSeparator_File_2";
            this.toolStripSeparator_File_2.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripRecentFilesMenuItem_File_RecentFiles
            // 
            this.toolStripRecentFilesMenuItem_File_RecentFiles.Name = "toolStripRecentFilesMenuItem_File_RecentFiles";
            this.toolStripRecentFilesMenuItem_File_RecentFiles.Size = new System.Drawing.Size(180, 22);
            this.toolStripRecentFilesMenuItem_File_RecentFiles.Text = "Recent &Files";
            // 
            // toolStripSeparator_File_3
            // 
            this.toolStripSeparator_File_3.Name = "toolStripSeparator_File_3";
            this.toolStripSeparator_File_3.Size = new System.Drawing.Size(177, 6);
            // 
            // toolStripMenuItem_File_Exit
            // 
            this.toolStripMenuItem_File_Exit.Image = global::DustInTheWind.Lisimba.Properties.Resources.exit_16;
            this.toolStripMenuItem_File_Exit.Name = "toolStripMenuItem_File_Exit";
            this.toolStripMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.toolStripMenuItem_File_Exit.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_File_Exit.Text = "E&xit";
            // 
            // toolStripMenuItem_AddressBook
            // 
            this.toolStripMenuItem_AddressBook.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AddressBook_AddContact,
            this.toolStripMenuItem_AddressBook_DeleteContact,
            this.toolStripMenuItem4,
            this.toolStripMenuItem_AddressBook_Properties});
            this.toolStripMenuItem_AddressBook.Name = "toolStripMenuItem_AddressBook";
            this.toolStripMenuItem_AddressBook.Size = new System.Drawing.Size(91, 20);
            this.toolStripMenuItem_AddressBook.Text = "&Address Book";
            // 
            // toolStripMenuItem_AddressBook_AddContact
            // 
            this.toolStripMenuItem_AddressBook_AddContact.Name = "toolStripMenuItem_AddressBook_AddContact";
            this.toolStripMenuItem_AddressBook_AddContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)));
            this.toolStripMenuItem_AddressBook_AddContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_AddContact.Text = "&Add Contact";
            // 
            // toolStripMenuItem_AddressBook_DeleteContact
            // 
            this.toolStripMenuItem_AddressBook_DeleteContact.Name = "toolStripMenuItem_AddressBook_DeleteContact";
            this.toolStripMenuItem_AddressBook_DeleteContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.toolStripMenuItem_AddressBook_DeleteContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_DeleteContact.Text = "Delete Contact";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem_AddressBook_Properties
            // 
            this.toolStripMenuItem_AddressBook_Properties.Name = "toolStripMenuItem_AddressBook_Properties";
            this.toolStripMenuItem_AddressBook_Properties.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_Properties.Text = "Properties";
            // 
            // toolStripMenuItem_Help
            // 
            this.toolStripMenuItem_Help.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Help_About});
            this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
            this.toolStripMenuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem_Help.Text = "&Help";
            // 
            // toolStripMenuItem_Help_About
            // 
            this.toolStripMenuItem_Help_About.Image = global::DustInTheWind.Lisimba.Properties.Resources.about_16;
            this.toolStripMenuItem_Help_About.Name = "toolStripMenuItem_Help_About";
            this.toolStripMenuItem_Help_About.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem_Help_About.Text = "&About";
            // 
            // LisimbaMainMenuStrip
            // 
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_AddressBook,
            this.toolStripMenuItem_Help});
            this.Location = new System.Drawing.Point(0, 24);
            this.Size = new System.Drawing.Size(743, 24);
            this.TabIndex = 7;
            this.Text = "LisimbaMainMenuStrip";
            this.ResumeLayout(false);

        }

        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_New;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Open;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Save;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_SaveAs;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_1;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Export;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_ExportToYahooCSV;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Import;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_ImportFromYahooCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_2;
        private DustInTheWind.Lisimba.MainMenu.RecentFilesMenuItem toolStripRecentFilesMenuItem_File_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_3;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_File_Exit;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_Help;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_Help_About;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_AddressBook;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_AddressBook_AddContact;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_AddressBook_DeleteContact;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private DustInTheWind.Lisimba.MainMenu.CustomMenuItem toolStripMenuItem_AddressBook_Properties;
    }
}
