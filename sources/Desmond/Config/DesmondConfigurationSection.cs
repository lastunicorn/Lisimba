using System.Configuration;

namespace DustInTheWind.Desmond.Config
{
    public class DesmondConfigurationSection : ConfigurationSection
    {
        private const string SECTION_NAME = "desmond";

        public static DesmondConfigurationSection GetSection()
        {
            return (DesmondConfigurationSection)ConfigurationManager.GetSection(DesmondConfigurationSection.SECTION_NAME);
        }

        public static DesmondConfigurationSection GetSection(Configuration config)
        {
            return (DesmondConfigurationSection)config.GetSection(DesmondConfigurationSection.SECTION_NAME);
        }

        [ConfigurationProperty("addressBook")]
        public AddressBookConfigurationElement AddressBook
        {
            get { return (AddressBookConfigurationElement)this["addressBook"]; }
            set { this["addressBook"] = value; }
        }
    }
}