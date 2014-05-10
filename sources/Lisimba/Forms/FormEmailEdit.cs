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
    public partial class FormEmailEdit : FormEditBase
    {
        private Email email = null;
        public Email Email
        {
            get { return email; }
            set
            {
                email = value;

                textBoxEmail.Text = value.Address;
                textBoxComments.Text = value.Description;
            }
        }

        #region Event EmailUpdated

        /// <summary>
        /// Event raised when ... Well, is raised when it should be raised. Ok?
        /// </summary>
        public event EmailUpdatedHandler EmailUpdated;

        /// <summary>
        /// Represents the method that will handle the EmailUpdated event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object providing data about the event.</param>
        public delegate void EmailUpdatedHandler(object sender, EmailUpdatedEventArgs e);

        /// <summary>
        /// Provides data for EmailUpdated event.
        /// </summary>
        public class EmailUpdatedEventArgs : EventArgs
        {
            private Email email;
            public Email Email
            {
                get { return email; }
            }

            public EmailUpdatedEventArgs(Email email)
            {
                this.email = email;
            }
        }

        /// <summary>
        /// Raises the EmailUpdated event.
        /// </summary>
        /// <param name="e">An EmailUpdatedEventArgs that contains the event data.</param>
        protected virtual void OnEmailUpdated(EmailUpdatedEventArgs e)
        {
            if (EmailUpdated != null)
            {
                EmailUpdated(this, e);
            }
        }

        #endregion

        public FormEmailEdit()
        {
            InitializeComponent();

            textBoxEmail.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
            textBoxComments.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!email.Address.Equals(textBoxEmail.Text) ||
                !email.Description.Equals(textBoxComments.Text))
            {
                email.Address = textBoxEmail.Text;
                email.Description = textBoxComments.Text;

                OnEmailUpdated(new EmailUpdatedEventArgs(email));
            }
        }
    }
}
