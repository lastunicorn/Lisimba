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
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Egg.Searching;

namespace DustInTheWind.Lisimba.Gating
{
    public class Saver
    {
        public void Save(AddressBook addressBook, Stream stream)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.Configuration.QuoteAllFields = true;
                    csvWriter.Configuration.IgnoreBlankLines = true;
                    csvWriter.Configuration.Quote = '"';
                    csvWriter.Configuration.Delimiter = ",";

                    WriteHeader(csvWriter);

                    foreach (Contact contact in addressBook.Contacts)
                    {
                        WriteCsvRecord(contact, csvWriter);
                    }
                }
            }
        }

        private static void WriteHeader(ICsvWriter csvWriter)
        {
            csvWriter.WriteField("First");
            csvWriter.WriteField("Middle");
            csvWriter.WriteField("Last");
            csvWriter.WriteField("Nickname");
            csvWriter.WriteField("Email");
            csvWriter.WriteField("Category");
            csvWriter.WriteField("Distribution Lists");
            csvWriter.WriteField("Messenger ID");
            csvWriter.WriteField("Home");
            csvWriter.WriteField("Work");
            csvWriter.WriteField("Pager");
            csvWriter.WriteField("Fax");
            csvWriter.WriteField("Mobile");
            csvWriter.WriteField("Other");
            csvWriter.WriteField("Yahoo! Phone");
            csvWriter.WriteField("Primary");
            csvWriter.WriteField("Alternate Email 1");
            csvWriter.WriteField("Alternate Email 2");
            csvWriter.WriteField("Personal Website");
            csvWriter.WriteField("Business Website");
            csvWriter.WriteField("Title");
            csvWriter.WriteField("Company");
            csvWriter.WriteField("Work Address");
            csvWriter.WriteField("Work City");
            csvWriter.WriteField("Work State");
            csvWriter.WriteField("Work ZIP");
            csvWriter.WriteField("Work Country");
            csvWriter.WriteField("Home Address");
            csvWriter.WriteField("Home City");
            csvWriter.WriteField("Home State");
            csvWriter.WriteField("Home ZIP");
            csvWriter.WriteField("Home Country");
            csvWriter.WriteField("Birthday");
            csvWriter.WriteField("Anniversary");
            csvWriter.WriteField("Custom 1");
            csvWriter.WriteField("Custom 2");
            csvWriter.WriteField("Custom 3");
            csvWriter.WriteField("Custom 4");
            csvWriter.WriteField("Comments");
            csvWriter.WriteField("Messenger ID1");
            csvWriter.WriteField("Messenger ID2");
            csvWriter.WriteField("Messenger ID3");
            csvWriter.WriteField("Messenger ID4");
            csvWriter.WriteField("Messenger ID5");
            csvWriter.WriteField("Messenger ID6");
            csvWriter.WriteField("Messenger ID7");
            csvWriter.WriteField("Messenger ID8");
            csvWriter.WriteField("Messenger ID9");
            csvWriter.WriteField("Skype ID");
            csvWriter.WriteField("IRC ID");
            csvWriter.WriteField("ICQ ID");
            csvWriter.WriteField("Google ID");
            csvWriter.WriteField("MSN ID");
            csvWriter.WriteField("AIM ID");
            csvWriter.WriteField("QQ ID");

            csvWriter.NextRecord();
        }

        private void WriteCsvRecord(Contact contact, CsvWriter csvWriter)
        {
            Phone phone;
            Email email;
            PostalAddress postalAddress;
            Date date;
            SocialProfile socialProfile;

            // First
            csvWriter.WriteField(contact.Name.FirstName);

            // Middle
            csvWriter.WriteField(contact.Name.MiddleName);

            // Last
            csvWriter.WriteField(contact.Name.LastName);

            // Nickname
            csvWriter.WriteField(contact.Name.Nickname);


            // Email
            email = contact.Items.SearchByDescription("Email", SearchMode.Exact) as Email;
            csvWriter.WriteField(email == null ? string.Empty : email.Address);

            // Category
            csvWriter.WriteField(contact.Category);

            // Distribution Lists
            csvWriter.WriteField(string.Empty);

            // Messenger ID
            socialProfile = contact.Items.SearchByDescription("Messenger ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Home
            phone = contact.Items.SearchByDescription("Home", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Work
            phone = contact.Items.SearchByDescription("Work", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Pager
            phone = contact.Items.SearchByDescription("Pager", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Fax
            phone = contact.Items.SearchByDescription("Fax", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Mobile
            phone = contact.Items.SearchByDescription("Mobile", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Other
            phone = contact.Items.SearchByDescription("Other", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Yahoo! Phone
            phone = contact.Items.SearchByDescription("Yahoo! Phone", SearchMode.Exact) as Phone;
            csvWriter.WriteField(phone == null ? string.Empty : phone.Number);

            // Primary
            csvWriter.WriteField(string.Empty);

            // Alternate Email 1
            email = contact.Items.SearchByDescription("Alternate Email 1", SearchMode.Exact) as Email;
            csvWriter.WriteField(email == null ? string.Empty : email.Address);

            // Alternate Email 2
            email = contact.Items.SearchByDescription("Alternate Email 2", SearchMode.Exact) as Email;
            csvWriter.WriteField(email == null ? string.Empty : email.Address);

            // Personal Website
            csvWriter.WriteField(string.Empty);

            // Business Website
            csvWriter.WriteField(string.Empty);

            // Title
            csvWriter.WriteField(string.Empty);

            // Company
            //csvRecord.WriteField(contact.sCompany);
            csvWriter.WriteField(string.Empty);

            // Work Address
            postalAddress = contact.Items.SearchByDescription("Work Address", SearchMode.Exact) as PostalAddress;
            if (postalAddress != null)
            {
                csvWriter.WriteField(postalAddress.Street); // Work Address
                csvWriter.WriteField(postalAddress.City); // Work City
                csvWriter.WriteField(postalAddress.State); // Work State
                csvWriter.WriteField(postalAddress.PostalCode); // Work ZIP
                csvWriter.WriteField(postalAddress.Country); // Work Country
            }
            else
            {
                csvWriter.WriteField(string.Empty); // Work Address
                csvWriter.WriteField(string.Empty); // Work City
                csvWriter.WriteField(string.Empty); // Work State
                csvWriter.WriteField(string.Empty); // Work ZIP
                csvWriter.WriteField(string.Empty); // Work Country
            }

            // Home Address
            postalAddress = contact.Items.SearchByDescription("Home Address", SearchMode.Exact) as PostalAddress;
            if (postalAddress != null)
            {
                csvWriter.WriteField(postalAddress.Street); // Home Address
                csvWriter.WriteField(postalAddress.City); // Home City
                csvWriter.WriteField(postalAddress.State); // Home State
                csvWriter.WriteField(postalAddress.PostalCode); // Home ZIP
                csvWriter.WriteField(postalAddress.Country); // Home Country
            }
            else
            {
                csvWriter.WriteField(string.Empty); // Home Address
                csvWriter.WriteField(string.Empty); // Home City
                csvWriter.WriteField(string.Empty); // Home State
                csvWriter.WriteField(string.Empty); // Home ZIP
                csvWriter.WriteField(string.Empty); // Home Country
            }

            // Birthday
            if (!contact.Birthday.IsNull())
            {
                date = contact.Birthday;
                string dateAsString = (date.Month > 0 ? date.Month.ToString() : string.Empty) + "/" +
                                      (date.Day > 0 ? date.Day.ToString() : string.Empty) + "/" +
                                      (date.Year > 0 ? date.Year.ToString() : string.Empty);
                csvWriter.WriteField(dateAsString);
            }
            else
            {
                csvWriter.WriteField(string.Empty);
            }

            // Anniversary
            csvWriter.WriteField(string.Empty);

            csvWriter.WriteField(string.Empty); // Custom 1
            csvWriter.WriteField(string.Empty); // Custom 2
            csvWriter.WriteField(string.Empty); // Custom 3
            csvWriter.WriteField(string.Empty); // Custom 4

            // Comments
            csvWriter.WriteField(contact.Notes);

            // Messenger ID1
            socialProfile = contact.Items.SearchByDescription("Messenger ID1", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID2
            socialProfile = contact.Items.SearchByDescription("Messenger ID2", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID3
            socialProfile = contact.Items.SearchByDescription("Messenger ID3", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID4
            socialProfile = contact.Items.SearchByDescription("Messenger ID4", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID5
            socialProfile = contact.Items.SearchByDescription("Messenger ID5", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID6
            socialProfile = contact.Items.SearchByDescription("Messenger ID6", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID7
            socialProfile = contact.Items.SearchByDescription("Messenger ID7", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID8
            socialProfile = contact.Items.SearchByDescription("Messenger ID8", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Messenger ID9
            socialProfile = contact.Items.SearchByDescription("Messenger ID9", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Skype ID
            socialProfile = contact.Items.SearchByDescription("Skype ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // IRC ID
            socialProfile = contact.Items.SearchByDescription("IRC ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // ICQ ID
            socialProfile = contact.Items.SearchByDescription("ICQ ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // Google ID
            socialProfile = contact.Items.SearchByDescription("Google ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // MSN ID
            socialProfile = contact.Items.SearchByDescription("MSN ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // AIM ID
            socialProfile = contact.Items.SearchByDescription("AIM ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            // QQ ID
            socialProfile = contact.Items.SearchByDescription("QQ ID", SearchMode.Exact) as SocialProfile;
            csvWriter.WriteField(socialProfile == null ? string.Empty : socialProfile.Id);

            csvWriter.NextRecord();
        }
    }
}