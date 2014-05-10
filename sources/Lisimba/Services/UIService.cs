using System;
using System.Windows.Forms;

namespace DustInTheWind.Lisimba.Services
{
    public class UIService
    {
        private Form mainForm;

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
    }
}
