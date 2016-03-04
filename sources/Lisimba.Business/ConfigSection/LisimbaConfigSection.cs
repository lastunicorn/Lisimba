// Lisimba
// Copyright (C) 2007-2016 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System.Configuration;

namespace DustInTheWind.Lisimba.Business.ConfigSection
{
    public class LisimbaConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("recentFiles")]
        public RecentFilesConfigElementCollection RecentFilesList
        {
            get { return (RecentFilesConfigElementCollection)this["recentFiles"] ?? new RecentFilesConfigElementCollection(); }
        }

        [ConfigurationProperty("loadFileAtStart")]
        public LoadFileAtStartConfigElement LoadFileAtStart
        {
            get { return (LoadFileAtStartConfigElement)this["loadFileAtStart"] ?? new LoadFileAtStartConfigElement(); }
        }

        [ConfigurationProperty("sortBy")]
        public SortByConfigElement SortBy
        {
            get { return (SortByConfigElement)this["sortBy"] ?? new SortByConfigElement(); }
        }

        [ConfigurationProperty("gates")]
        public GatesConfigElement Gates
        {
            get { return (GatesConfigElement)this["gates"] ?? new GatesConfigElement(); }
        }

        [ConfigurationProperty("startInTray", IsRequired = false)]
        public bool StartInTray
        {
            get { return (bool?)this["startInTray"] ?? false; }
        }
    }
}