// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

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
            set { biorythmView1.Birthday = value; }
        }

        [Category("Data")]
        public DateTime CurrentDay
        {
            get { return DateTime.Now; }
            set { biorythmView1.XDay = value; }
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
