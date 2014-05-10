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

using System;
using System.IO;
using System.Threading;
using DustInTheWind.Desmond.Config;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Desmond
{
    internal class RedEye : TaskRunner
    {
        private Timer timer;
        private AddressBookManager addressBookManager;
        private AddressBook addressBook;
        private object lastContact;
        private Contact nextContact;
        private DateTime nextBirthday = DateTime.MinValue;
        private DesmondConfigurationSection config;
        private DateTime fileModifiedDate;

        public RedEye()
            : base()
        {
            timer = new Timer(new TimerCallback(timer_TimerElapsed), null, Timeout.Infinite, Timeout.Infinite);

            task = new TaskMethod(Check);

            config = DesmondConfigurationSection.GetSection();

            if (string.IsNullOrEmpty(config.AddressBook.File))
                throw new ApplicationException("The configuration file does not specify an address book to load.");

            addressBookManager = new AddressBookManager();
            addressBookManager.IncorrectXmlVersion += addressBookLoader_IncorrectXmlVersion;
        }

        private void timer_TimerElapsed(object o)
        {
            System.Windows.Forms.MessageBox.Show(nextContact.ToString());
        }

        void addressBookLoader_IncorrectXmlVersion(object sender, IncorrectXmlVersionEventArgs e)
        {
            e.ContinueParsing = true;
        }

        private DateTime GetFileModifiedDate()
        {
            string addressBookFileName = config.AddressBook.File;
            FileInfo info = new FileInfo(addressBookFileName);
            return info.LastWriteTime;
        }

        private void LoadAddressBook(bool force)
        {
            if (!force)
            {
                DateTime fileModifiedDate = GetFileModifiedDate();

                if (this.fileModifiedDate < fileModifiedDate)
                    this.fileModifiedDate = fileModifiedDate;
                else
                    return;
            }

            string addressBookFileName = config.AddressBook.File;
            addressBook = addressBookManager.LoadFromFile(addressBookFileName);

            // Sort by birthday
            addressBook.Contacts.Sort(ContactsSortingType.Birthday, SortDirection.Ascending);
        }

        private void Check()
        {
            LoadAddressBook(false);

            if (addressBook == null)
            {
                Log.Instance.WriteLine("The address book file does not exist.");
                Thread.Sleep(1000);
                return;
            }
            else if (addressBook.Contacts.Count == 0)
            {
                Log.Instance.WriteLine("The address book does not contain any contacts.");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                DateTime today = DateTime.Today;

                nextContact = GetNextContact(addressBook, today);
                nextBirthday = new DateTime(today.Year, nextContact.Birthday.Month, nextContact.Birthday.Day);
                if (nextBirthday < today)
                    nextBirthday = nextBirthday.AddYears(1);

                TimeSpan dueTime = nextBirthday - today;
                TimeSpan period = TimeSpan.FromMilliseconds(-1);
                timer.Change(dueTime, period);
            }
        }

        /// <summary>
        /// Returns the first <see cref="Contact"/> that has the birthday after the specified date.
        /// </summary>
        /// <param name="addressBook"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        private Contact GetNextContact(AddressBook addressBook, DateTime date)
        {
            if (addressBook.Contacts.Count == 0)
                return null;

            foreach (Contact contact in this.addressBook.Contacts)
            {
                if (contact.Birthday.Month > date.Month || (contact.Birthday.Month == date.Month && contact.Birthday.Day >= date.Day))
                {
                    return contact;
                }
            }

            // The end of the list was reached and no contact was found. Wrap around and take the first contact from the list.
            return this.addressBook.Contacts[0];
        }
    }
}
