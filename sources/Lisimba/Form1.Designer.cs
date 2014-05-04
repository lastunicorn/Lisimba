using Egg;
namespace Lindani
{
	partial class Form1
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
			Person person1 = new Person();
			Date date1 = new Date();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem_File = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_File_New = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_File_Open = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_File_Save = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_File_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem_File_Export = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_ExportToYahooCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_File_Import = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_ImportFromYahooCSV = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem_File_Exit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Options = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem_Help_About = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.infoPerson1 = new InfoPerson();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.IntegralHeight = false;
			this.listBox1.Location = new System.Drawing.Point(0, 0);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(189, 360);
			this.listBox1.TabIndex = 0;
			this.listBox1.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
			this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File,
            this.toolStripMenuItem_Options,
            this.toolStripMenuItem_Help});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(713, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem_File
			// 
			this.toolStripMenuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File_New,
            this.toolStripMenuItem_File_Open,
            this.toolStripMenuItem_File_Save,
            this.toolStripMenuItem_File_SaveAs,
            this.toolStripMenuItem1,
            this.toolStripMenuItem_File_Export,
            this.toolStripMenuItem_File_Import,
            this.toolStripMenuItem2,
            this.toolStripMenuItem_File_Exit});
			this.toolStripMenuItem_File.Name = "toolStripMenuItem_File";
			this.toolStripMenuItem_File.Size = new System.Drawing.Size(35, 20);
			this.toolStripMenuItem_File.Text = "&File";
			// 
			// toolStripMenuItem_File_New
			// 
			this.toolStripMenuItem_File_New.Name = "toolStripMenuItem_File_New";
			this.toolStripMenuItem_File_New.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.toolStripMenuItem_File_New.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_New.Text = "&New";
			this.toolStripMenuItem_File_New.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_New_MouseLeave);
			this.toolStripMenuItem_File_New.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_New_MouseEnter);
			this.toolStripMenuItem_File_New.Click += new System.EventHandler(this.toolStripMenuItem_File_New_Click);
			// 
			// toolStripMenuItem_File_Open
			// 
			this.toolStripMenuItem_File_Open.Name = "toolStripMenuItem_File_Open";
			this.toolStripMenuItem_File_Open.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.toolStripMenuItem_File_Open.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_Open.Text = "&Open ...";
			this.toolStripMenuItem_File_Open.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_Open_MouseLeave);
			this.toolStripMenuItem_File_Open.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_Open_MouseEnter);
			this.toolStripMenuItem_File_Open.Click += new System.EventHandler(this.toolStripMenuItem_File_Open_Click);
			// 
			// toolStripMenuItem_File_Save
			// 
			this.toolStripMenuItem_File_Save.Name = "toolStripMenuItem_File_Save";
			this.toolStripMenuItem_File_Save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.toolStripMenuItem_File_Save.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_Save.Text = "&Save";
			this.toolStripMenuItem_File_Save.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_Save_MouseLeave);
			this.toolStripMenuItem_File_Save.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_Save_MouseEnter);
			this.toolStripMenuItem_File_Save.Click += new System.EventHandler(this.toolStripMenuItem_File_Save_Click);
			// 
			// toolStripMenuItem_File_SaveAs
			// 
			this.toolStripMenuItem_File_SaveAs.Name = "toolStripMenuItem_File_SaveAs";
			this.toolStripMenuItem_File_SaveAs.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_SaveAs.Text = "Save &As ...";
			this.toolStripMenuItem_File_SaveAs.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_SaveAs_MouseLeave);
			this.toolStripMenuItem_File_SaveAs.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_SaveAs_MouseEnter);
			this.toolStripMenuItem_File_SaveAs.Click += new System.EventHandler(this.toolStripMenuItem_File_SaveAs_Click);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 6);
			// 
			// toolStripMenuItem_File_Export
			// 
			this.toolStripMenuItem_File_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ExportToYahooCSV});
			this.toolStripMenuItem_File_Export.Name = "toolStripMenuItem_File_Export";
			this.toolStripMenuItem_File_Export.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_Export.Text = "&Export";
			this.toolStripMenuItem_File_Export.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_Export_MouseLeave);
			this.toolStripMenuItem_File_Export.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_Export_MouseEnter);
			// 
			// toolStripMenuItem_ExportToYahooCSV
			// 
			this.toolStripMenuItem_ExportToYahooCSV.Name = "toolStripMenuItem_ExportToYahooCSV";
			this.toolStripMenuItem_ExportToYahooCSV.Size = new System.Drawing.Size(141, 22);
			this.toolStripMenuItem_ExportToYahooCSV.Text = "To Yahoo CSV";
			// 
			// toolStripMenuItem_File_Import
			// 
			this.toolStripMenuItem_File_Import.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_ImportFromYahooCSV});
			this.toolStripMenuItem_File_Import.Name = "toolStripMenuItem_File_Import";
			this.toolStripMenuItem_File_Import.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_Import.Text = "&Import";
			this.toolStripMenuItem_File_Import.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_Import_MouseLeave);
			this.toolStripMenuItem_File_Import.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_Import_MouseEnter);
			// 
			// toolStripMenuItem_ImportFromYahooCSV
			// 
			this.toolStripMenuItem_ImportFromYahooCSV.Name = "toolStripMenuItem_ImportFromYahooCSV";
			this.toolStripMenuItem_ImportFromYahooCSV.Size = new System.Drawing.Size(153, 22);
			this.toolStripMenuItem_ImportFromYahooCSV.Text = "From Yahoo CSV";
			this.toolStripMenuItem_ImportFromYahooCSV.Click += new System.EventHandler(this.toolStripMenuItem_ImportFromYahooCSV_Click);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 6);
			// 
			// toolStripMenuItem_File_Exit
			// 
			this.toolStripMenuItem_File_Exit.Name = "toolStripMenuItem_File_Exit";
			this.toolStripMenuItem_File_Exit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.toolStripMenuItem_File_Exit.Size = new System.Drawing.Size(155, 22);
			this.toolStripMenuItem_File_Exit.Text = "E&xit";
			this.toolStripMenuItem_File_Exit.MouseLeave += new System.EventHandler(this.toolStripMenuItem_File_Exit_MouseLeave);
			this.toolStripMenuItem_File_Exit.MouseEnter += new System.EventHandler(this.toolStripMenuItem_File_Exit_MouseEnter);
			// 
			// toolStripMenuItem_Options
			// 
			this.toolStripMenuItem_Options.Name = "toolStripMenuItem_Options";
			this.toolStripMenuItem_Options.Size = new System.Drawing.Size(56, 20);
			this.toolStripMenuItem_Options.Text = "&Options";
			this.toolStripMenuItem_Options.Visible = false;
			// 
			// toolStripMenuItem_Help
			// 
			this.toolStripMenuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_Help_About});
			this.toolStripMenuItem_Help.Name = "toolStripMenuItem_Help";
			this.toolStripMenuItem_Help.Size = new System.Drawing.Size(40, 20);
			this.toolStripMenuItem_Help.Text = "&Help";
			// 
			// toolStripMenuItem_Help_About
			// 
			this.toolStripMenuItem_Help_About.Name = "toolStripMenuItem_Help_About";
			this.toolStripMenuItem_Help_About.Size = new System.Drawing.Size(152, 22);
			this.toolStripMenuItem_Help_About.Text = "&About";
			this.toolStripMenuItem_Help_About.MouseLeave += new System.EventHandler(this.toolStripMenuItem_Help_About_MouseLeave);
			this.toolStripMenuItem_Help_About.MouseEnter += new System.EventHandler(this.toolStripMenuItem_Help_About_MouseEnter);
			this.toolStripMenuItem_Help_About.Click += new System.EventHandler(this.toolStripMenuItem_Help_About_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(8, 32);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.listBox1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.infoPerson1);
			this.splitContainer1.Size = new System.Drawing.Size(696, 360);
			this.splitContainer1.SplitterDistance = 189;
			this.splitContainer1.SplitterWidth = 8;
			this.splitContainer1.TabIndex = 5;
			// 
			// infoPerson1
			// 
			this.infoPerson1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.infoPerson1.EditMode = false;
			this.infoPerson1.Location = new System.Drawing.Point(0, 0);
			this.infoPerson1.Name = "infoPerson1";
			this.infoPerson1.Size = new System.Drawing.Size(499, 360);
			this.infoPerson1.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 401);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(713, 22);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(103, 17);
			this.toolStripStatusLabel1.Text = "toolStripStatusLabel";
			// 
			// saveFileDialog1
			// 
			this.saveFileDialog1.DefaultExt = "lin";
			this.saveFileDialog1.Filter = "Agenda Files (*.lin)|*.lin|All Files (*.*)|*.*";
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			this.openFileDialog1.Filter = "Agenda Files (*.lin)|*.lin|All Files (*.*)|*.*";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(713, 423);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(721, 457);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private InfoPerson infoPerson1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_Open;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_Save;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_Export;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_Import;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_Exit;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_New;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_SaveAs;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ExportToYahooCSV;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_ImportFromYahooCSV;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Help_About;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_Options;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
	}
}

