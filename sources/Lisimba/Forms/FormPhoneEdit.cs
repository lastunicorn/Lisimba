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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormPhoneEdit : FormEditBase
    {
        private Phone phone = null;
        public Phone Phone
        {
            get { return phone; }
            set
            {
                phone = value;

                textBoxPhone.Text = value.Number;
                textBoxComments.Text = value.Description;
            }
        }

        #region Event PhoneUpdated

        /// <summary>
        /// Event raised when ... Well, is raised when it should be raised. Ok?
        /// </summary>
        public event PhoneUpdatedHandler PhoneUpdated;

        /// <summary>
        /// Represents the method that will handle the PhoneUpdated event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object providing data about the event.</param>
        public delegate void PhoneUpdatedHandler(object sender, PhoneUpdatedEventArgs e);

        /// <summary>
        /// Provides data for PhoneUpdated event.
        /// </summary>
        public class PhoneUpdatedEventArgs : EventArgs
        {
            private Phone phone = null;
            public Phone Phone
            {
                get { return phone; }
            }

            public PhoneUpdatedEventArgs(Phone phone)
            {
                this.phone = phone;
            }
        }

        /// <summary>
        /// Raises the PhoneUpdated event.
        /// </summary>
        /// <param name="e">An PhoneUpdatedEventArgs that contains the event data.</param>
        protected virtual void OnPhoneUpdated(PhoneUpdatedEventArgs e)
        {
            if (PhoneUpdated != null)
            {
                PhoneUpdated(this, e);
            }
        }

        #endregion

        public FormPhoneEdit()
        {
            InitializeComponent();

            textBoxPhone.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
            textBoxComments.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!phone.Number.Equals(textBoxPhone.Text) ||
                !phone.Description.Equals(textBoxComments.Text))
            {
                phone.Number = textBoxPhone.Text;
                phone.Description = textBoxComments.Text;

                OnPhoneUpdated(new PhoneUpdatedEventArgs(phone));
            }
        }
    }
}
