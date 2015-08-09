using System;
using System.ComponentModel;
using DustInTheWind.Lisimba.Services;
using DustInTheWind.Lisimba.ViewModels;

namespace DustInTheWind.Lisimba.Forms
{
    class TrayIconPresenter
    {
        private readonly LisimbaApplication lisimbaApplication;
        private TrayIcon trayIcon;

        public TrayIcon TrayIcon
        {
            get { return trayIcon; }
            set
            {
                trayIcon = value;

                lisimbaApplication.Exiting += HandleLisimbaApplicationExiting;
                TrayIcon.Visible = true;
            }
        }

        public TrayIconPresenter(LisimbaApplication lisimbaApplication)
        {
            this.lisimbaApplication = lisimbaApplication;
            if (lisimbaApplication == null) throw new ArgumentNullException("lisimbaApplication");
        }

        private void HandleLisimbaApplicationExiting(object sender, CancelEventArgs cancelEventArgs)
        {
            TrayIcon.Visible = false;
        }
    }
}