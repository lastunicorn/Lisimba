using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class ctrl : UserControl
    {
        private Contact value = null;
        public Contact Value
        {
            get { return value; }
            set { this.value = value; this.Invalidate(); }
        }

        //private int padding = 10;
        //public int Padding
        //{
        //    get { return padding; }
        //    set { padding = value; }
        //}

        public ctrl()
        {
            this.Padding = new Padding(10);

            this.InitializeComponent();
        }

        private Rectangle nameRect = Rectangle.Empty;
        private Rectangle phonesRect = Rectangle.Empty;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen basePen = new Pen(this.ForeColor);
            SolidBrush baseBrush = new SolidBrush(this.ForeColor);
            Size size;
            Point location;
            SizeF sizeF;

            string text = string.Empty;
            int lineSpace = this.Font.Height;

            // --------------------------------------------------------------------------------
            // Draw border
            // --------------------------------------------------------------------------------

             g.DrawRectangle(basePen, new Rectangle(new Point(0, 0), new Size(this.Size.Width - 1, this.Size.Height - 1)));
            
            // --------------------------------------------------------------------------------
            // Draw the name
            // --------------------------------------------------------------------------------
            
            if (value != null)
                text = "Name: " + value.Name.ToString();
            else
                text = "Name:";

            location = new Point(this.Padding.Left, this.Padding.Top);
            sizeF = g.MeasureString(text, this.Font);
            size = new Size((int)Math.Ceiling(sizeF.Width), (int)Math.Ceiling(sizeF.Height));

            nameRect = new Rectangle(location, size);

            g.DrawString(text, this.Font, baseBrush, location);

            // --------------------------------------------------------------------------------
            // Draw the phone list
            // --------------------------------------------------------------------------------

            if (value != null && value.Phones != null && value.Phones.Count > 0)
            {
                text = "Phones";
                int phonesIndent = 20;

                location = new Point(this.Padding.Left, nameRect.Top + nameRect.Height + lineSpace);
                sizeF = g.MeasureString(text, this.Font);
                size = g.MeasureString(text, this.Font).ToSize();

                phonesRect = new Rectangle(location, size);

                g.DrawString(text, this.Font, baseBrush, location);

                for (int i = 0; i < this.value.Phones.Count; i++)
                {
                    text = this.value.Phones[i].ToString();

                    location = new Point(phonesRect.Left + phonesIndent, phonesRect.Top + phonesRect.Height + lineSpace);
                    size = g.MeasureString(text, this.Font).ToSize();

                    phonesRect.Height += location.Y + size.Height - phonesRect.Top;

                    g.DrawString(text, this.Font, baseBrush, location);
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
                this.textBoxEdit.Font = this.Font;
                this.textBoxEdit.Location = nameRect.Location;
                this.textBoxEdit.Visible = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.cli
        }
    }
}
