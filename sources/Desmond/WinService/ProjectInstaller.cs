using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using DustInTheWind.Lisimba.Utils;

namespace DustInTheWind.Desmond
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            try
            {
                ServiceController serviceController = new ServiceController(this.serviceInstaller1.ServiceName);
                serviceController.Start();
            }
            catch(Exception ex)
            {
                Log.Instance.WriteError(ex);
            }
        }
    }
}