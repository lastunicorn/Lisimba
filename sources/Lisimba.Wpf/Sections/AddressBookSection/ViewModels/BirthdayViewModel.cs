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
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Wpf.Sections.AddressBookSection.ViewModels
{
    internal class BirthdayViewModel : ViewModelBase
    {
        private Date date;

        private int? year;
        private int month;
        private int day;
        private string description;

        public Date Date
        {
            get { return date; }
            set
            {
                if (date != null)
                    date.Changed -= HandleDateChanged;

                date = value;

                if (date != null)
                {
                    year = date.Year;
                    month = date.Month;
                    day = date.Day;

                    date.Changed += HandleDateChanged;
                }
            }
        }

        public int? Year
        {
            get { return year; }
            set
            {
                if (year == value)
                    return;

                year = value;

                if (value != null)
                    date.Year = value.Value;

                OnPropertyChanged();
            }
        }

        public List<string> Months { get; private set; }

        public int Month
        {
            get { return month; }
            set
            {
                if (month == value)
                    return;

                month = value;
                date.Month = value;

                OnPropertyChanged();
            }
        }

        public List<string> Days { get; private set; }

        public int Day
        {
            get { return day; }
            set
            {
                if (day == value)
                    return;

                day = value;
                date.Day = value;

                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (description == value)
                    return;

                description = value;
                date.Description = value;

                OnPropertyChanged();
            }
        }

        public BirthdayViewModel()
        {
            Months = CreateMonths();
            Days = CreateDays();

        }

        private static List<string> CreateMonths()
        {
            List<string> values = new List<string> { "-- Month --" };

            IEnumerable<string> monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames
                .Where(x => x != string.Empty);

            values.AddRange(monthNames);

            return values;
        }

        private static List<string> CreateDays()
        {
            List<string> values = new List<string> { "-- Day --" };

            for (int i = 1; i < 32; i++)
                values.Add(i.ToString());

            return values;
        }

        private void HandleDateChanged(object sender, EventArgs eventArgs)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
            Description = date.Description;
        }
    }
}
