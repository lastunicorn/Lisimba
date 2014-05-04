using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DustInTheWind.Lisimba.Egg
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
            get { return this.day; }
            set
            {
                if (value < 0)
                    this.day = 0;
                else
                {
                    if (this.month > 0)
                    {
                        int maxDay = this.year == 0 ? 31 : DateTime.DaysInMonth(this.year, this.month);
                        if (Day > maxDay)
                            this.day = maxDay;
                        else
                            this.day = value;
                    }
                    else
                    {
                        this.day = value;
                    }
                }
            }
        }

        //[XmlElement("Month", typeof(int))]
        [XmlAttribute("Month")]
        public int Month
        {
            get { return this.month; }
            set
            {
                if (value < 0)
                    this.month = 0;
                else if (value > 12)
                    this.month = 12;
                else
                    this.month = value;

                this.Day = this.day;
            }
        }

        //[XmlElement("Year", typeof(int))]
        [XmlAttribute("Year")]
        public int Year
        {
            get { return this.year; }
            set
            {
                if (value <= 0)
                    this.year = 0;
                else
                    this.year = value;

                this.Day = this.day;
            }
        }

        // Description
        [XmlAttribute("Description")]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public bool IsCompleteDate
        {
            get { return this.day > 0 && this.month > 0 && this.year > 0; }
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
            this.Month = month;

            // Year
            this.Year = year;

            // Day
            this.Day = day;
        }

        public Date(Date date)
        {
            this.CopyFrom(date);
        }

        #endregion

        #region public void SetValues(int day, int month, int year)

        public void SetValues(int day, int month, int year)
        {
            // Month
            this.Month = month;

            // Year
            this.Year = year;

            // Day
            this.Day = day;
        }

        #endregion

        #region public void SetValues(int day, int month, int year, string description)

        public void SetValues(int day, int month, int year, string description)
        {
            // Description
            this.description = description;

            // Month
            this.Month = month;

            // Year
            this.Year = year;

            // Day
            this.Day = day;
        }

        #endregion

        #region public void Clear()

        public void Clear()
        {
            this.day = 0;
            this.month = 0;
            this.year = 0;
            this.description = string.Empty;
        }

        #endregion

        #region public bool IsNull()

        public bool IsNull()
        {
            if (this.Year == 0 &&
                this.Month == 0 &&
                this.Day == 0)
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
            this.day = d.day;
            this.month = d.month;
            this.year = d.year;
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
                this.Day = int.Parse(v[1]);
                this.Month = int.Parse(v[0]);
                this.Year = int.Parse(v[2]);
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
            return (this.year == 0 ? "0000" : this.year.ToString()) + " " + (this.month < 10 ? "0" : "") + this.month + " " + (this.day < 10 ? "0" : "") + this.day;
        }

        public DateTime ToDateTime()
        {
            if (this.IsCompleteDate)
            {
                return new DateTime(this.year, this.month, this.day);
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
