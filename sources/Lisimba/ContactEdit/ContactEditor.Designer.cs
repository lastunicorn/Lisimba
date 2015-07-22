using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.ContactEdit
{
    partial class ContactEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactEditor));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.labelZodiacSign = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customTreeView1 = new DustInTheWind.Lisimba.ContactEdit.CustomTreeView();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddAddress = new System.Windows.Forms.Button();
            this.buttonAddDate = new System.Windows.Forms.Button();
            this.buttonAddEmail = new System.Windows.Forms.Button();
            this.buttonAddMessengerId = new System.Windows.Forms.Button();
            this.buttonAddPhone = new System.Windows.Forms.Button();
            this.buttonAddWebSite = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxZodiacSign = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelBirthday = new System.Windows.Forms.Label();
            this.labelFullName = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZodiacSign)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "notes.gif");
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(3, 3);
            this.label7.Margin = new System.Windows.Forms.Padding(3);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Birthday:";
            // 
            // labelZodiacSign
            // 
            this.labelZodiacSign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelZodiacSign.AutoSize = true;
            this.labelZodiacSign.Location = new System.Drawing.Point(3, 45);
            this.labelZodiacSign.Name = "labelZodiacSign";
            this.labelZodiacSign.Size = new System.Drawing.Size(61, 13);
            this.labelZodiacSign.TabIndex = 11;
            this.labelZodiacSign.Text = "ZodiacSign";
            this.labelZodiacSign.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 0;
            this.toolTip1.ReshowDelay = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(516, 375);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 9;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.splitContainer2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.labelFullName, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(516, 212);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(3, 54);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(510, 155);
            this.splitContainer2.SplitterDistance = 312;
            this.splitContainer2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.flowLayoutPanel3, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(312, 155);
            this.tableLayoutPanel4.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customTreeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(306, 92);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // customTreeView1
            // 
            this.customTreeView1.Addresses = null;
            this.customTreeView1.BackColor = System.Drawing.SystemColors.Control;
            this.customTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customTreeView1.Dates = null;
            this.customTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTreeView1.Emails = null;
            this.customTreeView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.customTreeView1.FullRowSelect = true;
            this.customTreeView1.ImageIndex = 0;
            this.customTreeView1.Location = new System.Drawing.Point(4, 17);
            this.customTreeView1.MessengerIds = null;
            this.customTreeView1.Name = "customTreeView1";
            this.customTreeView1.Phones = null;
            this.customTreeView1.SelectedImageIndex = 0;
            this.customTreeView1.ShowLines = false;
            this.customTreeView1.ShowPlusMinus = false;
            this.customTreeView1.ShowRootLines = false;
            this.customTreeView1.Size = new System.Drawing.Size(298, 71);
            this.customTreeView1.TabIndex = 12;
            this.customTreeView1.WebSites = null;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.buttonAddAddress);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddDate);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddEmail);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddMessengerId);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddPhone);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddWebSite);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(306, 51);
            this.flowLayoutPanel3.TabIndex = 14;
            this.flowLayoutPanel3.WrapContents = false;
            // 
            // buttonAddAddress
            // 
            this.buttonAddAddress.AutoSize = true;
            this.buttonAddAddress.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddAddress.Image = global::DustInTheWind.Lisimba.Properties.Resources.address_add;
            this.buttonAddAddress.Location = new System.Drawing.Point(3, 3);
            this.buttonAddAddress.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddAddress.Name = "buttonAddAddress";
            this.buttonAddAddress.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddAddress.Size = new System.Drawing.Size(45, 45);
            this.buttonAddAddress.TabIndex = 13;
            this.buttonAddAddress.UseVisualStyleBackColor = true;
            this.buttonAddAddress.Click += new System.EventHandler(this.buttonAddAddress_Click);
            // 
            // buttonAddDate
            // 
            this.buttonAddDate.AutoSize = true;
            this.buttonAddDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddDate.Image = global::DustInTheWind.Lisimba.Properties.Resources.date_add;
            this.buttonAddDate.Location = new System.Drawing.Point(54, 3);
            this.buttonAddDate.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddDate.Name = "buttonAddDate";
            this.buttonAddDate.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddDate.Size = new System.Drawing.Size(45, 45);
            this.buttonAddDate.TabIndex = 13;
            this.buttonAddDate.UseVisualStyleBackColor = true;
            this.buttonAddDate.Click += new System.EventHandler(this.buttonAddDate_Click);
            // 
            // buttonAddEmail
            // 
            this.buttonAddEmail.AutoSize = true;
            this.buttonAddEmail.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddEmail.Image = global::DustInTheWind.Lisimba.Properties.Resources.email_add;
            this.buttonAddEmail.Location = new System.Drawing.Point(105, 3);
            this.buttonAddEmail.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddEmail.Name = "buttonAddEmail";
            this.buttonAddEmail.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddEmail.Size = new System.Drawing.Size(45, 45);
            this.buttonAddEmail.TabIndex = 13;
            this.buttonAddEmail.UseVisualStyleBackColor = true;
            this.buttonAddEmail.Click += new System.EventHandler(this.buttonAddEmail_Click);
            // 
            // buttonAddMessengerId
            // 
            this.buttonAddMessengerId.AutoSize = true;
            this.buttonAddMessengerId.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddMessengerId.Image = global::DustInTheWind.Lisimba.Properties.Resources.mesengerid_add;
            this.buttonAddMessengerId.Location = new System.Drawing.Point(156, 3);
            this.buttonAddMessengerId.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddMessengerId.Name = "buttonAddMessengerId";
            this.buttonAddMessengerId.Size = new System.Drawing.Size(45, 45);
            this.buttonAddMessengerId.TabIndex = 13;
            this.buttonAddMessengerId.UseVisualStyleBackColor = true;
            this.buttonAddMessengerId.Click += new System.EventHandler(this.buttonAddMessengerId_Click);
            // 
            // buttonAddPhone
            // 
            this.buttonAddPhone.AutoSize = true;
            this.buttonAddPhone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddPhone.Image = global::DustInTheWind.Lisimba.Properties.Resources.phone_add;
            this.buttonAddPhone.Location = new System.Drawing.Point(207, 3);
            this.buttonAddPhone.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddPhone.Name = "buttonAddPhone";
            this.buttonAddPhone.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddPhone.Size = new System.Drawing.Size(45, 45);
            this.buttonAddPhone.TabIndex = 13;
            this.buttonAddPhone.UseVisualStyleBackColor = true;
            this.buttonAddPhone.Click += new System.EventHandler(this.buttonAddPhone_Click);
            // 
            // buttonAddWebSite
            // 
            this.buttonAddWebSite.AutoSize = true;
            this.buttonAddWebSite.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddWebSite.Image = global::DustInTheWind.Lisimba.Properties.Resources.webaddress_add;
            this.buttonAddWebSite.Location = new System.Drawing.Point(258, 3);
            this.buttonAddWebSite.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddWebSite.Name = "buttonAddWebSite";
            this.buttonAddWebSite.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddWebSite.Size = new System.Drawing.Size(45, 45);
            this.buttonAddWebSite.TabIndex = 13;
            this.buttonAddWebSite.UseVisualStyleBackColor = true;
            this.buttonAddWebSite.Click += new System.EventHandler(this.buttonAddWebSite_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 155);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.pictureBoxZodiacSign);
            this.flowLayoutPanel1.Controls.Add(this.labelZodiacSign);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(63, 61);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(67, 58);
            this.flowLayoutPanel1.TabIndex = 14;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // pictureBoxZodiacSign
            // 
            this.pictureBoxZodiacSign.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxZodiacSign.Location = new System.Drawing.Point(14, 3);
            this.pictureBoxZodiacSign.Name = "pictureBoxZodiacSign";
            this.pictureBoxZodiacSign.Size = new System.Drawing.Size(39, 39);
            this.pictureBoxZodiacSign.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxZodiacSign.TabIndex = 10;
            this.pictureBoxZodiacSign.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Controls.Add(this.labelBirthday);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(65, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(63, 19);
            this.flowLayoutPanel2.TabIndex = 15;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // labelBirthday
            // 
            this.labelBirthday.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelBirthday.AutoSize = true;
            this.labelBirthday.Location = new System.Drawing.Point(63, 3);
            this.labelBirthday.Margin = new System.Windows.Forms.Padding(0);
            this.labelBirthday.Name = "labelBirthday";
            this.labelBirthday.Size = new System.Drawing.Size(0, 13);
            this.labelBirthday.TabIndex = 13;
            this.labelBirthday.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelBirthday.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labelBirthday_MouseDoubleClick);
            // 
            // labelFullName
            // 
            this.labelFullName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFullName.AutoSize = true;
            this.labelFullName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFullName.Location = new System.Drawing.Point(3, 3);
            this.labelFullName.Margin = new System.Windows.Forms.Padding(3);
            this.labelFullName.Name = "labelFullName";
            this.labelFullName.Size = new System.Drawing.Size(510, 31);
            this.labelFullName.TabIndex = 1;
            this.labelFullName.Text = "Full Name";
            this.labelFullName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.labelFullName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.labelFullName_MouseDoubleClick);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(3, 42);
            this.label8.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(510, 4);
            this.label8.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.textBoxNotes, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(516, 159);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Image = ((System.Drawing.Image)(resources.GetObject("label4.Image")));
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.MinimumSize = new System.Drawing.Size(0, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 14);
            this.label4.TabIndex = 8;
            this.label4.Text = "     Notes:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.AcceptsReturn = true;
            this.textBoxNotes.AcceptsTab = true;
            this.textBoxNotes.BackColor = System.Drawing.Color.Ivory;
            this.textBoxNotes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNotes.Location = new System.Drawing.Point(3, 23);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxNotes.Size = new System.Drawing.Size(510, 133);
            this.textBoxNotes.TabIndex = 5;
            this.textBoxNotes.WordWrap = false;
            // 
            // ContactEditor
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.Black;
            this.MinimumSize = new System.Drawing.Size(516, 0);
            this.Name = "ContactEditor";
            this.Size = new System.Drawing.Size(516, 375);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxZodiacSign)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox pictureBoxZodiacSign;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label labelZodiacSign;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.Label labelBirthday;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label labelFullName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonAddAddress;
        private System.Windows.Forms.Button buttonAddEmail;
        private System.Windows.Forms.Button buttonAddWebSite;
        private System.Windows.Forms.Button buttonAddDate;
        private System.Windows.Forms.Button buttonAddPhone;
        private System.Windows.Forms.Button buttonAddMessengerId;
        private CustomTreeView customTreeView1;
    }
}
