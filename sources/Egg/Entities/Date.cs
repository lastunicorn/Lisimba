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
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg.Entities
{
    [Serializable()]
    [XmlRoot("Date")]
    public class Date
    {
        #region Fields

        private int day;
        private int month;
        private int year;
        private string description;

        #endregion

        #region Properties

        //[XmlElement("Day", typeof(int))]
        [XmlAttribute("Day")]
        public int Day
        {
            get { return day; }
            set
            {
                if (value < 0)
                    day = 0;
                else
                {
                    if (month > 0)
                    {
                        int maxDay = year == 0 ? 31 : DateTime.DaysInMonth(year, month);
                        if (Day > maxDay)
                            day = maxDay;
                        else
                            day = value;
                    }
                    else
                    {
                        day = value;
                    }
                }
            }
        }

        //[XmlElement("Month", typeof(int))]
        [XmlAttribute("Month")]
        public int Month
        {
            get { return month; }
            set
            {
                if (value < 0)
                    month = 0;
                else if (value > 12)
                    month = 12;
                else
                    month = value;

                Day = day;
            }
        }

        //[XmlElement("Year", typeof(int))]
        [XmlAttribute("Year")]
        public int Year
        {
            get { return year; }
            set
            {
                if (value <= 0)
                    year = 0;
                else
                    year = value;

                Day = day;
            }
        }

        // Description
        [XmlAttribute("Description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public bool IsCompleteDate
        {
            get { return day > 0 && month > 0 && year > 0; }
        }

        #endregion

        #region Constructors

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

        public Date(int day, int month)
            : this(day, month, 0, string.Empty)
        {
        }

        public Date(int day, int month, string description)
            : this(day, month, 0, description)
        {
        }

        public Date(int day, int month, int year)
            : this(day, month, year, "")
        {
        }

        public Date(int day, int month, int year, string description)
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

        public Date(Date date)
        {
            CopyFrom(date);
        }

        #endregion

        #region public void SetValues(int day, int month, int year)

        public void SetValues(int day, int month, int year)
        {
            // Month
            Month = month;

            // Year
            Year = year;

            // Day
            Day = day;
        }

        #endregion

        #region public void SetValues(int day, int month, int year, string description)

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

        #endregion

        #region public void Clear()

        public void Clear()
        {
            day = 0;
            month = 0;
            year = 0;
            description = string.Empty;
        }

        #endregion

        #region public bool IsNull()

        public bool IsNull()
        {
            if (Year == 0 &&
                Month == 0 &&
                Day == 0)
                return true;
            else
                return false;
        }

        #endregion

        #region public static int Compare(Date d1, Date d2)

        public static int Compare(Date d1, Date d2)
        {
            if (d1.Year < d2.Year)
                return -1;
            else if (d1.Year > d2.Year)
                return 1;
            else
            {
                if (d1.Month < d2.Month)
                    return -1;
                else if (d1.Month > d2.Month)
                    return 1;
                else
                {
                    if (d1.Day < d2.Day)
                        return -1;
                    else if (d1.Day > d2.Day)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        #endregion

        #region public static int Compare(Date d1, DateTime d2)

        public static int Compare(Date d1, DateTime d2)
        {
            if (d1.Year < d2.Year)
                return -1;
            else if (d1.Year > d2.Year)
                return 1;
            else
            {
                if (d1.Month < d2.Month)
                    return -1;
                else if (d1.Month > d2.Month)
                    return 1;
                else
                {
                    if (d1.Day < d2.Day)
                        return -1;
                    else if (d1.Day > d2.Day)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        #endregion

        #region public static int Compare(Date d1, DateTime d2)

        public static int Compare(DateTime d1, Date d2)
        {
            if (d1.Year < d2.Year)
                return -1;
            else if (d1.Year > d2.Year)
                return 1;
            else
            {
                if (d1.Month < d2.Month)
                    return -1;
                else if (d1.Month > d2.Month)
                    return 1;
                else
                {
                    if (d1.Day < d2.Day)
                        return -1;
                    else if (d1.Day > d2.Day)
                        return 1;
                    else
                        return 0;
                }
            }
        }

        #endregion

        #region public int Compare(Date d)

        public int Compare(Date d)
        {
            return Date.Compare(this, d);
        }

        #endregion

        #region public int Compare(DateTime d)

        public int Compare(DateTime d)
        {
            return Date.Compare(this, d);
        }

        #endregion

        #region public static int CompareWithoutYear(Date d1, Date d2)

        public static int CompareWithoutYear(Date d1, Date d2)
        {
            if (d1.Month < d2.Month)
                return -1;
            else if (d1.Month > d2.Month)
                return 1;
            else
            {
                if (d1.Day < d2.Day)
                    return -1;
                else if (d1.Day > d2.Day)
                    return 1;
                else
                    return 0;
            }
        }

        #endregion

        #region public int CompareWithoutYear(Date d)

        public int CompareWithoutYear(Date d)
        {
            return Date.CompareWithoutYear(this, d);
        }

        #endregion

        #region public void CopyFrom(Date d)

        public void CopyFrom(Date d)
        {
            day = d.day;
            month = d.month;
            year = d.year;
        }

        #endregion

        #region public static Date Parse(string value)

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

        #endregion

        #region public void FromString(string value)

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

        #endregion

        #region public static string ShortMonthName(int month)

        public static string ShortMonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedMonthNames[month];

            //switch (month)
            //{
            //    case 1: return "Jan";
            //    case 2: return "Feb";
            //    case 3: return "Mar";
            //    case 4: return "Apr";
            //    case 5: return "May";
            //    case 6: return "Jun";
            //    case 7: return "Jul";
            //    case 8: return "Aug";
            //    case 9: return "Sep";
            //    case 10: return "Oct";
            //    case 11: return "Nov";
            //    case 12: return "Dec";
            //    default: return string.Empty;
            //}
        }

        #endregion

        #region public static string MonthName(int month)

        public static string MonthName(int month)
        {
            return System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[month];
            //switch (month)
            //{
            //    case 1: return "January";
            //    case 2: return "February";
            //    case 3: return "March";
            //    case 4: return "Apryl";
            //    case 5: return "May";
            //    case 6: return "June";
            //    case 7: return "July";
            //    case 8: return "August";
            //    case 9: return "September";
            //    case 10: return "October";
            //    case 11: return "November";
            //    case 12: return "December";
            //    default: return string.Empty;
            //}
        }

        #endregion

        public override string ToString()
        {
            return (year == 0 ? "0000" : year.ToString()) + " " + (month < 10 ? "0" : "") + month + " " + (day < 10 ? "0" : "") + day;
        }

        public DateTime ToDateTime()
        {
            if (IsCompleteDate)
            {
                return new DateTime(year, month, day);
            }
            else
            {
                return new DateTime(0);
            }
        }

        //public string ToLongString()
        //{
        //    return (this.year == 0 ? "0000" : this.year.ToString()) + " " + (this.month < 10 ? "0" : "") + MonthName(this.month) + " " + (this.day < 10 ? "0" : "") + this.day;
        //}

        //public override string ToString(string template)
        //{
        //    string str = template.Replace("yyyy", this.year);
        //    str = str.Replace("mmmm", MonthName(this.month));
        //    str = str.Replace("mmm", 
        //    return (this.year == 0 ? "0000" : this.year.ToString()) + " " + (this.month < 10 ? "0" : "") + this.month + " " + (this.day < 10 ? "0" : "") + this.day;
        //}

        public override bool Equals(object obj)
        {
            if (!(obj is Date)) return false;

            Date date = (Date)obj;

            if (day != date.day) return false;
            if (month != date.month) return false;
            if (year != date.year) return false;
            if (!description.Equals(date.description)) return false;

            return true;
        }
    }
}
