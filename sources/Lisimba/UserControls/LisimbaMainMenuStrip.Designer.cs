using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustInTheWind.Lisimba.UserControls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LisimbaMainMenuStrip));
            this.toolStripMenuItem_File = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_File_New = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_File_Open = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_File_Save = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_File_SaveAs = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Export = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_ExportToYahooCSV = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_File_Import = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_ImportFromYahooCSV = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_RecentFiles = new DustInTheWind.Lisimba.UserControls.MenuItemWithChildren();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Exit = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_AddressBook = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_AddressBook_AddContact = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_AddressBook_DeleteContact = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_AddressBook_Properties = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_Help = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.toolStripMenuItem_Help_About = new DustInTheWind.Lisimba.UserControls.CommandedMenuItem();
            this.SuspendLayout();
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
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_RecentFiles
            // 
            this.toolStripMenuItem_File_RecentFiles.ChildrenCommand = null;
            this.toolStripMenuItem_File_RecentFiles.Name = "toolStripMenuItem_File_RecentFiles";
            this.toolStripMenuItem_File_RecentFiles.RecentFiles = null;
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
            // 
            // toolStripMenuItem_AddressBook
            // 
            this.toolStripMenuItem_AddressBook.Command = null;
            this.toolStripMenuItem_AddressBook.CommandParameter = null;
            this.toolStripMenuItem_AddressBook.CommandParameterProvider = null;
            this.toolStripMenuItem_AddressBook.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AddressBook_AddContact,
            this.toolStripMenuItem_AddressBook_DeleteContact,
            this.toolStripMenuItem4,
            this.toolStripMenuItem_AddressBook_Properties});
            this.toolStripMenuItem_AddressBook.Name = "toolStripMenuItem_AddressBook";
            this.toolStripMenuItem_AddressBook.ShortDescription = null;
            this.toolStripMenuItem_AddressBook.Size = new System.Drawing.Size(91, 20);
            this.toolStripMenuItem_AddressBook.StatusService = null;
            this.toolStripMenuItem_AddressBook.Text = "&Address Book";
            // 
            // toolStripMenuItem_AddressBook_AddContact
            // 
            this.toolStripMenuItem_AddressBook_AddContact.Command = null;
            this.toolStripMenuItem_AddressBook_AddContact.CommandParameter = null;
            this.toolStripMenuItem_AddressBook_AddContact.CommandParameterProvider = null;
            this.toolStripMenuItem_AddressBook_AddContact.Name = "toolStripMenuItem_AddressBook_AddContact";
            this.toolStripMenuItem_AddressBook_AddContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)));
            this.toolStripMenuItem_AddressBook_AddContact.ShortDescription = null;
            this.toolStripMenuItem_AddressBook_AddContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_AddContact.StatusService = null;
            this.toolStripMenuItem_AddressBook_AddContact.Text = "&Add Contact";
            // 
            // toolStripMenuItem_AddressBook_DeleteContact
            // 
            this.toolStripMenuItem_AddressBook_DeleteContact.Command = null;
            this.toolStripMenuItem_AddressBook_DeleteContact.CommandParameter = null;
            this.toolStripMenuItem_AddressBook_DeleteContact.CommandParameterProvider = null;
            this.toolStripMenuItem_AddressBook_DeleteContact.Name = "toolStripMenuItem_AddressBook_DeleteContact";
            this.toolStripMenuItem_AddressBook_DeleteContact.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
            this.toolStripMenuItem_AddressBook_DeleteContact.ShortDescription = null;
            this.toolStripMenuItem_AddressBook_DeleteContact.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_DeleteContact.StatusService = null;
            this.toolStripMenuItem_AddressBook_DeleteContact.Text = "Delete Contact";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem_AddressBook_Properties
            // 
            this.toolStripMenuItem_AddressBook_Properties.Command = null;
            this.toolStripMenuItem_AddressBook_Properties.CommandParameter = null;
            this.toolStripMenuItem_AddressBook_Properties.CommandParameterProvider = null;
            this.toolStripMenuItem_AddressBook_Properties.Name = "toolStripMenuItem_AddressBook_Properties";
            this.toolStripMenuItem_AddressBook_Properties.ShortDescription = null;
            this.toolStripMenuItem_AddressBook_Properties.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_Properties.StatusService = null;
            this.toolStripMenuItem_AddressBook_Properties.Text = "Properties";
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

        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_New;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_Open;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_Save;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_SaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_Export;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_ExportToYahooCSV;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_Import;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_ImportFromYahooCSV;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private DustInTheWind.Lisimba.UserControls.MenuItemWithChildren toolStripMenuItem_File_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_File_Exit;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_Help;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_Help_About;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_AddressBook;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_AddressBook_AddContact;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_AddressBook_DeleteContact;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private DustInTheWind.Lisimba.UserControls.CommandedMenuItem toolStripMenuItem_AddressBook_Properties;
    }
}
