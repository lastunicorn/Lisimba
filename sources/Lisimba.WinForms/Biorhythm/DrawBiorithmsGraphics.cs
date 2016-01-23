// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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
using System.Text;
using System.Drawing;

namespace DustInTheWind.Lisimba
{
    class DrawBiorithmsGraphics
    {
        private const int PERIOADA_PHYSICAL = 23;
        private const int PERIOADA_EMOTIONAL = 28;
        private const int PERIOADA_INTELECTUAL = 33;
        private const int PERIOADA_INTUITIONAL = 38;

        #region Properties

        #region Days

        private DateTime birthday = DateTime.Now;
        public DateTime Birthday
        {
            get { return birthday; }
            set { birthday = value; }
        }

        private DateTime firstDay = DateTime.Today.AddDays(-5);
        public DateTime FirstDay
        {
            get { return firstDay; }
            set { firstDay = value; }
        }

        public DateTime CurrentDay
        {
            get { return firstDay.AddDays(this.currentDayIndex-1); }
            set { firstDay = value.AddDays(-(this.currentDayIndex - 1)); }
        }

        public DateTime LastDay
        {
            get { return firstDay.AddDays(this.totalDays-1); }
            set { firstDay = value.AddDays(-(this.totalDays - 1)); }
        }

        private int currentDayIndex = 5;
        public int CurrentDayIndex
        {
            get { return currentDayIndex; }
            set { if (value >= 0 && value < totalDays) currentDayIndex = value; }
        }

        private int totalDays = 30;
        public int TotalDays
        {
            get { return totalDays; }
            set { if (value >= 3) totalDays = value; }
        }

        #endregion

        #region Days Lived

        public int DaysLivedUntilCurrentDay
        {
            get
            {
                //TimeSpan timeDiference = new TimeSpan(this.CurrentDay.Ticks - this.birthday.Ticks);
                //return timeDiference.Days;
                return (this.CurrentDay - this.birthday).Days;
            }
        }

        public int DaysLivedUntilFirstDay
        {
            get
            {
                //TimeSpan timeDiference = new TimeSpan(this.firstDay.Ticks - this.birthday.Ticks);
                //return timeDiference.Days;
                return (this.firstDay - this.birthday).Days;
            }
        }

        /// <summary>
        /// Returns the days betwin birthday and today
        /// </summary>
        public int DaysLivedUntilToday
        {
            get
            {
                TimeSpan timeDiference = new TimeSpan(DateTime.Today.Ticks - this.birthday.Ticks);
                return timeDiference.Days;
            }
        }

        #endregion

        #region Width / Height

        //private int graficWidth = 600;
        //public int GraficWidth
        //{
        //    get { return graficWidth; }
        //    set { if (value > 50 && value < 1000) graficWidth = value; }
        //}

        //private int graficHeight = 200;
        //public int GraficHeight
        //{
        //    get { return graficHeight; }
        //    set { if (value > 50 && value < 1000) graficHeight = value; }
        //}

        #endregion

        #region Grid Border Days

        private bool showBorder = true;
        public bool ShowBorder
        {
            get { return showBorder; }
            set { showBorder = value; }
        }

