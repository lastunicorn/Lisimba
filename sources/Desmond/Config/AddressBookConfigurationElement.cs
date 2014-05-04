using System.Configuration;

namespace DustInTheWind.Desmond.Config
{
    public class AddressBookConfigurationElement: ConfigurationElement
    {
        [ConfigurationProperty("file")]
        public string File
        {
            get { return (string)this["file"]; }
            set { this["file"] = value; }
        }
    }
}
