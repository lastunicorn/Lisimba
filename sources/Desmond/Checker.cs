using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.Desmond
{
    internal class Checker
    {
        private object[] contacts;
        private object lastContact;
        private DateTime fileModifiedDate;

        public Checker()
        {
            TaskRunner taskRunner = new TaskRunner(new TaskMethod(this.Check));
            taskRunner.Start();
        }

        private DateTime GetFileModifiedDate()
        {
            return DateTime.MinValue;
        }

        private void Check()
        {
            // Check if contacts file was modified.
            // If was modified, reload it.
            // Order the contacts by birth date.

            // Check if birth date 
        }
    }
}
