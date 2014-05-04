using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    public class LoadFileAtStartConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("type", DefaultValue = "last", IsRequired = true, IsKey = false)]
        public string Type
        {
            get
            {
                return (string)this["type"];
            }
            set
            {
                this["type"] = value;
            }
        }

        [ConfigurationProperty("fileName", IsRequired = false, IsKey = false)]
        public string FileName
        {
            get
            {
                return (string)this["fileName"];
            }
            set
            {
                this["fileName"] = value;
            }
        }
    }
}
