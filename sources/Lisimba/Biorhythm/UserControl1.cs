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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.UserControls
{
    public partial class UserControl1 : Control
    {
        #region Birthday

        private DateTime birthday = new DateTime(1980, 6, 13);
        [Category("Data")]
        public DateTime Birthday
        {
            get { return this.birthday; }
            set
            {
                if (!value.Equals(birthday))
                {
                    birthday = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region FirstDay

        private DateTime firstDay = DateTime.Now;
        public DateTime FirstDay
        {
            get { return firstDay; }
            set
            {
                if (!value.Equals(firstDay))
                {
                    firstDay = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region TotalDays

        private int totalDays = 30;
        public int TotalDays
        {
            get { return totalDays; }
            set
            {
                if (value != totalDays && value >= 3)
                {
                    totalDays = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region ShowBorder

        private bool showBorder = true;
        [Category("Appearance")]
        public bool ShowBorder
        {
            get { return showBorder; }
            set
            {
                if (value != showBorder)
                {
                    showBorder = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region BorderColor

        private Color borderColor = Color.Black;
        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (!value.Equals(borderColor))
                {
                    borderColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region ShowGrid

        private bool showGrid = true;
        [Category("Appearance")]
        public bool ShowGrid
        {
            get { return showGrid; }
            set
            {
                if (value != showGrid)
                {
                    showGrid = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region GridColor

        private Color gridColor = Color.Black;
        [Category("Appearance")]
        public Color GridColor
        {
            get { return gridColor; }
            set
            {
                if (!value.Equals(gridColor))
                {
                    gridColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region ShowDays

        private bool showDays = true;
        [Category("Appearance")]
        public bool ShowDays
        {
            get { return showDays; }
            set
            {
                if (value != showDays)
                {
                    showDays = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region DaysColor

        private Color daysColor = Color.Black;
        [Category("Appearance")]
        public Color DaysColor
        {
            get { return daysColor; }
            set
            {
                if (!value.Equals(daysColor))
                {
                    daysColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        private Font daysFont = new Font("Arial", 10);

        #region GraficColor

        #region PhysicalGraficColor

        private Color physicalGraficColor = Color.Red;
        [Category("Appearance")]
        public Color PhysicalGraficColor
        {
            get { return physicalGraficColor; }
            set
            {
                if (!value.Equals(physicalGraficColor))
                {
                    physicalGraficColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region EmotionalGraficColor

        private Color emotionalGraficColor = Color.Blue;
        [Category("Appearance")]
        public Color EmotionalGraficColor
        {
            get { return emotionalGraficColor; }
            set
            {
                if (!value.Equals(emotionalGraficColor))
                {
                    emotionalGraficColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region IntelectualGraficColor

        private Color intelectualGraficColor = Color.Green;
        [Category("Appearance")]
        public Color IntelectualGraficColor
        {
            get { return intelectualGraficColor; }
            set
            {
                if (!value.Equals(intelectualGraficColor))
                {
                    intelectualGraficColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #region IntuitionalGraficColor

        private Color intuitionalGraficColor = Color.Lime;
        [Category("Appearance")]
        public Color IntuitionalGraficColor
        {
            get { return intuitionalGraficColor; }
            set
            {
                if (!value.Equals(intuitionalGraficColor))
                {
                    intuitionalGraficColor = value;
                    this.Invalidate();
                }
            }
        }

        #endregion

        #endregion

        #region ShowGrafic

        private bool showPhysicalGrafic = true;
        [Category("Appearance")]
        public bool ShowPhysicalGrafic
        {
            get { return showPhysicalGrafic; }
            set
            {
                if (value != showPhysicalGrafic)
                {
                    showPhysicalGrafic = value;
                    this.Invalidate();
                }
            }
        }

        private bool showEmotionalGrafic = true;
        [Category("Appearance")]
        public bool ShowEmotionalGrafic
        {
            get { return showEmotionalGrafic; }
            set
            {
                if (value != showEmotionalGrafic)
                {
                    showEmotionalGrafic = value;
                    this.Invalidate();
                }
            }
        }

        private bool showIntelectualGrafic = true;
        [Category("Appearance")]
        public bool ShowIntelectualGrafic
        {
            get { return showIntelectualGrafic; }
            set
            {
                if (value != showIntelectualGrafic)
                {
                    showIntelectualGrafic = value;
                    this.Invalidate();
                }
            }
        }

        private bool showIntuitionalGrafic = false;
        [Category("Appearance")]
        public bool ShowIntuitionalGrafic
        {
            get { return showIntuitionalGrafic; }
            set
            {
                if (value != showIntuitionalGrafic)
                {
                    showIntuitionalGrafic = value;
                    this.Invalidate();
                }
            }
        }

        #endregion


        #region Constructors

        public UserControl1()
        {
            this.DoubleBuffered = true;
        }

        public UserControl1(DateTime birthday)
            : this()
        {
            this.birthday = birthday;
        }

        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!this.showPhysicalGrafic && !this.showEmotionalGrafic && !this.showIntelectualGrafic && !this.showIntuitionalGrafic)
            {
                this.DrawEmptyGrafic(g);
                return;
            }
        }

        public void DrawEmptyGrafic(Graphics g)
        {
            // Clear the image
            using (SolidBrush brush = new SolidBrush(this.backgroundColor))
            {
                g.FillRectangle(brush, 0, 0, this.Width, this.Height);
            }

            // Draw the days
            // --------------------------------------------------------------------------------

            //this.DrawDays(g);

            int width = this.Width;
            int height = this.Height;

            Single pas = width / this.totalDays;

            SolidBrush brushDays = new SolidBrush(this.daysColor);
            Pen penGrid = new Pen(this.gridColor);

            DateTime currentDay = this.CurrentDay;
            DateTime day = this.firstDay;

            StringFormat daysStringFormat = new StringFormat();
            daysStringFormat.Alignment = StringAlignment.Center;

            for (int i = 0; i < this.totalDays - 1; i++)
            {
                // Changing today's background color
                //if (DateTime.Compare(day, DateTime.Today) == 0)
                //{
                //    SolidBrush brush = new SolidBrush(this.todayBackColor);
                //    g.FillRectangle(brush, pas * i + 1, 0, pas, graficWidth);
                //    brush.Dispose();
                //}

                if (this.showGrid)
                {
                    //if (this.showCurrentDay)
                    //{
                    //    if (DateTime.Compare(day, currentDay) == 0 ||
                    //        DateTime.Compare(day, currentDay.AddDays(-1)) == 0)
                    //    {
                    //        penGrid.Width = 2;
                    //    }
                    //    else
                    //    {
                    //        penGrid.Width = 1;
                    //    }
                    //}
                    g.DrawLine(penGrid, pas * (i + 1), 0, pas * (i + 1), height-1);
                }

                if (this.showDays)
                {
                    g.DrawString(day.Day.ToString(), this.daysFont, brushDays, new RectangleF(pas * i, height / 2, pas, 50), daysStringFormat);
                }

                day = day.AddDays(1);
            }

            g.DrawLine(penGrid, 0, height / 2, width, height / 2);

            penGrid.Dispose();
            brushDays.Dispose();
            daysStringFormat.Dispose();

            // --------------------------------------------------------------------------------


            // Draw the border
            // --------------------------------------------------------------------------------

            //this.DrawBorder(g);

            if (this.showBorder == true)
            {
                Pen penBorder = new Pen(this.borderColor);
                g.DrawRectangle(penBorder, new Rectangle(0, 0, width - 1, height - 1));

                penBorder.Dispose();
            }

            // --------------------------------------------------------------------------------
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);
        }
    }
}
