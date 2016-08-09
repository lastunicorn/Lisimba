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
using System.Windows.Media.Imaging;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class ZodiacSignViewModel : ViewModelBase
    {
        private readonly Zodiac zodiac;
        private ZodiacSign zodiacSign;
        private BitmapSource image;
        private string text;

        public ZodiacSign ZodiacSign
        {
            get { return zodiacSign; }
            set
            {
                zodiacSign = value;

                UpdateDisplayedZodiacSign();
            }
        }

        public BitmapSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }

        public ZodiacSignViewModel(Zodiac zodiac)
        {
            if (zodiac == null) throw new ArgumentNullException("zodiac");
            this.zodiac = zodiac;
        }

        private void UpdateDisplayedZodiacSign()
        {
            Image zodiacImage = zodiac.GetZodiacImage(zodiacSign);
            Image = zodiacImage.ToBitmapSource();

            Text = zodiac.GetZodiacSignName(zodiacSign);
        }
    }
}
