using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Lisimba.ContactList
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
            this.treeViewContacts = new System.Windows.Forms.TreeView();
            this.contextMenuStripListBox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem_List_Add = new DustInTheWind.Lisimba.Utils.CustomMenuItem(this.components);
            this.toolStripMenuItem_List_Delete = new DustInTheWind.Lisimba.Utils.CustomMenuItem(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem_List_ViewBiorythm = new DustInTheWind.Lisimba.Utils.CustomMenuItem(this.components);
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.comboBoxSortBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStripListBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeViewContacts
            // 
            this.treeViewContacts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeViewContacts.ContextMenuStrip = this.contextMenuStripListBox;
            this.treeViewContacts.FullRowSelect = true;
            this.treeViewContacts.HideSelection = false;
            this.treeViewContacts.Location = new System.Drawing.Point(0, 27);
            this.treeViewContacts.Name = "treeViewContacts";
            this.treeViewContacts.ShowLines = false;
            this.treeViewContacts.ShowPlusMinus = false;
            this.treeViewContacts.Size = new System.Drawing.Size(178, 240);
            this.treeViewContacts.TabIndex = 2;
            this.treeViewContacts.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeViewContacts.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewContacts_NodeMouseClick);
            this.treeViewContacts.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // contextMenuStripListBox
            // 
            this.contextMenuStripListBox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_List_Add,
            this.toolStripMenuItem_List_Delete,
            this.toolStripMenuItem1,
            this.toolStripMenuItem_List_ViewBiorythm});
            this.contextMenuStripListBox.Name = "contextMenuStripList";
            this.contextMenuStripListBox.Size = new System.Drawing.Size(153, 98);
            // 
            // toolStripMenuItem_List_Add
            // 
            this.toolStripMenuItem_List_Add.Name = "toolStripMenuItem_List_Add";
            this.toolStripMenuItem_List_Add.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_List_Add.Text = "&Add Contact";
            // 
            // toolStripMenuItem_List_Delete
            // 
            this.toolStripMenuItem_List_Delete.Name = "toolStripMenuItem_List_Delete";
            this.toolStripMenuItem_List_Delete.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_List_Delete.Text = "&Delete Contact";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(149, 6);
            // 
            // toolStripMenuItem_List_ViewBiorythm
            // 
            this.toolStripMenuItem_List_ViewBiorythm.Name = "toolStripMenuItem_List_ViewBiorythm";
            this.toolStripMenuItem_List_ViewBiorythm.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem_List_ViewBiorythm.Text = "View &Biorythm";
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSearch.Location = new System.Drawing.Point(23, 3);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(155, 20);
            this.textBoxSearch.TabIndex = 3;
            // 
            // comboBoxSortBy
            // 
            this.comboBoxSortBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxSortBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSortBy.FormattingEnabled = true;
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
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.textBoxSearch, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 267);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(178, 26);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.Image = global::DustInTheWind.Lisimba.Properties.Resources.search;
            this.pictureBox1.InitialImage = global::DustInTheWind.Lisimba.Properties.Resources.search;
            this.pictureBox1.Location = new System.Drawing.Point(3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(14, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // ContactListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.comboBoxSortBy);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.treeViewContacts);
            this.Name = "ContactListView";
            this.Size = new System.Drawing.Size(178, 293);
            this.Load += new System.EventHandler(this.ContactListView_Load);
            this.contextMenuStripListBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewContacts;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripListBox;
        private CustomMenuItem toolStripMenuItem_List_Add;
        private CustomMenuItem toolStripMenuItem_List_Delete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private CustomMenuItem toolStripMenuItem_List_ViewBiorythm;
        private System.Windows.Forms.ComboBox comboBoxSortBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
