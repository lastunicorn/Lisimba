using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba
{
    public partial class FormAddressEdit : FormEditBase
    {
        private Address address = null;
        public Address Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;

                this.textBoxAddress.Text = value.Street;
                this.textBoxCity.Text = value.City;
                this.textBoxZip.Text = value.PostalCode;
                this.textBoxState.Text = value.State;
                this.textBoxCountry.Text = value.Country;
            }
        }

        public FormAddressEdit()
        {
            InitializeComponent();
        }

        #region Event AddressUpdated

        /// <summary>
        /// Event raised when ... Well, is raised when it should be raised. Ok?
        /// </summary>
        public event AddressUpdatedHandler AddressUpdated;

        /// <summary>
        /// Represents the method that will handle the AddressUpdated event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object providing data about the event.</param>
        public delegate void AddressUpdatedHandler(object sender, AddressUpdatedEventArgs e);

        /// <summary>
        /// Provides data for AddressUpdated event.
        /// </summary>
        public class AddressUpdatedEventArgs : EventArgs
        {
            private Address address;
            public Address Address
            {
                get { return address; }
            }

            public AddressUpdatedEventArgs(Address address)
            {
                this.address = address;
            }
        }

        /// <summary>
        /// Raises the AddressUpdated event.
        /// </summary>
        /// <param name="e">An AddressUpdatedEventArgs that contains the event data.</param>
        protected virtual void OnAddressUpdated(AddressUpdatedEventArgs e)
        {
            if (AddressUpdated != null)
            {
                AddressUpdated(this, e);
            }
        }

        #endregion

        protected override void UpdateData()
        {
            if (!this.address.Street.Equals(this.textBoxAddress.Text) ||
                !this.address.City.Equals(this.textBoxCity.Text) ||
                !this.address.PostalCode.Equals(this.textBoxZip.Text) ||
                !this.address.State.Equals(this.textBoxState.Text) ||
                !this.address.Country.Equals(this.textBoxCountry.Text))
            {
                this.address.Street = this.textBoxAddress.Text;
                this.address.City = this.textBoxCity.Text;
                this.address.PostalCode = this.textBoxZip.Text;
                this.address.State = this.textBoxState.Text;
                this.address.Country = this.textBoxCountry.Text;

                this.OnAddressUpdated(new AddressUpdatedEventArgs(this.address));
            }
        }

        //private void FormAddressEdit_Shown(object sender, EventArgs e)
        //{
        //    // Correct the position on the screen.

        //    int margin = 10;

        //    // the screen
        //    Rectangle screen = Screen.PrimaryScreen.WorkingArea;
        //    screen.Width -= this.Width + margin;
        //    screen.Height -= this.Height + margin;

        //    // new position
        //    Point p = this.Location;
        //    int x = Math.Min(screen.Width, p.X);
        //    x = Math.Max(margin, x);
        //    int y = Math.Min(screen.Height, p.Y);
        //    y = Math.Max(margin, y);

        //    this.Location = new Point(x, y);
        //}

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.AcceptChanges();
            }
        }
    }
}