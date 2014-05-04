using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;

namespace DustInTheWind.Desmond
{
    /// <summary>
    /// <para>
    /// The Windows service that instanciates the <see cref="RedEye"/> class.
    /// </para>
    /// <para>
    /// The application may be started as a desktop application or it can be installed as a Windows service.
    /// This class represents the Windows service.
    /// </para>
    /// </summary>
    public partial class DesmondService : ServiceBase
    {
        private RedEye redEye;

        public DesmondService()
        {
            InitializeComponent();

            this.redEye = new RedEye();
        }

        protected override void OnStart(string[] args)
        {
            this.redEye.Start();
        }

        protected override void OnStop()
        {
            this.redEye.Stop();
        }
    }
}
