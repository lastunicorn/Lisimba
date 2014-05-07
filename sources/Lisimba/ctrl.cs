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
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class ctrl : UserControl
    {
        private Contact value = null;
        public Contact Value
        {
            get { return value; }
            set { this.value = value; Invalidate(); }
        }

        //private int padding = 10;
        //public int Padding
        //{
        //    get { return padding; }
        //    set { padding = value; }
        //}

        public ctrl()
        {
            Padding = new Padding(10);

            InitializeComponent();
        }

        private Rectangle nameRect = Rectangle.Empty;
        private Rectangle phonesRect = Rectangle.Empty;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen basePen = new Pen(ForeColor);
            SolidBrush baseBrush = new SolidBrush(ForeColor);
            Size size;
            Point location;
            SizeF sizeF;

            string text = string.Empty;
            int lineSpace = Font.Height;

            // --------------------------------------------------------------------------------
            // Draw border
            // --------------------------------------------------------------------------------

             g.DrawRectangle(basePen, new Rectangle(new Point(0, 0), new Size(Size.Width - 1, Size.Height - 1)));
            
            // --------------------------------------------------------------------------------
            // Draw the name
            // --------------------------------------------------------------------------------
            
            if (value != null)
                text = "Name: " + value.Name.ToString();
            else
                text = "Name:";

            location = new Point(Padding.Left, Padding.Top);
            sizeF = g.MeasureString(text, Font);
            size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));

            nameRect = new Rectangle(location, size);

            g.DrawString(text, Font, baseBrush, location);

            // --------------------------------------------------------------------------------
            // Draw the phone list
            // --------------------------------------------------------------------------------

            if (value != null && value.Phones != null && value.Phones.Count > 0)
            {
                text = "Phones";
                int phonesIndent = 20;

                location = new Point(Padding.Left, nameRect.Top + nameRect.Height + lineSpace);
                sizeF = g.MeasureString(text, Font);
                size = g.MeasureString(text, Font).ToSize();

                phonesRect = new Rectangle(location, size);

                g.DrawString(text, Font, baseBrush, location);

                for (int i = 0; i < value.Phones.Count; i++)
                {
                    text = value.Phones[i].ToString();

                    location = new Point(phonesRect.Left + phonesIndent, phonesRect.Top + phonesRect.Height + lineSpace);
                    size = g.MeasureString(text, Font).ToSize();

                    phonesRect.Height += location.Y + size.Height - phonesRect.Top;

                    g.DrawString(text, Font, baseBrush, location);
                }
            }


            basePen.Dispose();
            baseBrush.Dispose();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (nameRect.Contains(e.Location))
            {
                textBoxEdit.Font = Font;
                textBoxEdit.Location = nameRect.Location;
                textBoxEdit.Visible = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.cli
        }
    }
}
