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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business
{
    public interface IZodiacSignProvider
    {
        Image GetZodiacImage(ZodiacSign zodiacSign);
    }
}

// ♈ Aries &#9800;
// ♉ Taurus &#9801;
// ♊ Gemini &#9802;
// ♋ Cancer &#9803;
// ♌ Leo &#9804;
// ♍ Virgo &#9805;
// ♎ Libra &#9806;
// ♏ Scorpio &#9807;
// ♐ Sagittarius &#9808;
// ♑ Capricorn &#9809;
// ♒ Aquarius &#9810;
// ♓ Pisces &#9811;