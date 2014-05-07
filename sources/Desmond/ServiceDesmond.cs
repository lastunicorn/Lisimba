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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Reflection;
using System.Configuration;
using DustInTheWind.Lisimba.Egg;
using DustInTheWind.Lisimba.Utils;
using System.Threading;

namespace DustInTheWind.Lisimba.Desmond
{
    public partial class ServiceDesmond : ServiceBase
    {
        //private FileSystemWatcher configFileWatcher = null;
        //private string linFilePath = string.Empty;
        private Timer timer = null;
        private AddressBookManager addressBookLoader = new AddressBookManager();
        private AddressBook addressBook;
        private Contact nextContact = null;
        private DateTime nextBirthday = DateTime.MinValue;

        public ServiceDesmond()
        {
            InitializeComponent();

            //this.configFileWatcher = new FileSystemWatcher(GetAssemblyPath(), GetConfigFileShortName());
            //this.configFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            //this.configFileWatcher.Changed += new FileSystemEventHandler(configFileWatcher_Changed);

            this.timer = new Timer(new TimerCallback(timer_TimerElapsed));
            this.addressBookLoader.IncorrectXmlVersion += new AddressBookManager.IncorrectXmlVersionHandler(addressBookLoader_IncorrectXmlVersion);
        }

        void addressBookLoader_IncorrectXmlVersion(object sender, AddressBookManager.IncorrectXmlVersionEventArgs e)
        {
            e.ContinueParsing = true;
        }

        private void timer_TimerElapsed(object o)
        {
            Log.Instance.WriteLine("Timer Elapsed");

            if (nextContact == null)
                return;

            Log.Instance.WriteLine("Days left: " + (nextBirthday - DateTime.Today).Days.ToString());
        }

        private string GetConfigFileFullName()
        {
            return Assembly.GetEntryAssembly().Location + ".config";
        }

        private string GetAssemblyPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private string GetConfigFileShortName()
        {
            return Path.GetFileName(Assembly.GetEntryAssembly().Location + ".config");
        }

        #region For Debug only

        internal void ManualStart()
        {
            this.OnStart(null);
        }

        internal void ManualStop()
        {
            this.OnStop();
        }

        #endregion

        protected override void OnStart(string[] args)
        {
            try
            {
                LogManager.OpenGlobalLogFile();
                Log.Instance.WriteLine("=====================================================================================");
                Log.Instance.WriteLine("Service Starting...");

                this.LoadBook();

                //Log.Instance.WriteLine("Start the timer.");
                //this.timer.Change(TimeSpan.FromTicks(0), TimeSpan.FromHours(1));
                //this.timer.Change(TimeSpan.FromTicks(0), TimeSpan.FromSeconds(3));
            }
            catch (Exception ex)
            {
                Log.Instance.WriteError(ex);

                this.Stop();

                Log.Instance.WriteLine("Service stopped.");
                Log.Instance.CloseFile();
            }
        }

        private void LoadBook()
        {
            string bookFile = ConfigurationManager.AppSettings["bookFile"];
            if (string.IsNullOrEmpty(bookFile))
                throw new ApplicationException("The configuration file does not specify an address book to load.");

            //AddressBook a = this.addressBookLoader.LoadFromFile(this.GetAssemblyPath() + "\\" + bookFile);
            addressBook = this.addressBookLoader.LoadFromFile(bookFile);

            if (addressBook.Contacts.Count == 0)
            {
                Log.Instance.WriteLine("The address book does not contains any contacts.");
                return;
            }

            // Sort by birthday
            addressBook.Contacts.Sort(SortField.Birthday, SortDirection.Ascending);

            // Find the next birthday

            DateTime today = DateTime.Today;
            Date birthday;
            
            nextContact = null;

            for (int i = 0; i < addressBook.Contacts.Count; i++)
            {
                birthday = addressBook.Contacts[i].Birthday;
                if (birthday.Month > today.Month ||
                    (birthday.Month == today.Month && birthday.Day >= today.Day))
                {
                    Log.Instance.WriteLine(addressBook.Contacts[i].Name.ToString() + " - " + addressBook.Contacts[i].Birthday.ToDateTime());
                    nextContact = addressBook.Contacts[i];
                    nextBirthday = new DateTime(today.Year, nextContact.Birthday.Month, nextContact.Birthday.Day);
                    break;
                }
            }

            if (nextContact == null)
            {
                nextContact = addressBook.Contacts[0];
                nextBirthday = new DateTime(today.Year + 1, nextContact.Birthday.Month, nextContact.Birthday.Day);
                Log.Instance.WriteLine(nextContact.Name.ToString() + " - " + nextContact.Birthday.ToDateTime());
            }

            Log.Instance.WriteLine("Start the timer.");
            this.timer.Change(TimeSpan.FromTicks(0), TimeSpan.FromSeconds(3));
        }

        protected override void OnStop()
        {
            Log.Instance.WriteLine("Stop the timer.");
            this.timer.Change(Timeout.Infinite, Timeout.Infinite);

            Log.Instance.WriteLine("Service stopped.");
            Log.Instance.CloseFile();
        }

        //void configFileWatcher_Changed(object sender, FileSystemEventArgs e)
        //{
        //    this.LoadConfig();
        //}

        //private void LoadConfig()
        //{
            
        //}

        
    }
}
