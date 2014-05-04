using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class FormDateEdit : FormEditBase
    {
        private bool isModified = false;
        public bool IsModified
        {
            get { return this.isModified; }
        }

        private Date date = null;
        public Date Date
        {
            get { return this.date; }
            set
            {
                this.date = value;

                this.comboBoxDay.SelectedIndex = value.Day;
                this.comboBoxMonth.SelectedIndex = value.Month;
                this.textBoxYear.Text = (value.Year != 0 ? value.Year.ToString() : string.Empty);

                this.isModified = false;
            }
        }

        public FormDateEdit()
        {
            InitializeComponent();

            this.comboBoxDay.Items.Add("-");
            for (int i = 1; i < 32; i++)
                this.comboBoxDay.Items.Add(i);

            this.comboBoxMonth.Items.Add("-");
            //this.comboBoxMonth.Items.AddRange(this.GetMonthList());
            this.comboBoxMonth.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames);

            this.comboBoxDay.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
            this.comboBoxMonth.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
            this.textBoxYear.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
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

            day = this.comboBoxDay.SelectedIndex;
            month = this.comboBoxMonth.SelectedIndex;
            int.TryParse(this.textBoxYear.Text, out year);

            if (this.date.Day != day ||
                this.date.Month != month ||
                this.date.Year != year)
            {
                this.date.SetValues(day, month, year);
                this.isModified = true;

                this.OnDateUpdated(new DateUpdatedEventArgs(this.date));
            }
        }
    }
}