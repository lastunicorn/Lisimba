// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
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

using DustInTheWind.Lisimba.Business.ActionManagement;
using DustInTheWind.Lisimba.Business.Actions;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.WinForms.ContactEditing.ContactItemEditForms
{
    public partial class WebSiteEditForm : EditBaseForm
    {
        private WebSite webSite;

        public WebSite WebSite
        {
            get { return webSite; }
            set
            {
                webSite = value;

                DisplayDataInView();
            }
        }

        public CustomObservableCollection<ContactItem> ContactItems { get; set; }

        public WebSiteEditForm()
        {
            InitializeComponent();

            EditMode = EditMode.Create;

            textBoxAddress.KeyDown += HandleFormKeyDown;
            textBoxComments.KeyDown += HandleFormKeyDown;
        }

        protected override void OnEditModeChanged()
        {
            Text = EditMode == EditMode.Create ? "Add Web Site" : "Edit Web Site";
            base.OnEditModeChanged();
        }

        private void DisplayDataInView()
        {
            textBoxAddress.Text = webSite.Address;
            textBoxComments.Text = webSite.Description;
        }

        protected override bool IsDataChanged()
        {
            if (webSite == null)
                return textBoxAddress.Text.Length > 0 ||
                       textBoxComments.Text.Length > 0;

            return !webSite.Address.Equals(textBoxAddress.Text) ||
                !webSite.Description.Equals(textBoxComments.Text);
        }

        protected override IAction GetCreateAction()
        {
            WebSite newWebSite = ReadWebSiteFromView();
            return new CreateContactItemAction(ContactItems, newWebSite);
        }

        protected override IAction GetUpdateAction()
        {
            WebSite newWebSite = ReadWebSiteFromView();
            return new UpdateContactItemAction(webSite, newWebSite);
        }

        private WebSite ReadWebSiteFromView()
        {
            string newAddress = textBoxAddress.Text;
            string newDescription = textBoxComments.Text;

            return new WebSite(newAddress, newDescription);
        }
    }
}