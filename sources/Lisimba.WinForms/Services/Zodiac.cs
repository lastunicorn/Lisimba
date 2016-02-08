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
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.ZodiacSigns;

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
                    return ZodiacResources.ZodiacSign_Aquarius;

                case ZodiacSign.Pisces:
                    return ZodiacResources.ZodiacSign_Pisces;

                case ZodiacSign.Aries:
                    return ZodiacResources.ZodiacSign_Aries;

                case ZodiacSign.Taurus:
                    return ZodiacResources.ZodiacSign_Taurus;

                case ZodiacSign.Gemini:
                    return ZodiacResources.ZodiacSign_Gemini;

                case ZodiacSign.Cancer:
                    return ZodiacResources.ZodiacSign_Cancer;

                case ZodiacSign.Leo:
                    return ZodiacResources.ZodiacSign_Leo;

                case ZodiacSign.Virgo:
                    return ZodiacResources.ZodiacSign_Virgo;

                case ZodiacSign.Libra:
                    return ZodiacResources.ZodiacSign_Libra;

                case ZodiacSign.Scorpio:
                    return ZodiacResources.ZodiacSign_Scorpio;

                case ZodiacSign.Sagittarius:
                    return ZodiacResources.ZodiacSign_Sagittarius;

                case ZodiacSign.Capricorn:
                    return ZodiacResources.ZodiacSign_Capricorn;

                case ZodiacSign.NotSpecified:
                    return string.Empty;

                default:
                    throw new LisimbaException("Invalid Zodiac Sign.");
            }
        }
    }
}