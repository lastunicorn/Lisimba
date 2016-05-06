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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Input;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class ImageClickCommand : ICommand
    {
        private readonly WindowSystem windowSystem;
        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set
            {
                contact = value;
                OnCanExecuteChanged();
            }
        }

        public event EventHandler CanExecuteChanged;

        public ImageClickCommand(WindowSystem windowSystem)
        {
            if (windowSystem == null) throw new ArgumentNullException("windowSystem");

            this.windowSystem = windowSystem;
        }

        public bool CanExecute(object parameter)
        {
            return Contact != null;
        }

        public void Execute(object parameter)
        {
            try
            {
                string fileName = windowSystem.AskToOpen(string.Empty, "Image Files|*.bmp;*.gif;*.jpg;*.jpeg;*.png|All Files|*.*");

                if (fileName != null)
                {
                    Image image = Image.FromFile(fileName);
                    image = ResizeImage(image, 128, 128);
                    Contact.Picture = image;
                }
            }
            catch (Exception ex)
            {
                windowSystem.DisplayError(ex.Message);
            }
        }

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        private static Image ResizeImage(Image image, int width, int height)
        {
            int sourceWidth = image.Width;
            int sourceHeight = image.Height;

            Rectangle sourceRectangle = new Rectangle(0, 0, sourceWidth, sourceHeight);
            Rectangle destRect = CalculateDestinationRectangle(sourceWidth, sourceHeight, width, height);

            Bitmap resultedImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            resultedImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics g = Graphics.FromImage(resultedImage))
            {
                g.Clear(Color.Transparent);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(image, destRect, sourceRectangle, GraphicsUnit.Pixel);
            }

            return resultedImage;
        }

        private static Rectangle CalculateDestinationRectangle(int sourceWidth, int sourceHeight, int desiredWidth, int desiredHeight)
        {
            int destX = 0;
            int destY = 0;
            float nPercentW = ((float)desiredWidth / (float)sourceWidth);
            float nPercentH = ((float)desiredHeight / (float)sourceHeight);

            float nPercent;

            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = Convert.ToInt16((desiredWidth - (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = Convert.ToInt16((desiredHeight - (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            return new Rectangle(destX, destY, destWidth, destHeight);
        }
    }
}