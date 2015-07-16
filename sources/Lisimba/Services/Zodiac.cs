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
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Properties;
using Lisimba.ZodiacSigns;

namespace DustInTheWind.Lisimba.Services
{
    public class Zodiac
    {
        public Image GetZodiacImage(ZodiacSign zodiacSign)
        {
            ZodiacSignProvider zodiacSignProvider = new ZodiacSignProvider();
            return zodiacSignProvider.GetZodiacImage(zodiacSign);
        }

        public Image GetEmptyImage()
        {
            ZodiacSignProvider zodiacSignProvider = new ZodiacSignProvider();
            return zodiacSignProvider.GetZodiacImage(ZodiacSign.Aquarius);
        }

        public string GetZodiacSignName(ZodiacSign zodiacSign)
        {
            switch (zodiacSign)
            {
                case ZodiacSign.Aquarius:
                    return Resources.ZodiacSign_Aquarius;

                case ZodiacSign.Pisces:
                    return Resources.ZodiacSign_Pisces;

                case ZodiacSign.Aries:
                    return Resources.ZodiacSign_Aries;

                case ZodiacSign.Taurus:
                    return Resources.ZodiacSign_Taurus;

                case ZodiacSign.Gemini:
                    return Resources.ZodiacSign_Gemini;

                case ZodiacSign.Cancer:
                    return Resources.ZodiacSign_Cancer;

                case ZodiacSign.Leo:
                    return Resources.ZodiacSign_Leo;

                case ZodiacSign.Virgo:
                    return Resources.ZodiacSign_Virgo;

                case ZodiacSign.Libra:
                    return Resources.ZodiacSign_Libra;

                case ZodiacSign.Scorpio:
                    return Resources.ZodiacSign_Scorpio;

                case ZodiacSign.Sagittarius:
                    return Resources.ZodiacSign_Sagittarius;

                case ZodiacSign.Capricorn:
                    return Resources.ZodiacSign_Capricorn;

                default:
                    return string.Empty;
            }
        }
    }
}
