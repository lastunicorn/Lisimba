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

using System.Drawing;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.Services;

namespace DustInTheWind.Lisimba.WinForms.ContactEdit
{
    public partial class ZodiacSignView : UserControl
    {
        private readonly Zodiac zodiac;
        private ZodiacSign zodiacSign;

        public ZodiacSign ZodiacSign
        {
            get { return zodiacSign; }
            set
            {
                zodiacSign = value;

                UpdateDisplayedZodiacSign();
            }
        }

        public ZodiacSignView()
        {
            InitializeComponent();

            zodiac = new Zodiac();
        }

        private void UpdateDisplayedZodiacSign()
        {
            Image zodiacImage = zodiac.GetZodiacImage(ZodiacSign);
            string zodiacSignName = zodiac.GetZodiacSignName(ZodiacSign);

            pictureBoxZodiacSign.Image = zodiacImage;
            pictureBoxZodiacSign.Text = zodiacSignName;
            labelZodiacSign.Text = zodiacSignName;
        }
    }
}
