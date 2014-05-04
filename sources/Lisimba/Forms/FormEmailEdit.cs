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
