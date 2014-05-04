using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Biorhythm;

namespace DustInTheWind.Lisimba.UserControls
{
    public partial class Biorythm : UserControl
    {
        [Category("Data")]
        public DateTime Birthday
        {
            get { return DateTime.Now; }
            set
            {
                this.biorythmView1.Birthday = value;
            }
        }

        [Category("Data")]
        public DateTime CurrentDay
        {
            get { return DateTime.Now; }
            set
            {
                this.biorythmView1.XDay = value;
            }
        }

        public Biorythm()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            this.labelFirstDay.Text = this.biorythmView1.FirstDay.ToLongDateString();
            this.labelLastDay.Text = this.biorythmView1.LastDay.ToLongDateString();
            this.labelDaysLived.Text = "You have lived " + this.biorythmView1.DaysLivedUntilXDay + " days";
        }

        public void AddDays(int days)
        {
            this.biorythmView1.SlideChart(days);
        }

        #region Checkbox

        private void checkBoxPhysical_CheckedChanged(object sender, EventArgs e)
        {
            this.biorythmView1.PhysicChartVisible = checkBoxPhysical.Checked;
        }

        private void checkBoxEmotional_CheckedChanged(object sender, EventArgs e)
        {
            this.biorythmView1.EmotionChartVisible = checkBoxEmotional.Checked;
        }

        private void checkBoxIntellectual_CheckedChanged(object sender, EventArgs e)
        {
            this.biorythmView1.IntelectChartVisible = checkBoxIntellectual.Checked;
        }

        private void labelColorPhysical_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.labelColorPhysical.BackColor = this.colorDialog1.Color;
                this.biorythmView1.PhysicChartColor = labelColorPhysical.BackColor;
            }
        }

        private void labelColorEmotional_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.labelColorEmotional.BackColor = this.colorDialog1.Color;
                this.biorythmView1.EmotionChartColor = labelColorEmotional.BackColor;
            }
        }

        private void labelColorIntellectual_Click(object sender, EventArgs e)
        {
            if (this.colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                this.labelColorIntellectual.BackColor = this.colorDialog1.Color;
                this.biorythmView1.IntelectChartColor = labelColorIntellectual.BackColor;
            }
        }

        #endregion

        private void biorythmView1_FirstDayChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void Biorythm_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
