using System;
using System.ComponentModel;
using System.Windows.Forms;

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
                biorythmView1.Birthday = value;
            }
        }

        [Category("Data")]
        public DateTime CurrentDay
        {
            get { return DateTime.Now; }
            set
            {
                biorythmView1.XDay = value;
            }
        }

        public Biorythm()
        {
            InitializeComponent();
        }

        private void RefreshData()
        {
            labelFirstDay.Text = biorythmView1.FirstDay.ToLongDateString();
            labelLastDay.Text = biorythmView1.LastDay.ToLongDateString();
            labelDaysLived.Text = "You have lived " + biorythmView1.DaysLivedUntilXDay + " days";
        }

        public void AddDays(int days)
        {
            biorythmView1.SlideChart(days);
        }

        #region Checkbox

        private void checkBoxPhysical_CheckedChanged(object sender, EventArgs e)
        {
            biorythmView1.PhysicChartVisible = checkBoxPhysical.Checked;
        }

        private void checkBoxEmotional_CheckedChanged(object sender, EventArgs e)
        {
            biorythmView1.EmotionChartVisible = checkBoxEmotional.Checked;
        }

        private void checkBoxIntellectual_CheckedChanged(object sender, EventArgs e)
        {
            biorythmView1.IntelectChartVisible = checkBoxIntellectual.Checked;
        }

        private void labelColorPhysical_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                labelColorPhysical.BackColor = colorDialog1.Color;
                biorythmView1.PhysicChartColor = labelColorPhysical.BackColor;
            }
        }

        private void labelColorEmotional_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                labelColorEmotional.BackColor = colorDialog1.Color;
                biorythmView1.EmotionChartColor = labelColorEmotional.BackColor;
            }
        }

        private void labelColorIntellectual_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                labelColorIntellectual.BackColor = colorDialog1.Color;
                biorythmView1.IntelectChartColor = labelColorIntellectual.BackColor;
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
