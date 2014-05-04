namespace DustInTheWind.Lisimba.UserControls
{
    partial class ContactListView
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
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.contextMenuStripListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_List_Add = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_List_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_List_ViewBiorythm = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxSortBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripListBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView1.FullRowSelect = true;
            this.treeView1.HideSelection = false;
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.ShowPlusMinus = false;
            this.treeView1.Size = new System.Drawing.Size(178, 240);
            this.treeView1.TabIndex = 2;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(0, 273);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(178, 20);
            this.textBoxSearch.TabIndex = 3;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // contextMenuStripListBox
            // 
            this.contextMenuStripListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_List_Add,
            this.toolStripMenuItem_List_Delete,
            this.toolStripMenuItem1,
            this.toolStripMenuItem_List_ViewBiorythm});
            this.contextMenuStripListBox.Name = "contextMenuStripList";
            this.contextMenuStripListBox.Size = new System.Drawing.Size(158, 76);
            // 
            // toolStripMenuItem_List_Add
            // 
            this.toolStripMenuItem_List_Add.Name = "toolStripMenuItem_List_Add";
            this.toolStripMenuItem_List_Add.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_List_Add.Text = "&Add Contact";
            this.toolStripMenuItem_List_Add.Click += new System.EventHandler(this.toolStripMenuItem_List_Add_Click);
            // 
            // toolStripMenuItem_List_Delete
            // 
            this.toolStripMenuItem_List_Delete.Name = "toolStripMenuItem_List_Delete";
            this.toolStripMenuItem_List_Delete.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_List_Delete.Text = "&Delete Contact";
            this.toolStripMenuItem_List_Delete.Click += new System.EventHandler(this.toolStripMenuItem_List_Delete_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(154, 6);
            // 
            // toolStripMenuItem_List_ViewBiorythm
            // 
            this.toolStripMenuItem_List_ViewBiorythm.Name = "toolStripMenuItem_List_ViewBiorythm";
            this.toolStripMenuItem_List_ViewBiorythm.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem_List_ViewBiorythm.Text = "View &Biorythm";
            this.toolStripMenuItem_List_ViewBiorythm.Click += new System.EventHandler(this.toolStripMenuItem_List_ViewBiorythm_Click);
            // 
            // comboBoxSortBy
            // 
            this.comboBoxSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortBy.FormattingEnabled = true;
            this.comboBoxSortBy.Items.AddRange(new object[] {
            "Birthday (without year)",
            "Birth Date (age)",
            "First Name",
            "Last Name",
            "Nickname",
            "Nickname or Name"});
            this.comboBoxSortBy.Location = new System.Drawing.Point(47, 0);
            this.comboBoxSortBy.Name = "comboBoxSortBy";
            this.comboBoxSortBy.Size = new System.Drawing.Size(131, 21);
            this.comboBoxSortBy.TabIndex = 11;
            this.comboBoxSortBy.SelectedIndexChanged += new System.EventHandler(this.comboBoxSortBy_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Sort by:";
            // 
            // ContactListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.comboBoxSortBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.textBoxSearch);
            this.Name = "ContactListView";
            this.Size = new System.Drawing.Size(178, 293);
            this.contextMenuStripListBox.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListBox;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_List_Add;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_List_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_List_ViewBiorythm;
        private System.Windows.Forms.ComboBox comboBoxSortBy;
        private System.Windows.Forms.Label label1;
    }
}
