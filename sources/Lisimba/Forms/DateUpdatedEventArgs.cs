using System;
using DustInTheWind.Lisimba.Egg.Entities;

namespace DustInTheWind.Lisimba.Forms
{
    public class DateUpdatedEventArgs : EventArgs
    {
        public Date Date { get; private set; }

        public DateUpdatedEventArgs(Date date)
        {
            Date = date;
        }
    }
}