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
using System.Reflection;
using DustInTheWind.Desmond.Config;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Egg.Entities;
using DustInTheWind.Lisimba.Egg.Enums;
using DustInTheWind.Lisimba.Utils;
using System.Threading;

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
            this.timer = new Timer(new TimerCallback(this.timer_TimerElapsed), null, Timeout.Infinite, Timeout.Infinite);

            this.task = new TaskMethod(this.Check);

            this.config = DesmondConfigurationSection.GetSection();

            if (string.IsNullOrEmpty(this.config.AddressBook.File))
                throw new ApplicationException("The configuration file does not specify an address book to load.");

            this.addressBookManager = new AddressBookManager();
            this.addressBookManager.IncorrectXmlVersion += addressBookLoader_IncorrectXmlVersion;
        }

        private void timer_TimerElapsed(object o)
        {
            System.Windows.Forms.MessageBox.Show(this.nextContact.ToString());
        }

        void addressBookLoader_IncorrectXmlVersion(object sender, IncorrectXmlVersionEventArgs e)
        {
            e.ContinueParsing = true;
        }

        private DateTime GetFileModifiedDate()
        {
            string addressBookFileName = this.config.AddressBook.File;
            FileInfo info = new FileInfo(addressBookFileName);
            return info.LastWriteTime;
        }

        private void LoadAddressBook(bool force)
        {
            if (!force)
            {
                DateTime fileModifiedDate = this.GetFileModifiedDate();

                if (this.fileModifiedDate < fileModifiedDate)
                    this.fileModifiedDate = fileModifiedDate;
                else
                    return;
            }

            string addressBookFileName = this.config.AddressBook.File;
            this.addressBook = this.addressBookManager.LoadFromFile(addressBookFileName);

            // Sort by birthday
            this.addressBook.Contacts.Sort(ContactsSortingType.Birthday, SortDirection.Ascending);
        }

        private void Check()
        {
            this.LoadAddressBook(false);

            if (this.addressBook == null)
            {
                Log.Instance.WriteLine("The address book file does not exist.");
                Thread.Sleep(1000);
                return;
            }
            else if (this.addressBook.Contacts.Count == 0)
            {
                Log.Instance.WriteLine("The address book does not contain any contacts.");
                Thread.Sleep(1000);
                return;
            }
            else
            {
                DateTime today = DateTime.Today;

                this.nextContact = this.GetNextContact(this.addressBook, today);
                this.nextBirthday = new DateTime(today.Year, this.nextContact.Birthday.Month, this.nextContact.Birthday.Day);
                if (this.nextBirthday < today)
                    this.nextBirthday = this.nextBirthday.AddYears(1);

                TimeSpan dueTime = this.nextBirthday - today;
                TimeSpan period = TimeSpan.FromMilliseconds(-1);
                this.timer.Change(dueTime, period);
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
