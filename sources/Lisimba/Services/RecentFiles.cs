// Lisimba
// Copyright (C) 2007-2014 Dust in the Wind
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

using System;

namespace DustInTheWind.Lisimba.Services
{
    class RecentFiles
    {
        private readonly ConfigurationService configurationService;

        public event EventHandler<EventArgs> FileNameAdded;

        protected virtual void OnFileNameAdded(EventArgs e)
        {
            EventHandler<EventArgs> handler = FileNameAdded;

            if (handler != null)
                handler(this, e);
        }

        public RecentFiles(ConfigurationService configurationService)
        {
            if (configurationService == null)
                throw new ArgumentNullException("configurationService");

            this.configurationService = configurationService;
        }

        public string GetMostRecentFileName()
        {
            if (configurationService.LisimbaConfigSection.RecentFilesList.Count > 0)
                return configurationService.LisimbaConfigSection.RecentFilesList[0].FileName;

            return null;
        }

        public void AddRecentFile(string fileName)
        {
            configurationService.LisimbaConfigSection.RecentFilesList.AddNewRecentFile(fileName);

            OnFileNameAdded(EventArgs.Empty);

            configurationService.Save();
        }

        public Config.RecentFilesConfigElementCollection GetAllFiles()
        {
            return configurationService.LisimbaConfigSection.RecentFilesList;
        }
    }
}
