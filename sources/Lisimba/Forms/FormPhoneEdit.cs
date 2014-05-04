using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;

namespace DustInTheWind.Lisimba
{
    public partial class FormPhoneEdit : FormEditBase
    {
        private Phone phone = null;
        public Phone Phone
        {
            get { return this.phone; }
            set
            {
                this.phone = value;

                this.textBoxPhone.Text = value.Number;
                this.textBoxComments.Text = value.Description;
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
                get { return this.phone; }
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

            this.textBoxPhone.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
            this.textBoxComments.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!this.phone.Number.Equals(this.textBoxPhone.Text) ||
                !this.phone.Description.Equals(this.textBoxComments.Text))
            {
                this.phone.Number = this.textBoxPhone.Text;
                this.phone.Description = this.textBoxComments.Text;

                this.OnPhoneUpdated(new PhoneUpdatedEventArgs(this.phone));
            }
        }
    }
}
