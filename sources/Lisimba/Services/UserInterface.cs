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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Forms;
using DustInTheWind.Lisimba.Properties;
using DustInTheWind.Lisimba.UserControls;

namespace DustInTheWind.Lisimba.Services
{
    internal class UserInterface
    {
        private readonly UiFactory uiFactory;
        private Form mainWindow;
        private TrayIcon trayIcon;

        private Form MainWindow
        {
            get { return mainWindow; }
            set
            {
                if (mainWindow != null)
                    mainWindow.Closed -= NandleMainWindowClosed;

                mainWindow = value;

                if (mainWindow != null)
                    mainWindow.Closed += NandleMainWindowClosed;
            }
        }

        private void NandleMainWindowClosed(object sender, EventArgs args)
        {
            MainWindow = null;
        }

        public UserInterface(UiFactory uiFactory)
        {
            if (uiFactory == null) throw new ArgumentNullException("uiFactory");

            this.uiFactory = uiFactory;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        public void RunAsWindowApp()
        {
            CreateMainWindow();
            Application.Run(MainWindow);
        }

        public void RunAsTrayApp()
        {
            CreateTrayIcon();
            Application.Run();
        }

        public void Exit()
        {
            Application.Exit();
        }

        public void DisplayWarning(string message)
        {
            MessageBox.Show(MainWindow, message, Resources.WarningPopup_Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        public string AskToSaveYahooCsvFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "csv";
                saveFileDialog.FileName = string.Empty;

                DialogResult dialogResult = saveFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? saveFileDialog.FileName : null;
            }
        }

        public string AskToOpenYahooCsvFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "Csv Files (*.csv)|*.csv|All Files (*.*)|*.*";
                openFileDialog.DefaultExt = "csv";
                openFileDialog.FileName = string.Empty;

                DialogResult dialogResult = openFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? openFileDialog.FileName : null;
            }
        }

        public string AskToSaveLsbFile()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                saveFileDialog.Filter = "Lisimba Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
                saveFileDialog.DefaultExt = "lsb";

                DialogResult dialogResult = saveFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? saveFileDialog.FileName : null;
            }
        }

        public string AskToOpenLsbFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Directory.GetCurrentDirectory();
                openFileDialog.Filter = "Lisimba Files (*.lsb)|*.lsb|All Files (*.*)|*.*";
                openFileDialog.DefaultExt = "lsb";

                DialogResult dialogResult = openFileDialog.ShowDialog(MainWindow);

                return dialogResult == DialogResult.OK ? openFileDialog.FileName : null;
            }
        }

        public void ShowAbout()
        {
            using (AboutForm form = new AboutForm())
            {
                form.ShowDialog(MainWindow);
            }
        }

        public void DisplayAddressBookProperties(AddressBookPropertiesViewModel viewModel)
        {
            using (AddressBookPropertiesForm form = new AddressBookPropertiesForm { ViewModel = viewModel })
            {
                form.ShowDialog(MainWindow);
            }
        }

        public void DisplayMainWindow()
        {
            CreateMainWindow();

            MainWindow.Show();
        }

        public void CreateMainWindow()
        {
            if (MainWindow != null)
                return;

            LisimbaForm lisimbaForm = uiFactory.GetForm<LisimbaForm>();
            lisimbaForm.ViewModel = uiFactory.GetViewModel<LisimbaViewModel>();

            MainWindow = lisimbaForm;
        }

        public void CreateTrayIcon()
        {
            TrayIcon trayIcon = new TrayIcon();
            TrayIconPresenter trayIconPresenter = uiFactory.GetViewModel<TrayIconPresenter>();

            trayIcon.Presenter = trayIconPresenter;
            trayIconPresenter.TrayIcon = trayIcon;

            this.trayIcon = trayIcon;
        }
    }
}