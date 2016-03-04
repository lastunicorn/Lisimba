using DustInTheWind.Lisimba.WinForms.NameEditing;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.nameEditor1 = new NameEditor();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customTreeView1 = new CustomTreeView();
            this.contactDetailsList1 = new ContactDetailsList();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.buttonAddAddress = new System.Windows.Forms.Button();
            this.buttonAddDate = new System.Windows.Forms.Button();
            this.buttonAddEmail = new System.Windows.Forms.Button();
            this.buttonAddSocialProfileId = new System.Windows.Forms.Button();
            this.buttonAddPhone = new System.Windows.Forms.Button();
            this.buttonAddWebSite = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.birthdayView1 = new BirthdayView();
            this.zodiacSignView1 = new ZodiacSignView();
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
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.White;
            this.imageList1.Images.SetKeyName(0, "notes.gif");
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
            this.tableLayoutPanel3.Controls.Add(this.nameEditor1, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.splitContainer2, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(516, 212);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // nameEditor1
            // 
            this.nameEditor1.AutoSize = true;
            this.nameEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameEditor1.Location = new System.Drawing.Point(3, 3);
            this.nameEditor1.Name = "nameEditor1";
            this.nameEditor1.PersonName = null;
            this.nameEditor1.Size = new System.Drawing.Size(510, 38);
            this.nameEditor1.TabIndex = 13;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(3, 61);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tableLayoutPanel4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(510, 148);
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
            this.tableLayoutPanel4.Size = new System.Drawing.Size(312, 148);
            this.tableLayoutPanel4.TabIndex = 13;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customTreeView1);
            this.groupBox1.Controls.Add(this.contactDetailsList1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(306, 85);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            // 
            // customTreeView1
            // 
            this.customTreeView1.BackColor = System.Drawing.SystemColors.Control;
            this.customTreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.customTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customTreeView1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.customTreeView1.FullRowSelect = true;
            this.customTreeView1.ImageIndex = 0;
            this.customTreeView1.Location = new System.Drawing.Point(4, 17);
            this.customTreeView1.Name = "customTreeView1";
            this.customTreeView1.SelectedImageIndex = 0;
            this.customTreeView1.ShowLines = false;
            this.customTreeView1.ShowPlusMinus = false;
            this.customTreeView1.ShowRootLines = false;
            this.customTreeView1.Size = new System.Drawing.Size(298, 64);
            this.customTreeView1.TabIndex = 12;
            // 
            // contactDetailsList1
            // 
            this.contactDetailsList1.AutoScroll = true;
            this.contactDetailsList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactDetailsList1.Location = new System.Drawing.Point(4, 17);
            this.contactDetailsList1.Name = "contactDetailsList1";
            this.contactDetailsList1.Padding = new System.Windows.Forms.Padding(2);
            this.contactDetailsList1.Size = new System.Drawing.Size(298, 64);
            this.contactDetailsList1.TabIndex = 13;
            this.contactDetailsList1.Visible = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.Controls.Add(this.buttonAddAddress);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddDate);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddEmail);
            this.flowLayoutPanel3.Controls.Add(this.buttonAddSocialProfileId);
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
            this.buttonAddAddress.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.address_add;
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
            this.buttonAddDate.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.date_add;
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
            this.buttonAddEmail.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.email_add;
            this.buttonAddEmail.Location = new System.Drawing.Point(105, 3);
            this.buttonAddEmail.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddEmail.Name = "buttonAddEmail";
            this.buttonAddEmail.Padding = new System.Windows.Forms.Padding(1);
            this.buttonAddEmail.Size = new System.Drawing.Size(45, 45);
            this.buttonAddEmail.TabIndex = 13;
            this.buttonAddEmail.UseVisualStyleBackColor = true;
            this.buttonAddEmail.Click += new System.EventHandler(this.buttonAddEmail_Click);
            // 
            // buttonAddSocialProfileId
            // 
            this.buttonAddSocialProfileId.AutoSize = true;
            this.buttonAddSocialProfileId.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddSocialProfileId.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.mesengerid_add;
            this.buttonAddSocialProfileId.Location = new System.Drawing.Point(156, 3);
            this.buttonAddSocialProfileId.MinimumSize = new System.Drawing.Size(45, 45);
            this.buttonAddSocialProfileId.Name = "buttonAddSocialProfileId";
            this.buttonAddSocialProfileId.Size = new System.Drawing.Size(45, 45);
            this.buttonAddSocialProfileId.TabIndex = 13;
            this.buttonAddSocialProfileId.UseVisualStyleBackColor = true;
            this.buttonAddSocialProfileId.Click += new System.EventHandler(this.buttonAddSocialProfileId_Click);
            // 
            // buttonAddPhone
            // 
            this.buttonAddPhone.AutoSize = true;
            this.buttonAddPhone.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonAddPhone.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.phone_add;
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
            this.buttonAddWebSite.Image = global::DustInTheWind.Lisimba.WinForms.Properties.Resources.webaddress_add;
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
            this.tableLayoutPanel1.Controls.Add(this.birthdayView1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.zodiacSignView1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 148);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // birthdayView1
            // 
            this.birthdayView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.birthdayView1.AutoSize = true;
            this.birthdayView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.birthdayView1.Birthday = null;
            this.birthdayView1.Location = new System.Drawing.Point(62, 3);
            this.birthdayView1.Name = "birthdayView1";
            this.birthdayView1.Size = new System.Drawing.Size(69, 23);
            this.birthdayView1.TabIndex = 16;
            // 
            // zodiacSignView1
            // 
            this.zodiacSignView1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.zodiacSignView1.AutoSize = true;
            this.zodiacSignView1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.zodiacSignView1.Location = new System.Drawing.Point(74, 59);
            this.zodiacSignView1.Name = "zodiacSignView1";
            this.zodiacSignView1.Size = new System.Drawing.Size(45, 58);
            this.zodiacSignView1.TabIndex = 17;
            this.zodiacSignView1.ZodiacSign = DustInTheWind.Lisimba.Egg.AddressBookModel.ZodiacSign.NotSpecified;
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(3, 49);
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
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Button buttonAddAddress;
        private System.Windows.Forms.Button buttonAddEmail;
        private System.Windows.Forms.Button buttonAddWebSite;
        private System.Windows.Forms.Button buttonAddDate;
        private System.Windows.Forms.Button buttonAddPhone;
        private System.Windows.Forms.Button buttonAddSocialProfileId;
        private CustomTreeView customTreeView1;
        private NameEditor nameEditor1;
        private ContactDetailsList contactDetailsList1;
        private BirthdayView birthdayView1;
        private ZodiacSignView zodiacSignView1;
    }
}
