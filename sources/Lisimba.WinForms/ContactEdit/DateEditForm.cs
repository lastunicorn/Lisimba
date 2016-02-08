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

using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class DateEditForm : EditBaseForm
    {
        private Date date;
        private bool addMode;

        public Date Date
        {
            get { return date; }
            set
            {
                date = value;
                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? Resources.AddDate_WindowTitle : Resources.EditDate_WindowTitle;
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public DateEditForm()
        {
            InitializeComponent();

            comboBoxDay.Items.Add("-");

            for (int i = 1; i < 32; i++)
                comboBoxDay.Items.Add(i);

            comboBoxMonth.Items.Add("-");
            comboBoxMonth.Items.AddRange(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames);

            comboBoxDay.KeyDown += HandleFormKeyDown;
            comboBoxMonth.KeyDown += HandleFormKeyDown;
            textBoxYear.KeyDown += HandleFormKeyDown;
            textBoxComment.KeyDown += HandleFormKeyDown;
        }

        protected override void UpdateData()
        {
            bool dataWasChanged = UserChangedData();

            if (!dataWasChanged)
                return;

            ReadDataFromView();

            if (AddMode && ContactItems != null)
                ContactItems.Add(date);
        }

        private void ReadDataFromView()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            int.TryParse(textBoxYear.Text, out year);

            date.SetValues(day, month, year);

            date.Description = textBoxComment.Text;
        }

        private bool UserChangedData()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            int.TryParse(textBoxYear.Text, out year);

            return date.Day != day || date.Month != month || date.Year != year || date.Description != textBoxComment.Text;
        }

        private void DisplayDataInView()
        {
            comboBoxDay.SelectedIndex = date.Day;
            comboBoxMonth.SelectedIndex = date.Month;
            textBoxYear.Text = (date.Year != 0 ? date.Year.ToString() : string.Empty);
            textBoxComment.Text = date.Description;
        }
    }
}