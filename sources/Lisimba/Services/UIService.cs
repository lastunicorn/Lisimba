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
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Services
{
    public class UIService
    {
        private readonly Form mainForm;

        public UIService(Form mainForm)
        {
            if (mainForm == null)
                throw new ArgumentNullException("mainForm");

            this.mainForm = mainForm;
        }

        public void DisplayWarning(string message)
        {
            MessageBox.Show(mainForm, message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void DisplayError(string message)
        {
            MessageBox.Show(mainForm, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool? DisplayYesNoQuestion(string question, string title)
        {
            DialogResult dialogResult = MessageBox.Show(question, title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

            if (dialogResult == DialogResult.Cancel)
                return null;

            return dialogResult == DialogResult.Yes;
        }
    }
}
