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
    public partial class FormWebSiteEdit : FormEditBase
    {
        private WebSite webSite = null;
        public WebSite WebSite
        {
            get { return this.webSite; }
            set
            {
                this.webSite = value;

                this.textBoxAddress.Text = value.Address;
                this.textBoxComments.Text = value.Description;
            }
        }

        #region Event WebSiteUpdated

        /// <summary>
        /// Event raised when ... Well, is raised when it should be raised. Ok?
        /// </summary>
        public event WebSiteUpdatedHandler WebSiteUpdated;

        /// <summary>
        /// Represents the method that will handle the WebSiteUpdated event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">Object providing data about the event.</param>
        public delegate void WebSiteUpdatedHandler(object sender, WebSiteUpdatedEventArgs e);

        /// <summary>
        /// Provides data for WebSiteUpdated event.
        /// </summary>
        public class WebSiteUpdatedEventArgs : EventArgs
        {
            private WebSite webSite = null;
            public WebSite WebSite
            {
                get { return this.webSite; }
            }

            public WebSiteUpdatedEventArgs(WebSite webSite)
            {
                this.webSite = webSite;
            }
        }

        /// <summary>
        /// Raises the WebSiteUpdated event.
        /// </summary>
        /// <param name="e">An WebSiteUpdatedEventArgs that contains the event data.</param>
        protected virtual void OnWebSiteUpdated(WebSiteUpdatedEventArgs e)
        {
            if (WebSiteUpdated != null)
            {
                WebSiteUpdated(this, e);
            }
        }

        #endregion

        public FormWebSiteEdit()
        {
            InitializeComponent();

            this.textBoxAddress.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
            this.textBoxComments.KeyDown += new KeyEventHandler(this.FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!this.webSite.Address.Equals(this.textBoxAddress.Text) ||
                !this.webSite.Description.Equals(this.textBoxComments.Text))
            {
                this.webSite.Address = this.textBoxAddress.Text;
                this.webSite.Description = this.textBoxComments.Text;

                this.OnWebSiteUpdated(new WebSiteUpdatedEventArgs(this.webSite));
            }
        }
    }
}
