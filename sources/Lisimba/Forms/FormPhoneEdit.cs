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
