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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DustInTheWind.Lisimba.Egg.AddressBookModel;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Main;
using DustInTheWind.Lisimba.Properties;

namespace DustInTheWind.Lisimba.Services
{
    internal class WindowSystem
    {
        private readonly UiFactory uiFactory;
        private Form mainWindow;
        private TrayIcon trayIcon;

        public Form MainWindow
        {
            get { return mainWindow; }
            private set
            {
                if (mainWindow != null)
                    mainWindow.Closed -= HandleMainWindowClosed;

                mainWindow = value;

                if (mainWindow != null)
                    mainWindow.Closed += HandleMainWindowClosed;
            }
        }

        private void HandleMainWindowClosed(object sender, EventArgs args)
        {
            MainWindow = null;
        }

        public WindowSystem(UiFactory uiFactory, ApplicationStatus applicationStatus)
        {
            if (uiFactory == null) throw new ArgumentNullException("uiFactory");
            if (applicationStatus == null) throw new ArgumentNullException("applicationStatus");

            this.uiFactory = uiFactory;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            applicationStatus.DefaultStatusText = LocalizedResources.DefaultStatusText;
        }

        public void CreateMainWindow()
        {
            MainWindow = uiFactory.GetForm<LisimbaForm>();
        }

        public void ShowTrayIcon()
        {
            if (trayIcon == null)
                trayIcon = uiFactory.GetComponent<TrayIcon>();

            trayIcon.Visible = true;
        }

        public void HideTrayIcon()
        {
            if (trayIcon != null)
                trayIcon.Visible = false;
        }

        public void DisplayInfo(string message)
        {
            MessageBox.Show(MainWindow, message, Resources.InfoPopup_Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void DisplayWarning(IEnumerable<Exception> warnings)
        {
            using (WarningsForm form = new WarningsForm())
            {
                form.Warnings = warnings.ToList();
                form.ShowDialog(mainWindow);
            }
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(MainWindow, message, Resources.ErrorPopup_Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool? DisplayYesNoCancelQuestion(string question, string title)
        {
            DialogResult dialogResult = MessageBox.Show(MainWindow, question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return null;

            return dialogResult == DialogResult.Yes;
        }

        public bool DisplayYesNoExclamation(string text, string title)
        {
            DialogResult dialogResult = MessageBox.Show(text, title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);

            return dialogResult == DialogResult.Yes;
        }

        public string AskToSave(string extension, string filter)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog.Filter = filter;
                saveFileDialog.DefaultExt = extension;

                DialogResult dialogResult = saveFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? saveFileDialog.FileName : null;
            }
        }

        public string AskToOpen(string extension, string filter)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = filter;
                openFileDialog.DefaultExt = extension;

                DialogResult dialogResult = openFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? openFileDialog.FileName : null;
            }
        }

        public void ShowAbout()
        {
            using (AboutForm form = uiFactory.GetForm<AboutForm>())
            {
                if (mainWindow == null)
                    form.StartPosition = FormStartPosition.CenterScreen;

                form.ShowDialog(MainWindow);
            }
        }

        public void DisplayAddressBookProperties()
        {
            using (AddressBookPropertiesForm form = uiFactory.GetForm<AddressBookPropertiesForm>())
            {
                form.ShowDialog(MainWindow);
            }
        }

        public void DisplayMainWindow()
        {
            if (mainWindow == null)
                CreateMainWindow();

            MainWindow.Show();
            MainWindow.Activate();
        }

        public void DisplayAddContactWindow()
        {
            AddContactForm form = uiFactory.GetForm<AddContactForm>();
            form.Show(MainWindow);
        }

        public void ShowGateSelector(Point point)
        {
            GateSelectorForm form = uiFactory.GetForm<GateSelectorForm>();

            int x = point.X - form.Width;
            int y = point.Y - form.Height;
            form.Location = new Point(x, y);

            form.Show();
        }

        public void DisplayBirthdays(IEnumerable<Contact> contacts, DateTime startDate, DateTime endDate)
        {
            string birthdaysInfo = BuildInfoText(contacts, startDate, endDate);
            DisplayInfo(birthdaysInfo);
        }

        private string BuildInfoText(IEnumerable<Contact> contacts, DateTime startDate, DateTime endDate)
        {
            StringBuilder sb = new StringBuilder();

            double totalDays = (endDate - startDate).TotalDays;
            sb.AppendLine("The birthdays for the next " + totalDays + " days are:");
            sb.AppendLine();

            foreach (Contact contact in contacts)
            {
                string line = string.Format("{0} - {1}", contact.Name, contact.Birthday.ToShortString());
                sb.AppendLine(line);
            }

            return sb.ToString();
        }
    }
}