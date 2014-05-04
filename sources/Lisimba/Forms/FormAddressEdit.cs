using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormAddressEdit : FormEditBase
    {
        private Address address = null;
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;

                textBoxAddress.Text = value.Street;
                textBoxCity.Text = value.City;
                textBoxZip.Text = value.PostalCode;
                textBoxState.Text = value.State;
                textBoxCountry.Text = value.Country;
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
            if (!address.Street.Equals(textBoxAddress.Text) ||
                !address.City.Equals(textBoxCity.Text) ||
                !address.PostalCode.Equals(textBoxZip.Text) ||
                !address.State.Equals(textBoxState.Text) ||
                !address.Country.Equals(textBoxCountry.Text))
            {
                address.Street = textBoxAddress.Text;
                address.City = textBoxCity.Text;
                address.PostalCode = textBoxZip.Text;
                address.State = textBoxState.Text;
                address.Country = textBoxCountry.Text;

                OnAddressUpdated(new AddressUpdatedEventArgs(address));
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
                AcceptChanges();
            }
        }
    }
}