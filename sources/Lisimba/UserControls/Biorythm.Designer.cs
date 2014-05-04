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
            this.SuspendLayout();
            // 
            // labelFirstDay
            // 
            this.labelFirstDay.Location = new System.Drawing.Point(3, 0);
            this.labelFirstDay.Name = "labelFirstDay";
            this.labelFirstDay.Size = new System.Drawing.Size(198, 16);
            this.labelFirstDay.TabIndex = 1;
            this.labelFirstDay.Text = "labelFirstDay";
            this.labelFirstDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLastDay
            // 
            this.labelLastDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLastDay.Location = new System.Drawing.Point(487, 0);
            this.labelLastDay.Name = "labelLastDay";
            this.labelLastDay.Size = new System.Drawing.Size(198, 16);
            this.labelLastDay.TabIndex = 1;
            this.labelLastDay.Text = "labelLastDay";
            this.labelLastDay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelDaysLived
            // 
            this.labelDaysLived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelDaysLived.Location = new System.Drawing.Point(3, 260);
            this.labelDaysLived.Name = "labelDaysLived";
            this.labelDaysLived.Size = new System.Drawing.Size(136, 16);
            this.labelDaysLived.TabIndex = 1;
            this.labelDaysLived.Text = "labelDaysLived";
            this.labelDaysLived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxIntellectual
            // 
            this.checkBoxIntellectual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxIntellectual.Checked = true;
            this.checkBoxIntellectual.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIntellectual.Location = new System.Drawing.Point(584, 260);
            this.checkBoxIntellectual.Name = "checkBoxIntellectual";
            this.checkBoxIntellectual.Size = new System.Drawing.Size(104, 16);
            this.checkBoxIntellectual.TabIndex = 2;
            this.checkBoxIntellectual.Text = "Intellectual";
            this.checkBoxIntellectual.UseVisualStyleBackColor = true;
            this.checkBoxIntellectual.CheckedChanged += new System.EventHandler(this.checkBoxIntellectual_CheckedChanged);
            // 
            // labelColorIntellectual
            // 
            this.labelColorIntellectual.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColorIntellectual.BackColor = System.Drawing.Color.Green;
            this.labelColorIntellectual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorIntellectual.Location = new System.Drawing.Point(562, 260);
            this.labelColorIntellectual.Name = "labelColorIntellectual";
            this.labelColorIntellectual.Size = new System.Drawing.Size(16, 16);
            this.labelColorIntellectual.TabIndex = 3;
            this.labelColorIntellectual.Click += new System.EventHandler(this.labelColorIntellectual_Click);
            // 
            // checkBoxEmotional
            // 
            this.checkBoxEmotional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxEmotional.Checked = true;
            this.checkBoxEmotional.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEmotional.Location = new System.Drawing.Point(452, 260);
            this.checkBoxEmotional.Name = "checkBoxEmotional";
            this.checkBoxEmotional.Size = new System.Drawing.Size(104, 16);
            this.checkBoxEmotional.TabIndex = 2;
            this.checkBoxEmotional.Text = "Emotional";
            this.checkBoxEmotional.UseVisualStyleBackColor = true;
            this.checkBoxEmotional.CheckedChanged += new System.EventHandler(this.checkBoxEmotional_CheckedChanged);
            // 
            // labelColorEmotional
            // 
            this.labelColorEmotional.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColorEmotional.BackColor = System.Drawing.Color.Blue;
            this.labelColorEmotional.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorEmotional.Location = new System.Drawing.Point(430, 260);
            this.labelColorEmotional.Name = "labelColorEmotional";
            this.labelColorEmotional.Size = new System.Drawing.Size(16, 16);
            this.labelColorEmotional.TabIndex = 3;
            this.labelColorEmotional.Click += new System.EventHandler(this.labelColorEmotional_Click);
            // 
            // checkBoxPhysical
            // 
            this.checkBoxPhysical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxPhysical.Checked = true;
            this.checkBoxPhysical.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPhysical.Location = new System.Drawing.Point(320, 260);
            this.checkBoxPhysical.Name = "checkBoxPhysical";
            this.checkBoxPhysical.Size = new System.Drawing.Size(104, 16);
            this.checkBoxPhysical.TabIndex = 2;
            this.checkBoxPhysical.Text = "Physical";
            this.checkBoxPhysical.UseVisualStyleBackColor = true;
            this.checkBoxPhysical.CheckedChanged += new System.EventHandler(this.checkBoxPhysical_CheckedChanged);
            // 
            // labelColorPhysical
            // 
            this.labelColorPhysical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelColorPhysical.BackColor = System.Drawing.Color.Red;
            this.labelColorPhysical.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelColorPhysical.Location = new System.Drawing.Point(298, 260);
            this.labelColorPhysical.Name = "labelColorPhysical";
            this.labelColorPhysical.Size = new System.Drawing.Size(16, 16);
            this.labelColorPhysical.TabIndex = 3;
            this.labelColorPhysical.Click += new System.EventHandler(this.labelColorPhysical_Click);
            // 
            // biorythmView1
            // 
            this.biorythmView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.biorythmView1.BackColor = System.Drawing.Color.White;
            this.biorythmView1.Birthday = new System.DateTime(1980, 6, 13, 0, 0, 0, 0);
            this.biorythmView1.Cursor = System.Windows.Forms.Cursors.SizeWE;
            this.biorythmView1.EmotionChartColor = System.Drawing.Color.Blue;
            this.biorythmView1.FirstDay = new System.DateTime(2008, 2, 9, 0, 0, 0, 0);
            this.biorythmView1.ForeColor = System.Drawing.Color.DimGray;
            this.biorythmView1.IntelectChartColor = System.Drawing.Color.Green;
            this.biorythmView1.IntuitionChartColor = System.Drawing.Color.Lime;
            this.biorythmView1.IntuitionChartVisible = false;
            this.biorythmView1.LastDay = new System.DateTime(2008, 3, 9, 0, 0, 0, 0);
            this.biorythmView1.Location = new System.Drawing.Point(0, 19);
            this.biorythmView1.Name = "biorythmView1";
            this.biorythmView1.Size = new System.Drawing.Size(688, 238);
            this.biorythmView1.SundaysFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.biorythmView1.TabIndex = 4;
            this.biorythmView1.Text = "biorythmView1";
            this.biorythmView1.TodayBackColor = System.Drawing.Color.YellowGreen;
            this.biorythmView1.XDay = new System.DateTime(2008, 2, 14, 0, 0, 0, 0);
            this.biorythmView1.XDayBorderColor = System.Drawing.Color.Gray;
            this.biorythmView1.XDayIndex = 5;
            this.biorythmView1.FirstDayChanged += new DustInTheWind.Biorhythm.BiorhythmView.FirstDayChangedHandler(this.biorythmView1_FirstDayChanged);
            // 
            // Biorythm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.biorythmView1);
            this.Controls.Add(this.labelColorPhysical);
            this.Controls.Add(this.labelColorEmotional);
            this.Controls.Add(this.labelColorIntellectual);
            this.Controls.Add(this.checkBoxPhysical);
            this.Controls.Add(this.checkBoxEmotional);
            this.Controls.Add(this.checkBoxIntellectual);
            this.Controls.Add(this.labelLastDay);
            this.Controls.Add(this.labelDaysLived);
            this.Controls.Add(this.labelFirstDay);
            this.DoubleBuffered = true;
            this.Name = "Biorythm";
            this.Size = new System.Drawing.Size(688, 276);
            this.Load += new System.EventHandler(this.Biorythm_Load);
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
    }
}
