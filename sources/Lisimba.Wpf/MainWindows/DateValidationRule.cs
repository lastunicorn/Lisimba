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

using System.Globalization;
using System.Windows.Controls;

namespace DustInTheWind.Lisimba.Wpf.MainWindows
{
    public class DateValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo ultureInfo)
        {
            string dateAsString = value as string;

            if (dateAsString == null)
                return new ValidationResult(false, "The value is not a valid date");

            string[] parts = dateAsString.Split(' ');

            if (parts.Length != 3)
                return new ValidationResult(false, "The value is not a valid date");

            int integer;

            if (!int.TryParse(parts[0], out integer) || !int.TryParse(parts[0], out integer) || !int.TryParse(parts[0], out integer))
                return new ValidationResult(false, "The value is not a valid date");

            return new ValidationResult(true, null);
        }
    }
}