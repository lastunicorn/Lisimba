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

using System;
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.ConsoleCommon.Templating;
using DustInTheWind.Lisimba.Business.AddressBookManagement;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    internal class PrompterTextProvider : IPrompterTextProvider
    {
        private readonly AddressBooks addressBooks;

        public PrompterTextProvider(AddressBooks addressBooks)
        {
            if (addressBooks == null) throw new ArgumentNullException("addressBooks");
            this.addressBooks = addressBooks;
        }

        public ConsoleTemplate BuildTemplate()
        {
            string templateFileName = addressBooks.Current == null
                ? ViewTemplates.GetFullFileName("Prompter.default.t")
                : ViewTemplates.GetFullFileName("Prompter.addressbook.t");

            var parameters = new
            {
                AddressBookName = GetAddressBookName(),
                ModificationMarker = GetModificationMarker()
            };

            return ConsoleTemplate.CreateFromEmbeddedFile(templateFileName, parameters);
        }

        private string GetAddressBookName()
        {
            return addressBooks.Current == null
                ? null
                : addressBooks.Current.AddressBook.Name;
        }

        private string GetModificationMarker()
        {
            bool isModified = addressBooks.Current != null && addressBooks.Current.Status == AddressBookStatus.Modified;
            return isModified ? "*" : string.Empty;
        }
    }
}