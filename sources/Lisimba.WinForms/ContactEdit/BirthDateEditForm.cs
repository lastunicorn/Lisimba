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
using DustInTheWind.Lisimba.Egg.AddressBookModel;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class BirthDateEditForm : EditBaseForm
    {
        private Date date;

        public Date Date
        {
            get { return date; }
            set
            {
                date = value;
                DisplayDataInView();
            }
        }

        public DateCollection Dates { get; set; }

        public BirthDateEditForm()
        {
            InitializeComponent();

            comboBoxDay.Items.Add("-");

            for (int i = 1; i < 32; i++)
                comboBoxDay.Items.Add(i);

            comboBoxMonth.Items.Add("-");
            comboBoxMonth.Items.AddRange(CultureInfo.CurrentCulture.DateTimeFormat.MonthNames);

            comboBoxDay.KeyDown += HandleFormKeyDown;
            comboBoxMonth.KeyDown += HandleFormKeyDown;
            textBoxYear.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool dataWasChanged = UserChangedData();

            if (!dataWasChanged)
                return;

            ReadDataFromView();

            if (Dates != null)
                Dates.Add(date);
        }

        private void ReadDataFromView()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            int.TryParse(textBoxYear.Text, out year);

            date.SetValues(day, month, year);
        }

        private bool UserChangedData()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            int.TryParse(textBoxYear.Text, out year);

            return date.Day != day || date.Month != month || date.Year != year;
        }

        private void DisplayDataInView()
        {
            comboBoxDay.SelectedIndex = date.Day;
            comboBoxMonth.SelectedIndex = date.Month;
            textBoxYear.Text = (date.Year != 0 ? date.Year.ToString() : string.Empty);
        }
    }
}