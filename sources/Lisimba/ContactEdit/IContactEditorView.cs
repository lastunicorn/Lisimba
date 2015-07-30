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
    public interface IContactEditorView
    {
        void EditBirthday(Date birthday);
        void EditName(PersonName name);
        void AddAddress(PostalAddressCollection postalAddresses);
        void AddDate(DateCollection dates);
        void AddEmail(EmailCollection emails);
        void AddSocialProfileId(SocialProfileIdCollection socialProfileIds);
        void AddPhone(PhoneCollection phones);
        void AddWebSite(WebSiteCollection webSites);
    }
}