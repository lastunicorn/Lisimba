using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormDateEdit : FormEditBase
    {
        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
        }

        private Date date = null;
        public Date Date
        {
            get { return date; }
            set
            {
                date = value;

                comboBoxDay.SelectedIndex = value.Day;
                comboBoxMonth.SelectedIndex = value.Month;
                textBoxYear.Text = (value.Year != 0 ? value.Year.ToString() : string.Empty);

                isModified = false;
            }
        }

        public FormDateEdit()
        {
            InitializeComponent();

            comboBoxDay.Items.Add("-");
            for (int i = 1; i < 32; i++)
                comboBoxDay.Items.Add(i);

            comboBoxMonth.Items.Add("-");
            //this.comboBoxMonth.Items.AddRange(this.GetMonthList());
            comboBoxMonth.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames);

            comboBoxDay.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
            comboBoxMonth.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
            textBoxYear.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
        }

        //private string[] GetMonthList()
        //{
        //    return new string[12]
        //        {
        //            "January",
        //            "February",
        //            "March",
        //            "Apryl",
        //            "May",
        //            "June",
        //            "July",
        //            "August",
        //            "September",
        //            "October",
        //            "November",
        //            "December"
        //        };
        //}

        #region Event DateUpdated

        public event DateUpdatedHandler DateUpdated;
        public delegate void DateUpdatedHandler(object sender, DateUpdatedEventArgs e);

        public class DateUpdatedEventArgs : EventArgs
        {
            private Date date;
            public Date Date
            {
                get { return date; }
            }

            public DateUpdatedEventArgs(Date date)
            {
                this.date = date;
            }
        }

        protected virtual void OnDateUpdated(DateUpdatedEventArgs e)
        {
            if (DateUpdated != null)
            {
                DateUpdated(this, e);
            }
        }

        #endregion

        protected override void UpdateData()
        {
            int day = 0;
            int month = 0;
            int year = 0;

            day = comboBoxDay.SelectedIndex;
            month = comboBoxMonth.SelectedIndex;
            int.TryParse(textBoxYear.Text, out year);

            if (date.Day != day ||
                date.Month != month ||
                date.Year != year)
            {
                date.SetValues(day, month, year);
                isModified = true;

                OnDateUpdated(new DateUpdatedEventArgs(date));
            }
        }
    }
}