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

using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.WinForms.ContactEditing.Properties;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms
{
    public partial class DateEditForm : EditBaseForm
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

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create
                ? Resources.AddDate_WindowTitle
                : Resources.EditDate_WindowTitle;

            base.OnEditModeChanged();
        }

        private void DisplayDataInView()
        {
            comboBoxDay.SelectedIndex = date.Day;
            comboBoxMonth.SelectedIndex = date.Month;
            textBoxYear.Text = (date.Year != 0 ? date.Year.ToString() : string.Empty);
            textBoxComment.Text = date.Description;
        }

        protected override bool IsDataChanged()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            bool yearIsValid = int.TryParse(textBoxYear.Text, out year);

            if (date == null)
                return day != 0 || month != 0 || yearIsValid || textBoxComment.Text.Length > 0;

            return date.Day != day || date.Month != month || date.Year != year || date.Description != textBoxComment.Text;
        }

        protected override IAction GetCreateAction()
        {
            Date newDate = ReadDateFromView();
            return new CreateContactItemAction(ContactItems, newDate);
        }

        protected override IAction GetUpdateAction()
        {
            Date newDate = ReadDateFromView();
            return new UpdateContactItemAction(date, newDate);
        }

        private Date ReadDateFromView()
        {
            int day = comboBoxDay.SelectedIndex;
            int month = comboBoxMonth.SelectedIndex;

            int year;
            int.TryParse(textBoxYear.Text, out year);

            string description = textBoxComment.Text;

            return new Date(day, month, year, description);
        }
    }
}