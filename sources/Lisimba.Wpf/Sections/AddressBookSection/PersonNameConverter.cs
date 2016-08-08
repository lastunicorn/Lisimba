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
using System.Globalization;
using System.Windows.Data;
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection
{
    internal class PersonNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            PersonName personName = value as PersonName;

            if (personName == null)
                return null;

            if (targetType == typeof(string))
                return personName.ToString();

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string personNameAsString = value as string;

            if (personNameAsString == null)
                return null;

            if (targetType == typeof (PersonName))
            {
                NameParser nameParser = new NameParser(personNameAsString);

                if (!nameParser.Success)
                    return false;

                return nameParser.Result;
            }

            return null;
        }
    }
}
