using DustInTheWind.WinFormsCommon.Controls;

namespace DustInTheWind.Lisimba.WinForms.MainMenu
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
            this.toolStripMenuItem_File = new CustomMenuItem(this.components);
            this.toolStripMenuItem_File_New = new CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Open = new CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Save = new CustomMenuItem(this.components);
            this.toolStripMenuItem_File_SaveAs = new CustomMenuItem(this.components);
            this.toolStripMenuItem_File_Close = new CustomMenuItem(this.components);
            this.toolStripSeparator_File_1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Export = new ListMenuItem(this.components);
            this.toolStripMenuItem_File_Import = new ListMenuItem(this.components);
            this.toolStripSeparator_File_2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripListMenuItem_File_RecentFiles = new ListMenuItem(this.components);
            this.toolStripSeparator_File_3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_File_Exit = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_AddContact = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_DeleteContact = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_Separator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_AddressBook_Undo = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_Redo = new CustomMenuItem(this.components);
            this.toolStripMenuItem_AddressBook_Separator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_AddressBook_Properties = new CustomMenuItem(this.components);
            this.toolStripMenuItem_Help = new CustomMenuItem(this.components);
            this.toolStripMenuItem_Help_About = new CustomMenuItem(this.components);
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
            this.toolStripListMenuItem_File_RecentFiles,
            this.toolStripSeparator_File_3,
            this.toolStripMenuItem_File_Exit});
            this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
            this.toolStripMenuItem_File.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem_File.Text = "&File";
            // 
            // toolStripMenuItem_File_New
            // 
            this.toolStripMenuItem_File_New.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.new_16;
            this.toolStripMenuItem_File_New.Name = "toolStripMenuItem_File_New";
            this.toolStripMenuItem_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.toolStripMenuItem_File_New.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_New.Text = "&New";
            // 
            // toolStripMenuItem_File_Open
            // 
            this.toolStripMenuItem_File_Open.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.open_16;
            this.toolStripMenuItem_File_Open.Name = "toolStripMenuItem_File_Open";
            this.toolStripMenuItem_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem_File_Open.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Open.Text = "&Open ...";
            // 
            // toolStripMenuItem_File_Save
            // 
            this.toolStripMenuItem_File_Save.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.save_16;
            this.toolStripMenuItem_File_Save.Name = "toolStripMenuItem_File_Save";
            this.toolStripMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.toolStripMenuItem_File_Save.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Save.Text = "&Save";
            // 
            // toolStripMenuItem_File_SaveAs
            // 
            this.toolStripMenuItem_File_SaveAs.Name = "toolStripMenuItem_File_SaveAs";
            this.toolStripMenuItem_File_SaveAs.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_SaveAs.Text = "Save &As ...";
            // 
            // toolStripMenuItem_File_Close
            // 
            this.toolStripMenuItem_File_Close.Name = "toolStripMenuItem_File_Close";
            this.toolStripMenuItem_File_Close.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Close.Text = "Close";
            // 
            // toolStripSeparator_File_1
            // 
            this.toolStripSeparator_File_1.Name = "toolStripSeparator_File_1";
            this.toolStripSeparator_File_1.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_Export
            // 
            this.toolStripMenuItem_File_Export.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.export;
            this.toolStripMenuItem_File_Export.Name = "toolStripMenuItem_File_Export";
            this.toolStripMenuItem_File_Export.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Export.Text = "&Export";
            this.toolStripMenuItem_File_Export.ViewModel = null;
            // 
            // toolStripMenuItem_File_Import
            // 
            this.toolStripMenuItem_File_Import.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.import;
            this.toolStripMenuItem_File_Import.Name = "toolStripMenuItem_File_Import";
            this.toolStripMenuItem_File_Import.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Import.Text = "&Import";
            this.toolStripMenuItem_File_Import.ViewModel = null;
            // 
            // toolStripSeparator_File_2
            // 
            this.toolStripSeparator_File_2.Name = "toolStripSeparator_File_2";
            this.toolStripSeparator_File_2.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripListMenuItem_File_RecentFiles
            // 
            this.toolStripListMenuItem_File_RecentFiles.Name = "toolStripListMenuItem_File_RecentFiles";
            this.toolStripListMenuItem_File_RecentFiles.Size = new System.Drawing.Size(158, 22);
            this.toolStripListMenuItem_File_RecentFiles.Text = "Recent &Files";
            this.toolStripListMenuItem_File_RecentFiles.ViewModel = null;
            // 
            // toolStripSeparator_File_3
            // 
            this.toolStripSeparator_File_3.Name = "toolStripSeparator_File_3";
            this.toolStripSeparator_File_3.Size = new System.Drawing.Size(155, 6);
            // 
            // toolStripMenuItem_File_Exit
            // 
            this.toolStripMenuItem_File_Exit.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.exit_16;
            this.toolStripMenuItem_File_Exit.Name = "toolStripMenuItem_File_Exit";
            this.toolStripMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.toolStripMenuItem_File_Exit.Size = new System.Drawing.Size(158, 22);
            this.toolStripMenuItem_File_Exit.Text = "E&xit";
            // 
            // toolStripMenuItem_AddressBook
            // 
            this.toolStripMenuItem_AddressBook.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_AddressBook_AddContact,
            this.toolStripMenuItem_AddressBook_DeleteContact,
            this.toolStripMenuItem_AddressBook_Separator1,
            this.toolStripMenuItem_AddressBook_Undo,
            this.toolStripMenuItem_AddressBook_Redo,
            this.toolStripMenuItem_AddressBook_Separator2,
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
            // toolStripMenuItem_AddressBook_Separator1
            // 
            this.toolStripMenuItem_AddressBook_Separator1.Name = "toolStripMenuItem_AddressBook_Separator1";
            this.toolStripMenuItem_AddressBook_Separator1.Size = new System.Drawing.Size(205, 6);
            // 
            // toolStripMenuItem_AddressBook_Undo
            // 
            this.toolStripMenuItem_AddressBook_Undo.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.undo;
            this.toolStripMenuItem_AddressBook_Undo.Name = "toolStripMenuItem_AddressBook_Undo";
            this.toolStripMenuItem_AddressBook_Undo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.toolStripMenuItem_AddressBook_Undo.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_Undo.Text = "&Undo";
            // 
            // toolStripMenuItem_AddressBook_Redo
            // 
            this.toolStripMenuItem_AddressBook_Redo.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.redo;
            this.toolStripMenuItem_AddressBook_Redo.Name = "toolStripMenuItem_AddressBook_Redo";
            this.toolStripMenuItem_AddressBook_Redo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.toolStripMenuItem_AddressBook_Redo.Size = new System.Drawing.Size(208, 22);
            this.toolStripMenuItem_AddressBook_Redo.Text = "&Redo";
            // 
            // toolStripMenuItem_AddressBook_Separator2
            // 
            this.toolStripMenuItem_AddressBook_Separator2.Name = "toolStripMenuItem_AddressBook_Separator2";
            this.toolStripMenuItem_AddressBook_Separator2.Size = new System.Drawing.Size(205, 6);
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
            this.toolStripMenuItem_Help_About.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.about_16;
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

        private CustomMenuItem toolStripMenuItem_File;
        private CustomMenuItem toolStripMenuItem_File_New;
        private CustomMenuItem toolStripMenuItem_File_Open;
        private CustomMenuItem toolStripMenuItem_File_Save;
        private CustomMenuItem toolStripMenuItem_File_SaveAs;
        private CustomMenuItem toolStripMenuItem_File_Close;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_1;
        private ListMenuItem toolStripMenuItem_File_Export;
        private ListMenuItem toolStripMenuItem_File_Import;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_2;
        private ListMenuItem toolStripListMenuItem_File_RecentFiles;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator_File_3;
        private CustomMenuItem toolStripMenuItem_File_Exit;
        private CustomMenuItem toolStripMenuItem_Help;
        private CustomMenuItem toolStripMenuItem_Help_About;
        private CustomMenuItem toolStripMenuItem_AddressBook;
        private CustomMenuItem toolStripMenuItem_AddressBook_AddContact;
        private CustomMenuItem toolStripMenuItem_AddressBook_DeleteContact;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_AddressBook_Separator1;
        private CustomMenuItem toolStripMenuItem_AddressBook_Properties;
        private CustomMenuItem toolStripMenuItem_AddressBook_Undo;
        private CustomMenuItem toolStripMenuItem_AddressBook_Redo;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem_AddressBook_Separator2;
    }
}
