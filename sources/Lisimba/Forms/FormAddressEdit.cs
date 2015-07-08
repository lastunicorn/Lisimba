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

using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Book;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormAddressEdit : FormEditBase
    {
        private Address address;
        public Address Address
        {
            get { return address; }
            set
            {
                address = value;
                DisplayDataInView();
            }
        }

        public FormAddressEdit()
        {
            InitializeComponent();
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = !address.Street.Equals(textBoxAddress.Text) ||
                     !address.City.Equals(textBoxCity.Text) ||
                     !address.PostalCode.Equals(textBoxZip.Text) ||
                     !address.State.Equals(textBoxState.Text) ||
                     !address.Country.Equals(textBoxCountry.Text);

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                AcceptChanges();
        }

        private void DisplayDataInView()
        {
            textBoxAddress.Text = address.Street;
            textBoxCity.Text = address.City;
            textBoxZip.Text = address.PostalCode;
            textBoxState.Text = address.State;
            textBoxCountry.Text = address.Country;
        }

        private void ReadDataFromView()
        {
            address.Street = textBoxAddress.Text;
            address.City = textBoxCity.Text;
            address.PostalCode = textBoxZip.Text;
            address.State = textBoxState.Text;
            address.Country = textBoxCountry.Text;
        }
    }
}