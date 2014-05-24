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
            this.contactView1 = new DustInTheWind.Lisimba.UserControls.ContactEditor();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStripMain = new DustInTheWind.Lisimba.UserControls.LisimbaMainMenuStrip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(727, 415);
            this.splitContainer1.SplitterDistance = 194;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 4;
            // 
            // contactListView1
            // 
            this.contactListView1.AllowSort = true;
            this.contactListView1.ConfigurationService = null;
            this.contactListView1.CurrentData = null;
            this.contactListView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactListView1.Location = new System.Drawing.Point(0, 0);
            this.contactListView1.Name = "contactListView1";
            this.contactListView1.SearchText = "";
            this.contactListView1.Size = new System.Drawing.Size(194, 415);
            this.contactListView1.SortField = DustInTheWind.Lisimba.Egg.Enums.ContactsSortingType.NicknameOrName;
            this.contactListView1.TabIndex = 9;
            // 
            // contactView1
            // 
            this.contactView1.BackColor = System.Drawing.SystemColors.Control;
            this.contactView1.Birthday = "";
            this.contactView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactView1.Enabled = false;
            this.contactView1.FirstName = "";
            this.contactView1.ForeColor = System.Drawing.Color.Black;
            this.contactView1.LastName = "";
            this.contactView1.Location = new System.Drawing.Point(0, 0);
            this.contactView1.MiddleName = "";
            this.contactView1.MinimumSize = new System.Drawing.Size(400, 300);
            this.contactView1.Name = "contactView1";
            this.contactView1.Nickname = "";
            this.contactView1.Notes = "";
            this.contactView1.Size = new System.Drawing.Size(525, 415);
            this.contactView1.TabIndex = 5;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(8);
            this.panel1.Size = new System.Drawing.Size(743, 431);
            this.panel1.TabIndex = 9;
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
            this.ClientSize = new System.Drawing.Size(743, 477);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStripMain);
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
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private ContactEditor contactView1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private ContactListView contactListView1;
        private System.Windows.Forms.Panel panel1;
        private LisimbaMainMenuStrip menuStripMain;
    }
}