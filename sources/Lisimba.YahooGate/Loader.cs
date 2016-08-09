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

using System.IO;
using CsvHelper;
using DustInTheWind.Lisimba.Business.AddressBookModel;

namespace DustInTheWind.Lisimba.Gating
{
    public class Loader
    {
        public AddressBook Load(Stream stream)
        {
            AddressBook addressBook = new AddressBook
            {
                Version = string.Empty,
                Name = string.Empty
            };

            using (StreamReader streamReader = new StreamReader(stream))
            {
                using (CsvReader csvReader = new CsvReader(streamReader))
                {
                    csvReader.Configuration.HasHeaderRecord = true;
                    csvReader.Configuration.QuoteAllFields = true;
                    csvReader.Configuration.IgnoreBlankLines = true;
                    csvReader.Configuration.Quote = '"';
                    csvReader.Configuration.Delimiter = ",";

                    while (csvReader.Read())
                    {
                        Contact contact = FromCsvRecord(csvReader);
                        addressBook.Contacts.Add(contact);
                    }
                }
            }

            return addressBook;
        }

        private Contact FromCsvRecord(ICsvReaderRow csvRecord)
        {
            Contact contact = new Contact();

            // First
            contact.Name.FirstName = csvRecord[0];

            // Middle
            contact.Name.MiddleName = csvRecord[1];

            // Last
            contact.Name.LastName = csvRecord[2];

            // Nickname
            contact.Name.Nickname = csvRecord[3];


            // Email
            if (csvRecord[4].Length > 0) contact.Items.Add(new Email(csvRecord[4], "Email"));


            // Category
            // Distribution Lists


            // Messenger ID
            if (csvRecord[7].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[7], "Yahoo!"));


            // Home
            if (csvRecord[8].Length > 0) contact.Items.Add(new Phone(csvRecord[8], "Home"));

            // Work
            if (csvRecord[9].Length > 0) contact.Items.Add(new Phone(csvRecord[9], "Work"));

            // Pager
            if (csvRecord[10].Length > 0) contact.Items.Add(new Phone(csvRecord[10], "Pager"));

            // Fax
            if (csvRecord[11].Length > 0) contact.Items.Add(new Phone(csvRecord[11], "Fax"));

            // Mobile
            if (csvRecord[12].Length > 0) contact.Items.Add(new Phone(csvRecord[12], "Mobile"));

            // Other
            if (csvRecord[13].Length > 0) contact.Items.Add(new Phone(csvRecord[13], "Other"));

            // Yahoo! Phone
            if (csvRecord[14].Length > 0) contact.Items.Add(new Phone(csvRecord[14], "Yahoo! Phone"));


            // Primary


            // Alternate Email 1
            if (csvRecord[16].Length > 0) contact.Items.Add(new Email(csvRecord[16], "Alternate Email 1"));

            // Alternate Email 2
            if (csvRecord[17].Length > 0) contact.Items.Add(new Email(csvRecord[17], "Alternate Email 2"));


            // Personal Website
            if (csvRecord[18].Length > 0) contact.Items.Add(new WebSite(csvRecord[18], "Personal Website"));

            // Business Website
            if (csvRecord[19].Length > 0) contact.Items.Add(new WebSite(csvRecord[19], "Business Website"));


            // Title
            if (csvRecord[20].Length > 0) contact.Notes += "Title: " + csvRecord[20] + "\r\n";

            // Company
            if (csvRecord[21].Length > 0) contact.Notes += "Company: " + csvRecord[21] + "\r\n";


            // Work Address
            // Work City
            // Work State
            // Work ZIP
            // Work Country
            if (csvRecord[22].Length > 0 ||
                csvRecord[23].Length > 0 ||
                csvRecord[24].Length > 0 ||
                csvRecord[25].Length > 0 ||
                csvRecord[26].Length > 0)
            {
                contact.Items.Add(new PostalAddress(csvRecord[22], csvRecord[23], csvRecord[24], csvRecord[25], csvRecord[26], "Work Address"));
            }

            // Home Address
            // Home City
            // Home State
            // Home ZIP
            // Home Country
            if (csvRecord[27].Length > 0 ||
                csvRecord[28].Length > 0 ||
                csvRecord[29].Length > 0 ||
                csvRecord[30].Length > 0 ||
                csvRecord[31].Length > 0)
            {
                contact.Items.Add(new PostalAddress(csvRecord[27], csvRecord[28], csvRecord[29], csvRecord[30], csvRecord[31], "Home Address"));
            }


            // Birthday
            if (csvRecord[32].Length > 0) contact.Birthday.FromString(csvRecord[32]);

            // Anniversary
            if (csvRecord[33].Length > 0) contact.Items.Add(Date.Parse(csvRecord[33]));


            // Custom 1
            if (csvRecord[34].Length > 0) contact.Notes += csvRecord[34] + "\r\n";

            // Custom 2
            if (csvRecord[35].Length > 0) contact.Notes += csvRecord[35] + "\r\n";

            // Custom 3
            if (csvRecord[36].Length > 0) contact.Notes += csvRecord[36] + "\r\n";

            // Custom 4
            if (csvRecord[37].Length > 0) contact.Notes += csvRecord[37] + "\r\n";

            // Comments
            if (csvRecord[38].Length > 0) contact.Notes += csvRecord[38];


            // Messenger ID1
            if (csvRecord[39].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[39], "Messenger ID1"));

            // Messenger ID2
            if (csvRecord[40].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[40], "Messenger ID2"));

            // Messenger ID3
            if (csvRecord[41].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[41], "Messenger ID3"));

            // Messenger ID4
            if (csvRecord[42].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[42], "Messenger ID4"));

            // Messenger ID5
            if (csvRecord[43].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[43], "Messenger ID5"));

            // Messenger ID6
            if (csvRecord[44].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[44], "Messenger ID6"));

            // Messenger ID7
            if (csvRecord[45].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[45], "Messenger ID7"));

            // Messenger ID8
            if (csvRecord[46].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[46], "Messenger ID8"));

            // Messenger ID9
            if (csvRecord[47].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[47], "Messenger ID9"));

            // Skype ID
            if (csvRecord[48].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[48], "Skype ID"));

            // IRC ID
            if (csvRecord[49].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[49], "IRC ID"));

            // ICQ ID
            if (csvRecord[50].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[50], "ICQ ID"));

            // Google ID
            if (csvRecord[51].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[51], "Google ID"));

            // MSN ID
            if (csvRecord[52].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[52], "MSN ID"));

            // AIM ID
            if (csvRecord[53].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[53], "AIM ID"));

            // QQ ID
            if (csvRecord[54].Length > 0) contact.Items.Add(new SocialProfile(csvRecord[54], "QQ ID"));

            return contact;
        }
    }
}