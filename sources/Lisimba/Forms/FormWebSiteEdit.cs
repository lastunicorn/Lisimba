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

using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public partial class FormWebSiteEdit : FormEditBase
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

        public FormWebSiteEdit()
        {
            InitializeComponent();

            textBoxAddress.KeyDown += FormEditBase_KeyDown;
            textBoxComments.KeyDown += FormEditBase_KeyDown;
        }

        protected override void UpdateData()
        {
            bool isAnyDataChanged = !webSite.Address.Equals(textBoxAddress.Text) ||
                                    !webSite.Description.Equals(textBoxComments.Text);

            if (!isAnyDataChanged)
                return;

            ReadDataFromView();
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
