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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class DateViewModel : ViewModelBase
    {
        private readonly Date date;

        public int Year
        {
            get { return date.Year; }
            set { date.Year = value; }
        }

        public List<string> Months { get; private set; }

        public int Month
        {
            get { return date.Month; }
            set { date.Month = value; }
        }

        public int Day
        {
            get { return date.Day; }
            set { date.Day = value; }
        }

        public string Description
        {
            get { return date.Description; }
            set { date.Description = value; }
        }

        public DateViewModel(Date date)
        {
            if (date == null) throw new ArgumentNullException("date");

            this.date = date;

            Months = CreateMonths();

            date.Changed += HandleDateChanged;
        }

        private static List<string> CreateMonths()
        {
            List<string> values = new List<string> { "-" };

            IEnumerable<string> monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(x => x != string.Empty);

            values.AddRange(monthNames);

            return values;
        }

        private void HandleDateChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("Year");
            OnPropertyChanged("Month");
            OnPropertyChanged("Day");
            OnPropertyChanged("Description");
        }
    }
}
