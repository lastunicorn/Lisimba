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

namespace DustInTheWind.Lisimba.Business.AddressBookModel
{
    public class Date : ContactItem, IEquatable<Date>, IComparable, IComparable<Date>
    {
        private int day;
        private int month;
        private int year;

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

        public bool IsCompleteDate
        {
            get { return day > 0 && month > 0 && year > 0; }
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

        public Date(int day, int month, int year)
            : this(day, month, year, string.Empty)
        {
        }

        public Date(int day, int month, int year, string description)
        {
            SetMonthInternal(month);
            SetYearInternal(year);
            SetDayInternal(day);

            this.description = description;
        }

        public Date(Date date)
        {
            CopyFrom(date);
        }

        public void SetValues(int newDay, int newMonth, int newYear)
        {
            SetDayInternal(newDay);
            SetMonthInternal(newMonth);
            SetYearInternal(newYear);

            OnChanged();
        }

        public void SetValues(int newDay, int newMonth, int newYear, string newDescription)
        {
            SetDayInternal(newDay);
            SetMonthInternal(newMonth);
            SetYearInternal(newYear);

            description = newDescription;

            OnChanged();
        }

        public void Clear()
        {
            day = 0;
            month = 0;
            year = 0;

            description = string.Empty;

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

        public bool IsNull()
        {
            return Year == 0 && Month == 0 && Day == 0;
        }

        #region Compare - static

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

        #endregion

        #region Compare Without Year - static

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

        public static int CompareWithoutYear(Date d1, DateTime d2)
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

        public static int CompareWithoutYear(DateTime d1, Date d2)
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

        #endregion

        #region Compare Operators

        public static bool operator ==(Date d1, DateTime d2)
        {
            return Compare(d1, d2) == 0;
        }

        public static bool operator !=(Date d1, DateTime d2)
        {
            return Compare(d1, d2) != 0;
        }

        public static bool operator <(Date d1, DateTime d2)
        {
            return Compare(d1, d2) < 0;
        }

        public static bool operator >(Date d1, DateTime d2)
        {
            return Compare(d1, d2) > 0;
        }

        public static bool operator <=(Date d1, DateTime d2)
        {
            return Compare(d1, d2) <= 0;
        }

        public static bool operator >=(Date d1, DateTime d2)
        {
            return Compare(d1, d2) >= 0;
        }

        public static bool operator ==(DateTime d1, Date d2)
        {
            return Compare(d1, d2) == 0;
        }

        public static bool operator !=(DateTime d1, Date d2)
        {
            return Compare(d1, d2) != 0;
        }

        public static bool operator <(DateTime d1, Date d2)
        {
            return Compare(d1, d2) < 0;
        }

        public static bool operator >(DateTime d1, Date d2)
        {
            return Compare(d1, d2) > 0;
        }

        public static bool operator <=(DateTime d1, Date d2)
        {
            return Compare(d1, d2) <= 0;
        }

        public static bool operator >=(DateTime d1, Date d2)
        {
            return Compare(d1, d2) >= 0;
        }

        #endregion

        public int CompareTo(object obj)
        {
            return CompareTo(obj as Date);
        }

        public int CompareTo(Date d)
        {
            return Compare(this, d);
        }

        public int CompareTo(DateTime d)
        {
            return Compare(this, d);
        }

        public int CompareToWithoutYear(Date d)
        {
            return CompareWithoutYear(this, d);
        }

        public int CompareToWithoutYear(DateTime d)
        {
            return CompareWithoutYear(this, d);
        }

        public override void CopyFrom(ContactItem contactItem)
        {
            if (contactItem == null) throw new ArgumentNullException("contactItem");

            Date date = contactItem as Date;

            if (date != null)
                CopyFrom(date);
        }

        public void CopyFrom(Date date)
        {
            day = date.day;
            month = date.month;
            year = date.year;
            description = date.description;

            OnChanged();
        }

        public override ContactItem Clone()
        {
            return new Date(day, month, year, description);
        }

        public static Date Parse(string value)
        {
            Date d = new Date();
            d.FromString(value);
            return d;
        }

        public void FromString(string value)
        {
            string[] parts = value.Split('/');

            try
            {
                Day = int.Parse(parts[1]);
                Month = int.Parse(parts[0]);
                Year = int.Parse(parts[2]);
            }
            catch
            {
            }
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
                int hashCode = day;
                hashCode = (hashCode * 397) ^ month;
                hashCode = (hashCode * 397) ^ year;
                hashCode = (hashCode * 397) ^ (description != null ? description.GetHashCode() : 0);

                return hashCode;
            }
        }

        public static bool Equals(Date date1, Date date2)
        {
            if (date1 == null)
                return date2 == null;

            return date1.Equals(date2);
        }
    }
}