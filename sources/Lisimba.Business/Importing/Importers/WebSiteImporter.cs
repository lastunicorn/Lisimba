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

using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Business.Importing.Importers
{
    public class WebSiteImporter : ImporterBase<Contact, WebSite>
    {
        protected override string Name
        {
            get { return "Web Site"; }
        }

        protected override void AddAsNew()
        {
            DestinationParent.Items.Add(SourceValue);
        }

        protected override void Merge()
        {
            if (!string.IsNullOrEmpty(SourceValue.Address))
                DestinationValue.Address = SourceValue.Address;

            if (!string.IsNullOrEmpty(SourceValue.Description))
                DestinationValue.Description = SourceValue.Description;
        }

        protected override void Replace()
        {
            DestinationParent.Items.Remove(DestinationValue);
            DestinationParent.Items.Add(SourceValue);
        }
    }
}