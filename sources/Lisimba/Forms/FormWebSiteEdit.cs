using System;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormWebSiteEdit : FormEditBase
    {
        private WebSite webSite = null;
        public WebSite WebSite
        {
            get { return webSite; }
            set
            {
                webSite = value;

                textBoxAddress.Text = value.Address;
                textBoxComments.Text = value.Description;
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
                get { return webSite; }
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

            textBoxAddress.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
            textBoxComments.KeyDown += new KeyEventHandler(FormEditBase_KeyDown);
        }

        protected override void UpdateData()
        {
            if (!webSite.Address.Equals(textBoxAddress.Text) ||
                !webSite.Description.Equals(textBoxComments.Text))
            {
                webSite.Address = textBoxAddress.Text;
                webSite.Description = textBoxComments.Text;

                OnWebSiteUpdated(new WebSiteUpdatedEventArgs(webSite));
            }
        }
    }
}
