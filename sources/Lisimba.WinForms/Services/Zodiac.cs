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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
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
                    return LocalizedResources.ZodiacSign_Aquarius;

                case ZodiacSign.Pisces:
                    return LocalizedResources.ZodiacSign_Pisces;

                case ZodiacSign.Aries:
                    return LocalizedResources.ZodiacSign_Aries;

                case ZodiacSign.Taurus:
                    return LocalizedResources.ZodiacSign_Taurus;

                case ZodiacSign.Gemini:
                    return LocalizedResources.ZodiacSign_Gemini;

                case ZodiacSign.Cancer:
                    return LocalizedResources.ZodiacSign_Cancer;

                case ZodiacSign.Leo:
                    return LocalizedResources.ZodiacSign_Leo;

                case ZodiacSign.Virgo:
                    return LocalizedResources.ZodiacSign_Virgo;

                case ZodiacSign.Libra:
                    return LocalizedResources.ZodiacSign_Libra;

                case ZodiacSign.Scorpio:
                    return LocalizedResources.ZodiacSign_Scorpio;

                case ZodiacSign.Sagittarius:
                    return LocalizedResources.ZodiacSign_Sagittarius;

                case ZodiacSign.Capricorn:
                    return LocalizedResources.ZodiacSign_Capricorn;

                default:
                    return string.Empty;
            }
        }
    }
}