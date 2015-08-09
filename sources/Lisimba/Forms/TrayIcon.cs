using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DustInTheWind.Lisimba.Forms
{
    partial class TrayIcon : Component
    {
        public bool Visible
        {
            get { return notifyIcon1.Visible; }
            set { notifyIcon1.Visible = value; }
        }

        public TrayIcon()
        {
            InitializeComponent();
        }

        public TrayIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