        private Color borderColor = Color.Black;
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; }
        }

        private bool showGrid = true;
        public bool ShowGrid
        {
            get { return showBorder; }
            set { showBorder = value; }
        }

        private Color gridColor = Color.Black;
        public Color GridColor
        {
            get { return gridColor; }
            set { gridColor = value; }
        }

        private bool showCurrentDay = true;
        public bool ShowCurrentDay
        {
            get { return showCurrentDay; }
            set { showCurrentDay = value; }
        }

        private bool showDays = true;
        public bool ShowDays
        {
            get { return showDays; }
            set { showDays = value; }
        }

        private Color daysColor = Color.Black;
        public Color DaysColor
        {
            get { return daysColor; }
            set { daysColor = value; }
        }

        private Font daysFont = new Font("Arial", 10);
        public Font DaysFont
        {
            get { return daysFont; }
            set { daysFont = value; }
        }

        #endregion

        #region Background Color

        private Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        private Color todayBackColor = Color.Moccasin;
        public Color TodayBackColor
        {
            get { return todayBackColor; }
            set { todayBackColor = value; }
        }

        #endregion

        #region GraficColor

        private Color physicalGraficColor = Color.Red;
        public Color PhysicalGraficColor
        {
            get { return physicalGraficColor; }
            set { physicalGraficColor = value; }
        }

        private Color emotionalGraficColor = Color.Blue;
        public Color EmotionalGraficColor
        {
            get { return emotionalGraficColor; }
            set { emotionalGraficColor = value; }
        }

        private Color intelectualGraficColor = Color.Green;
        public Color IntelectualGraficColor
        {
            get { return intelectualGraficColor; }
            set { intelectualGraficColor = value; }
        }

        private Color intuitionalGraficColor = Color.Lime;
        public Color IntuitionalGraficColor
        {
            get { return intuitionalGraficColor; }
            set { intuitionalGraficColor = value; }
        }

        #endregion

        #region ShowGrafic

        private bool showPhysicalGrafic = true;
        public bool ShowPhysicalGrafic
        {
            get { return showPhysicalGrafic; }
            set { showPhysicalGrafic = value; }
        }

        private bool showEmotionalGrafic = true;
        public bool ShowEmotionalGrafic
        {
            get { return showEmotionalGrafic; }
            set { showEmotionalGrafic = value; }
        }

        private bool showIntelectualGrafic = true;
        public bool ShowIntelectualGrafic
        {
            get { return showIntelectualGrafic; }
            set { showIntelectualGrafic = value; }
        }

        private bool showIntuitionalGrafic = false;
        public bool ShowIntuitionalGrafic
        {
            get { return showIntuitionalGrafic; }
            set { showIntuitionalGrafic = value; }
        }

        #endregion

        #endregion

        #region Constructors

        public DrawBiorithmsGraphics()
        {
        }

        public DrawBiorithmsGraphics(DateTime birthDay)
        {
            this.birthday = birthDay;
        }

        #endregion

        ///// <summary>
        ///// Returns a Bitmap with the biorythm.
        ///// </summary>
        ///// <returns>A Bitmap with the biorythm.</returns>
        //public Bitmap DrawAll()
        //{
        //    Bitmap img = new Bitmap(this.graficWidth, this.graficHeight);
        //    Graphics g = Graphics.FromImage(img);
        //    this.DrawAll(g);
        //    return img;
        //}

        //private Rectangle rect = Rectangle.Empty;
        //public Rectangle Rect
        //{
        //    get { return rect; }
        //    set { rect = value; }
        //}

        /// <summary>
        /// Drawing all the biorythms graphics using the g object.
        /// </summary>
        /// <param name="g"></param>
        public void DrawAll(Graphics g, Rectangle rect)
        //public void DrawAll(Graphics g)
        {
            if (g == null)
                throw new ArgumentNullException("g");

            if (rect == null)
                throw new ArgumentNullException("rect");

            //this.rect = rect;

            // Test the existece of the birthday
            if (this.birthday == null ||
               !this.showPhysicalGrafic && !this.showEmotionalGrafic && !this.showIntelectualGrafic && !this.showIntuitionalGrafic)
            {
                this.DrawEmptyGrafic(g, rect);
                return;
            }

            int graficWidth = rect.Width;
            int graficHeight = rect.Height;

            // Variable declarations
            Pen penDays = new Pen(this.daysColor);
            Pen penGrid = new Pen(this.gridColor);
            Pen penPhysical = new Pen(this.physicalGraficColor);
            Pen penEmotional = new Pen(this.emotionalGraficColor);
            Pen penIntelectual = new Pen(this.intelectualGraficColor);
            Pen penIntuitional = new Pen(this.intuitionalGraficColor);

            DateTime today = DateTime.Today;
            DateTime currentDay = this.CurrentDay;
            DateTime day = this.firstDay;
            day = day.AddDays(-1);
            StringFormat Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;

            float step = graficWidth / this.totalDays;
            float decalaj = step / 2;
            float x1 = 0;
            float y1f = 0;
            float y1e = 0;
            float y1i = 0;
            float y1int = 0;
            float x2 = 0;
            float y2f = 0;
            float y2e = 0;
            float y2i = 0;
            float y2int = 0;
            int DaysLived = this.DaysLivedUntilFirstDay;
            int startf = DaysLived % PERIOADA_PHYSICAL;
            int starte = DaysLived % PERIOADA_EMOTIONAL;
            int starti = DaysLived % PERIOADA_INTELECTUAL;
            int startint = DaysLived % PERIOADA_INTUITIONAL;

            x1 = -decalaj;

            // Clear the image before drawing the biorythms
            g.FillRectangle(new SolidBrush(this.backgroundColor), rect);
            //this.ClearImage(g);


            // Iterating for every day for what has to be drawn the biorythms
            for (int i = 0; i < this.totalDays; i++)
            {
                day = day.AddDays(1);

                // Calculating the coordonates
                if (i > 0)
                {
                    x1 = x2;
                    y1f = y2f;
                    y1e = y2e;
                    y1i = y2i;
                    y1int = y2int;
                }

                x2 = decalaj + step * i;
                if (this.showPhysicalGrafic)
                {
                    y2f = -(float)((Math.Sin((startf + i) * 2 * Math.PI / DrawBiorithmsGraphics.PERIOADA_PHYSICAL) * (graficHeight - 20) / 2) - graficHeight / 2);
                }
                if (this.showEmotionalGrafic)
                {
                    y2e = -(float)((Math.Sin((starte + i) * 2 * Math.PI / DrawBiorithmsGraphics.PERIOADA_EMOTIONAL) * (graficHeight - 20) / 2) - graficHeight / 2);
                }
                if (this.showIntelectualGrafic)
                {
                    y2i = -(float)((Math.Sin((starti + i) * 2 * Math.PI / DrawBiorithmsGraphics.PERIOADA_INTELECTUAL) * (graficHeight - 20) / 2) - graficHeight / 2);
                }
                if (this.showIntuitionalGrafic)
                {
                    y2int = -(float)((Math.Sin((startint + i) * 2 * Math.PI / DrawBiorithmsGraphics.PERIOADA_INTUITIONAL) * (graficHeight - 20) / 2) - graficHeight / 2);
                }

                // Changing today's background color
                if (DateTime.Compare(day, today) == 0)
                {
                    g.FillRectangle(new SolidBrush(this.todayBackColor), x1 + decalaj + 1, 0, step, graficHeight);
                }

                if (i == 0)
                {
                    x1 = x2;
                    y1f = y2f;
                    y1e = y2e;
                    y1i = y2i;
                    y1int = y2int;
                }

                // Drawing the biorythms graphics
                if (this.showPhysicalGrafic)
                {
                    g.DrawLine(penPhysical, x1, y1f, x2, y2f);
                }
                if (this.showEmotionalGrafic)
                {
                    g.DrawLine(penEmotional, x1, y1e, x2, y2e);
                }
                if (this.showIntelectualGrafic)
                {
                    g.DrawLine(penIntelectual, x1, y1i, x2, y2i);
                }
                if (this.showIntuitionalGrafic)
                {
                    g.DrawLine(penIntuitional, x1, y1int, x2, y2int);
                }

                // Drawing the day grid
                if (this.showGrid)
                {
                    if (this.showCurrentDay)
                    {
                        if (DateTime.Compare(day, currentDay) == 0 ||
                            DateTime.Compare(day, currentDay.AddDays(-1)) == 0)
                        {
                            penGrid.Width = 2;
                        }
                        else
                        {
                            penGrid.Width = 1;
                        }
                    }
                    g.DrawLine(penGrid, step * (i + 1), 0, step * (i + 1), graficHeight);
                }

                if (this.showDays)
                {
                    g.DrawString(day.Day.ToString(), this.daysFont, new SolidBrush(this.daysColor), new RectangleF(step * i, graficHeight / 2, step, 50), Format);
                }
            }

            // Drawing the zero line
            g.DrawLine(penGrid, 0, graficHeight / 2, graficWidth, graficHeight / 2);

            // Drawing the border
            this.DrawBorder(g, rect);
        }



        /// <summary>
        /// Draws the day grid. Used when there is no biorythm to display.
        /// </summary>
        /// <param name="g"></param>
        private void DrawDays(Graphics g, Rectangle rect)
        {
            int graficWidth = rect.Width;
            int graficHeight = rect.Height;

            Single pas = graficWidth / this.totalDays;
            
            Pen penGrid = new Pen(this.gridColor);
            DateTime currentDay = this.CurrentDay;

            DateTime zi = this.firstDay;
            zi = zi.AddDays(-1);

            StringFormat Format = new StringFormat();
            Format.Alignment = StringAlignment.Center;

            for (int i = 0; i < this.totalDays - 2; i++)
            {
                zi = zi.AddDays(1);

                // Changing today's background color
                if (DateTime.Compare(zi, DateTime.Today) == 0)
                {
                    g.FillRectangle(new SolidBrush(this.todayBackColor), pas * i + 1, 0, pas, graficWidth);
                }

                if (this.showGrid)
                {
                    if (this.showCurrentDay)
                    {
                        if (DateTime.Compare(zi, currentDay) == 0 ||
                            DateTime.Compare(zi, currentDay.AddDays(-1)) == 0)
                        {
                            penGrid.Width = 2;
                        }
                        else
                        {
                            penGrid.Width = 1;
                        }
                    }
                    g.DrawLine(penGrid, pas * (i + 1), 0, pas * (i + 1), graficHeight);
                }

                if (this.showDays)
                {
                    g.DrawString(zi.Day.ToString(), this.daysFont, new SolidBrush(this.daysColor), new RectangleF(pas * i, graficHeight / 2, pas, 50), Format);
                }
            }

            g.DrawLine(penGrid, 0, graficHeight / 2, graficWidth, graficHeight / 2);
        }

        /// <summary>
        /// Draws the border of the grafic
        /// </summary>
        /// <param name="g"></param>
        private void DrawBorder(Graphics g, Rectangle rect)
        {
            int graficWidth = rect.Width;
            int graficHeight = rect.Height;

            if (this.showBorder == true)
            {
                Pen pen = new Pen(this.borderColor);
                g.DrawRectangle(pen, new Rectangle(0, 0, graficWidth - 1, graficHeight - 1));
            }
        }

        public void DrawEmptyGrafic(Graphics g, Rectangle rect)
        {
            // Clear the image
            g.FillRectangle(new SolidBrush(this.backgroundColor), rect);
            
            this.DrawDays(g, rect);
            this.DrawBorder(g, rect);
        }
    }
}
