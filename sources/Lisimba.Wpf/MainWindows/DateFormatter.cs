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

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    internal class DateFormatter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Date date = value as Date;

            if (date == null)
                return null;

            if (targetType == typeof(string))
                return date.ToShortString();

            if (targetType == typeof(object))
                return date.ToShortString();

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string dateAsString = value as string;

            if (dateAsString == null)
                return null;

            if (targetType == typeof(Date))
            {
                string[] parts = dateAsString.Split(' ');

                if (parts.Length != 3)
                    return null;

                try
                {
                    Date date = new Date
                    {
                        Year = int.Parse(parts[0]),
                        Month = int.Parse(parts[1]),
                        Day = int.Parse(parts[2])
                    };

                    return date;
                }
                catch
                {
                    return null;
                }
            }

            return null;
        }
    }
}
