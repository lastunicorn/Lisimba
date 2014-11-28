namespace DustInTheWind.Lisimba.UserControls
{
    partial class Biorythm
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
            this.labelFirstDay = new System.Windows.Forms.Label();
            this.labelLastDay = new System.Windows.Forms.Label();
            this.labelDaysLived = new System.Windows.Forms.Label();
            this.checkBoxIntellectual = new System.Windows.Forms.CheckBox();
            this.labelColorIntellectual = new System.Windows.Forms.Label();
            this.checkBoxEmotional = new System.Windows.Forms.CheckBox();
            this.labelColorEmotional = new System.Windows.Forms.Label();
            this.checkBoxPhysical = new System.Windows.Forms.CheckBox();
            this.labelColorPhysical = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.biorythmView1 = new DustInTheWind.Biorhythm.BiorhythmView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelFirstDay.AutoSize = true;
            this.labelFirstDay.Location = new System.Drawing.Point(3, 0);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(67, 13);
            this.labelFirstDay.TabIndex = 1;
            this.labelFirstDay.Text = "labelFirstDay";
            this.labelFirstDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLastDay
            // 
            this.labelLastDay.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelLastDay.AutoSize = true;
            this.labelLastDay.Location = new System.Drawing.Point(762, 0);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(68, 13);
            this.labelLastDay.TabIndex = 1;
            this.labelLastDay.Text = "labelLastDay";
            this.labelLastDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDaysLived
            // 
            this.labelDaysLived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.labelDaysLived.AutoSize = true;
            this.labelDaysLived.Location = new System.Drawing.Point(3, 5);
            this.labelDaysLived.Name = "labelDaysLived";
            this.labelDaysLived.Size = new System.Drawing.Size(473, 13);
            this.labelDaysLived.TabIndex = 1;
            this.labelDaysLived.Text = "labelDaysLived";
            this.labelDaysLived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxIntellectual
            // 
            this.checkBoxIntellectual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxIntellectual.AutoSize = true;
            this.checkBoxIntellectual.Checked = true;
            this.checkBoxIntellectual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIntellectual.Location = new System.Drawing.Point(740, 3);
            this.checkBoxIntellectual.MinimumSize = new System.Drawing.Size(90, 0);
            this.checkBoxIntellectual.Name = "checkBoxIntellectual";
            this.checkBoxIntellectual.Size = new System.Drawing.Size(90, 17);
            this.checkBoxIntellectual.TabIndex = 2;
            this.checkBoxIntellectual.Text = "Intellectual";
            this.checkBoxIntellectual.UseVisualStyleBackColor = true;
            this.checkBoxIntellectual.CheckedChanged += new System.EventHandler(this.checkBoxIntellectual_CheckedChanged);
            // 
            // labelColorIntellectual
            // 
            this.labelColorIntellectual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelColorIntellectual.BackColor = System.Drawing.Color.Green;
            this.labelColorIntellectual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorIntellectual.Location = new System.Drawing.Point(718, 3);
            this.labelColorIntellectual.Name = "labelColorIntellectual";
            this.labelColorIntellectual.Size = new System.Drawing.Size(16, 16);
            this.labelColorIntellectual.TabIndex = 3;
            this.labelColorIntellectual.Click += new System.EventHandler(this.labelColorIntellectual_Click);
            // 
            // checkBoxEmotional
            // 
            this.checkBoxEmotional.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxEmotional.AutoSize = true;
            this.checkBoxEmotional.Checked = true;
            this.checkBoxEmotional.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmotional.Location = new System.Drawing.Point(622, 3);
            this.checkBoxEmotional.MinimumSize = new System.Drawing.Size(90, 0);
            this.checkBoxEmotional.Name = "checkBoxEmotional";
            this.checkBoxEmotional.Size = new System.Drawing.Size(90, 17);
            this.checkBoxEmotional.TabIndex = 2;
            this.checkBoxEmotional.Text = "Emotional";
            this.checkBoxEmotional.UseVisualStyleBackColor = true;
            this.checkBoxEmotional.CheckedChanged += new System.EventHandler(this.checkBoxEmotional_CheckedChanged);
            // 
            // labelColorEmotional
            // 
            this.labelColorEmotional.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelColorEmotional.BackColor = System.Drawing.Color.Blue;
            this.labelColorEmotional.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorEmotional.Location = new System.Drawing.Point(600, 3);
            this.labelColorEmotional.Name = "labelColorEmotional";
            this.labelColorEmotional.Size = new System.Drawing.Size(16, 16);
            this.labelColorEmotional.TabIndex = 3;
            this.labelColorEmotional.Click += new System.EventHandler(this.labelColorEmotional_Click);
            // 
            // checkBoxPhysical
            // 
            this.checkBoxPhysical.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxPhysical.AutoSize = true;
            this.checkBoxPhysical.Checked = true;
            this.checkBoxPhysical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPhysical.Location = new System.Drawing.Point(504, 3);
            this.checkBoxPhysical.MinimumSize = new System.Drawing.Size(90, 0);
            this.checkBoxPhysical.Name = "checkBoxPhysical";
            this.checkBoxPhysical.Size = new System.Drawing.Size(90, 17);
            this.checkBoxPhysical.TabIndex = 2;
            this.checkBoxPhysical.Text = "Physical";
            this.checkBoxPhysical.UseVisualStyleBackColor = true;
            this.checkBoxPhysical.CheckedChanged += new System.EventHandler(this.checkBoxPhysical_CheckedChanged);
            // 
            // labelColorPhysical
            // 
            this.labelColorPhysical.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelColorPhysical.BackColor = System.Drawing.Color.Red;
            this.labelColorPhysical.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorPhysical.Location = new System.Drawing.Point(482, 3);
            this.labelColorPhysical.Name = "labelColorPhysical";
            this.labelColorPhysical.Size = new System.Drawing.Size(16, 16);
            this.labelColorPhysical.TabIndex = 3;
            this.labelColorPhysical.Click += new System.EventHandler(this.labelColorPhysical_Click);
            // 
            // biorythmView1
            // 
            this.biorythmView1.BackColor = System.Drawing.Color.White;
            this.biorythmView1.Birthday = new System.DateTime(1980, 6, 13, 0, 0, 0, 0);
            this.biorythmView1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.biorythmView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biorythmView1.EmotionChartColor = System.Drawing.Color.Blue;
            this.biorythmView1.FirstDay = new System.DateTime(2008, 2, 5, 0, 0, 0, 0);
            this.biorythmView1.ForeColor = System.Drawing.Color.DimGray;
            this.biorythmView1.IntelectChartColor = System.Drawing.Color.Green;
            this.biorythmView1.IntuitionChartColor = System.Drawing.Color.Lime;
            this.biorythmView1.IntuitionChartVisible = false;
            this.biorythmView1.LastDay = new System.DateTime(2008, 3, 5, 0, 0, 0, 0);
            this.biorythmView1.Location = new System.Drawing.Point(3, 22);
            this.biorythmView1.Name = "biorythmView1";
            this.biorythmView1.Size = new System.Drawing.Size(833, 224);
            this.biorythmView1.SundaysFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.biorythmView1.TabIndex = 4;
            this.biorythmView1.Text = "biorythmView1";
            this.biorythmView1.TodayBackColor = System.Drawing.Color.YellowGreen;
            this.biorythmView1.XDay = new System.DateTime(2008, 2, 10, 0, 0, 0, 0);
            this.biorythmView1.XDayBorderColor = System.Drawing.Color.Gray;
            this.biorythmView1.XDayIndex = 5;
            this.biorythmView1.FirstDayChanged += new DustInTheWind.Biorhythm.BiorhythmView.FirstDayChangedHandler(this.biorythmView1_FirstDayChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.biorythmView1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(839, 278);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.labelLastDay, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.labelFirstDay, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(833, 13);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AutoSize = true;
            this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel3.ColumnCount = 7;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.Controls.Add(this.checkBoxIntellectual, 6, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelDaysLived, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelColorPhysical, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelColorIntellectual, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxPhysical, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.labelColorEmotional, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.checkBoxEmotional, 4, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 252);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(833, 23);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // Biorythm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Name = "Biorythm";
            this.Size = new System.Drawing.Size(839, 278);
            this.Load += new System.EventHandler(this.Biorythm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelFirstDay;
        private System.Windows.Forms.Label labelLastDay;
        private System.Windows.Forms.Label labelDaysLived;
        private System.Windows.Forms.CheckBox checkBoxIntellectual;
        private System.Windows.Forms.Label labelColorIntellectual;
        private System.Windows.Forms.CheckBox checkBoxEmotional;
        private System.Windows.Forms.Label labelColorEmotional;
        private System.Windows.Forms.CheckBox checkBoxPhysical;
        private System.Windows.Forms.Label labelColorPhysical;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private DustInTheWind.Biorhythm.BiorhythmView biorythmView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}
