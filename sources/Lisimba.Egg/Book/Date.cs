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

using System;

namespace DustInTheWind.Lisimba.Egg.Book
{
    public class Date : IObservableEntity, IEquatable<Date>
    {
        private int day;
        private int month;
        private int year;
        private string description;

        public int Day
        {
            get { return day; }
            set
            {
                SetDayInternal(value);
                OnChanged();
            }
        }

        public int Month
        {
            get { return month; }
            set
            {
                SetMonthInternal(value);
                OnChanged();
            }
        }

        public int Year
        {
            get { return year; }
            set
            {
                SetYearInternal(value);
                OnChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnChanged();
            }
        }

        public bool IsCompleteDate
        {
            get { return day > 0 && month > 0 && year > 0; }
        }

        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            EventHandler handler = Changed;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public Date()
            : this(0, 0, 0, string.Empty)
        {
        }

        public Date(DateTime dt)
            : this(dt.Day, dt.Month, dt.Year, string.Empty)
        {
        }

        public Date(DateTime dt, string description)
            : this(dt.Day, dt.Month, dt.Year, description)
        {
        }

        public Date(int day, int month, int year, string description)
        {
            this.description = description;

            Month = month;
            Year = year;
            Day = day;
        }

        public Date(Date date)
        {
            CopyFrom(date);
        }

        public void SetValues(int day, int month, int year)
        {
            this.month = month;
            this.year = year;
            this.day = day;

            OnChanged();
        }

        private void SetDayInternal(int value)
        {
            if (value < 0)
            {
                // todo: throw exception instead

                day = 0;
                return;
            }

            if (month == 0)
            {
                // todo: throw exception if greater then 31

                day = value;
                return;
            }

            int maxDay = year == 0
                ? 31
                : DateTime.DaysInMonth(year, month);

            // todo: throw exception if value is greater then maxDay.
            day = (Day > maxDay) ? maxDay : value;
        }

        private void SetMonthInternal(int value)
        {
            if (value < 0)
            {
                month = 0;
                return;
            }

            // todo: throw exception if day does not permit the month that is about to be set.

            month = (value > 12) ? 12 : value;

            Day = day;
        }

        private void SetYearInternal(int value)
        {
            // todo: throw exception if value is less then 0
            year = (value <= 0) ? 0 : value;

            // todo: throw exception if day does not permit the year that is about to be set.

            Day = day;
        }

        public void SetValues(int day, int month, int year, string description)
        {
            // Description
            this.description = description;

            // Month
            Month = month;

            // Year
            Year = year;

            // Day
            Day = day;
        }

        public void Clear()
        {
            day = 0;
            month = 0;
            year = 0;
            description = string.Empty;
        }

        public bool IsNull()
        {
            return Year == 0 && Month == 0 && Day == 0;
        }

        public static int Compare(Date d1, Date d2)
        {
            if (d1.Year < d2.Year)
                return -1;

            if (d1.Year > d2.Year)
                return 1;

            if (d1.Month < d2.Month)
                return -1;

            if (d1.Month > d2.Month)
                return 1;

            if (d1.Day < d2.Day)
                return -1;

            if (d1.Day > d2.Day)
                return 1;

            return 0;
        }

        public static int Compare(Date d1, DateTime d2)
        {
            if (d1.Year < d2.Year)
                return -1;

            if (d1.Year > d2.Year)
                return 1;

            if (d1.Month < d2.Month)
                return -1;

            if (d1.Month > d2.Month)
                return 1;

            if (d1.Day < d2.Day)
                return -1;

            if (d1.Day > d2.Day)
                return 1;

            return 0;
        }

        public static int Compare(DateTime d1, Date d2)
        {
            if (d1.Year < d2.Year)
                return -1;

            if (d1.Year > d2.Year)
                return 1;

            if (d1.Month < d2.Month)
                return -1;

            if (d1.Month > d2.Month)
                return 1;

            if (d1.Day < d2.Day)
                return -1;

            if (d1.Day > d2.Day)
                return 1;

            return 0;
        }

        public int Compare(Date d)
        {
            return Compare(this, d);
        }

        public int Compare(DateTime d)
        {
            return Compare(this, d);
        }

        public static int CompareWithoutYear(Date d1, Date d2)
        {
            if (d1.Month < d2.Month)
                return -1;

            if (d1.Month > d2.Month)
                return 1;

            if (d1.Day < d2.Day)
                return -1;

            if (d1.Day > d2.Day)
                return 1;

            return 0;
        }

        public int CompareWithoutYear(Date d)
        {
            return CompareWithoutYear(this, d);
        }

        public void CopyFrom(Date d)
        {
            day = d.day;
            month = d.month;
            year = d.year;
        }

        public static Date Parse(string value)
        {
            Date d = new Date();
            string[] v = value.Split('/');

            try
            {
                d.Day = int.Parse(v[1]);
                d.Month = int.Parse(v[0]);
                d.Year = int.Parse(v[2]);
            }
            catch { }

            return d;
        }

        public void FromString(string value)
        {
            string[] v = value.Split('/');
            try
            {
                Day = int.Parse(v[1]);
                Month = int.Parse(v[0]);
                Year = int.Parse(v[2]);
            }
            catch { }
        }

        public static string ShortMonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[month];
        }

        public static string MonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month];
        }

        public override string ToString()
        {
            string yearAsString = year == 0 ? "0000" : year.ToString();
            string monthAsString = (month < 10 ? "0" : "") + month;
            string dayAsString = (day < 10 ? "0" : "") + day;

            return string.Format("{0} {1} {2} - {3}", yearAsString, monthAsString, dayAsString, description);
        }

        public string ToShortString()
        {
            string yearAsString = year == 0 ? "0000" : year.ToString();
            string monthAsString = (month < 10 ? "0" : "") + month;
            string dayAsString = (day < 10 ? "0" : "") + day;

            return string.Format("{0} {1} {2}", yearAsString, monthAsString, dayAsString);
        }

        public DateTime ToDateTime()
        {
            return IsCompleteDate
                ? new DateTime(year, month, day)
                : new DateTime(0);
        }

        public bool Equals(Date other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return day == other.day &&
                month == other.month &&
                year == other.year &&
                string.Equals(description, other.description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Date)) return false;

            return Equals((Date)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = day;
                hashCode = (hashCode * 397) ^ month;
                hashCode = (hashCode * 397) ^ year;
                hashCode = (hashCode * 397) ^ (description != null ? description.GetHashCode() : 0);

                return hashCode;
            }
        }
    }
}
