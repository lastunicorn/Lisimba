// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using Lisimba.ZodiacSigns.Properties;

namespace Lisimba.ZodiacSigns
{
    public class ZodiacSignProvider : IZodiacSignProvider
    {
        public Image GetZodiacImage(ZodiacSign zodiacSign)
        {
            switch (zodiacSign)
            {
                case ZodiacSign.Aquarius:
                    return Resources.Aquarius;

                case ZodiacSign.Pisces:
                    return Resources.Pisces;

                case ZodiacSign.Aries:
                    return Resources.Aries;

                case ZodiacSign.Taurus:
                    return Resources.Taurus;

                case ZodiacSign.Gemini:
                    return Resources.Gemini;

                case ZodiacSign.Cancer:
                    return Resources.Cancer;

                case ZodiacSign.Leo:
                    return Resources.Leo;

                case ZodiacSign.Virgo:
                    return Resources.Virgo;

                case ZodiacSign.Libra:
                    return Resources.Libra;

                case ZodiacSign.Scorpio:
                    return Resources.Scorpio;

                case ZodiacSign.Sagittarius:
                    return Resources.Sagittarius;

                case ZodiacSign.Capricorn:
                    return Resources.Capricorn;

                default:
                    return null;
            }
        }
    }
}