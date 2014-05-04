using System.Configuration;

namespace DustInTheWind.Lisimba.Config
{
    [ConfigurationCollection(typeof(RecentFilesConfigElement), AddItemName = "file")]
    public class RecentFilesConfigElementCollection : ConfigurationElementCollection
    {
        public RecentFilesConfigElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as RecentFilesConfigElement;
            }
        }

        public void AddNewRecentFile(string fileName)
        {
            int i = 0;
            while (i < Count)
            {
                if (((RecentFilesConfigElement)this[i]).FileName.Equals(fileName))
                {
                    BaseRemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            RecentFilesConfigElement element = CreateNewElement() as RecentFilesConfigElement;
            element.FileName = fileName;

            BaseAdd(0, element);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new RecentFilesConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((RecentFilesConfigElement)element).FileName;
        }
    }
}
