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

using System.Drawing;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Services
{
    public class Zodiac
    {
        public Image GetZodiacImage(ZodiacSign zodiacSign)
        {
            Image img = null;

            switch (zodiacSign)
            {
                case ZodiacSign.Aquarius:
                    img = Resources.Aquarius;
                    break;

                case ZodiacSign.Pisces:
                    img = Resources.Pisces;
                    break;

                case ZodiacSign.Aries:
                    img = Resources.Aries;
                    break;

                case ZodiacSign.Taurus:
                    img = Resources.Taurus;
                    break;

                case ZodiacSign.Gemini:
                    img = Resources.Gemini;
                    break;

                case ZodiacSign.Cancer:
                    img = Resources.Cancer;
                    break;

                case ZodiacSign.Leo:
                    img = Resources.Leo;
                    break;

                case ZodiacSign.Virgo:
                    img = Resources.Virgo;
                    break;

                case ZodiacSign.Libra:
                    img = Resources.Libra;
                    break;

                case ZodiacSign.Scorpio:
                    img = Resources.Scorpio;
                    break;

                case ZodiacSign.Sagittarius:
                    img = Resources.Sagittarius;
                    break;

                case ZodiacSign.Capricorn:
                    img = Resources.Capricorn;
                    break;
            }

            return img;
        }

        public string GetZodiacSignName(ZodiacSign zodiacSign)
        {
            return zodiacSign.ToString();
        }
    }
}
