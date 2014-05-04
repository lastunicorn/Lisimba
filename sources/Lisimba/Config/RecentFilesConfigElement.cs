using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    public class RecentFilesConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName", IsRequired = true, IsKey = false)]
        public string FileName
        {
            get { return (string)this["fileName"]; }
            set { this["fileName"] = value; }
        }

    }
}
