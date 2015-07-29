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

using DustInTheWind.Lisimba.Egg.Book;

namespace DustInTheWind.Lisimba.ContactEdit
{
    public partial class WebSiteEditForm : EditBaseForm
    {
        private WebSite webSite;
        private bool addMode;

        public WebSite WebSite
        {
            get { return webSite; }
            set
            {
                webSite = value;

                DisplayDataInView();
            }
        }

        public bool AddMode
        {
            get { return addMode; }
            set
            {
                addMode = value;

                Text = value ? "Add Web Site" : "Edit Web Site";
            }
        }

        public WebSiteCollection WebSites { get; set; }

        public WebSiteEditForm()
        {
            InitializeComponent();

            AddMode = false;

            textBoxAddress.KeyDown += FormEditBase_KeyDown;
            textBoxComments.KeyDown += FormEditBase_KeyDown;
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = UserChangedData();

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();

            if (AddMode && WebSites != null)
                WebSites.Add(webSite);
        }

        private bool UserChangedData()
        {
            return !webSite.Address.Equals(textBoxAddress.Text) ||
                   !webSite.Description.Equals(textBoxComments.Text);
        }

        private void DisplayDataInView()
        {
            textBoxAddress.Text = webSite.Address;
            textBoxComments.Text = webSite.Description;
        }

        private void ReadDataFromView()
        {
            webSite.Address = textBoxAddress.Text;
            webSite.Description = textBoxComments.Text;
        }
    }
}
