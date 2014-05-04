using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    public class SortByConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("value", DefaultValue="NicknameOrName", IsRequired = true, IsKey = false)]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
    }
}
