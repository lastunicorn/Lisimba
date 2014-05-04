using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    public class LisimbaConfigSection : ConfigurationSection
    {
        public LisimbaConfigSection()
        {
        }

        [ConfigurationProperty("recentFiles")]
        public RecentFilesConfigElementCollection RecentFilesList
        {
            get
            {
                return (RecentFilesConfigElementCollection)this["recentFiles"] ?? new RecentFilesConfigElementCollection();
            }
        }

        [ConfigurationProperty("loadFileAtStart")]
        public LoadFileAtStartConfigElement LoadFileAtStart
        {
            get
            {
                return (LoadFileAtStartConfigElement)this["loadFileAtStart"] ?? new LoadFileAtStartConfigElement();
            }
        }

        //[ConfigurationProperty("sortBy")]
        //public string SortBy
        //{
        //    get
        //    {
        //        return (string)this["sortBy"] ?? string.Empty;
        //    }
        //}

        [ConfigurationProperty("sortBy")]
        public SortByConfigElement SortBy
        {
            get
            {
                return (SortByConfigElement)this["sortBy"] ?? new SortByConfigElement();
            }
        }

        //protected override ConfigurationElement CreateNewElement()
        //{
        //    return new RecentFileConfigSection();
        //}

        //protected override object GetElementKey(ConfigurationElement element)
        //{
        //    return null;
        //}

        //[ConfigurationProperty("recentFile", IsRequired = false, IsKey = false)]
        //public RecentFileConfigSection RecentFile
        //{
        //    get
        //    {
        //        return (RecentFileConfigSection)this["recentFile"] ?? new RecentFileConfigSection();
        //    }
        //    set
        //    {
        //        this["recentFile"] = value;
        //    }
        //}

        //public static LisimbaConfigSection GetSection()
        //{
        //    return this.config.GetSection("lisimba") as LisimbaConfigSection;
        //}
    }
}
