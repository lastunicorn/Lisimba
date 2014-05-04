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
    public partial class FormEmailEdit : FormEditBase
    {
        private Email email = null;
        public Email Email
        {
            get { return this.email; }
            set
            {
                this.email = value;

                this.textBoxEmail.Text = value.Address;
                this.textBoxComments.Text = value.Description;
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
                get { return this.email; }
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

            this.textBoxEmail.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
            this.textBoxComments.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!this.email.Address.Equals(this.textBoxEmail.Text) ||
                !this.email.Description.Equals(this.textBoxComments.Text))
            {
                this.email.Address = this.textBoxEmail.Text;
                this.email.Description = this.textBoxComments.Text;

                this.OnEmailUpdated(new EmailUpdatedEventArgs(this.email));
            }
        }
    }
}
