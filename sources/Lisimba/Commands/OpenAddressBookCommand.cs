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
using System.Linq;
using System.Text;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.Commands
{
    class OpenAddressBookCommand : CommandBase<string>
    {
        private readonly CurrentData currentData;
        private readonly UIService uiService;

        public OpenAddressBookCommand(CurrentData currentData, UIService uiService)
        {
            if (currentData == null)
                throw new ArgumentNullException("currentData");

            if (uiService == null)
                throw new ArgumentNullException("uiService");

            this.currentData = currentData;
            this.uiService = uiService;
        }

        protected override void DoExecute(string fileName)
        {
            try
            {
                currentData.Open(fileName);

                if (currentData.Warnings.Any())
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (Exception warning in currentData.Warnings)
                    {
                        sb.AppendLine(warning.Message);
                        sb.AppendLine();
                    }

                    uiService.DisplayWarning(sb.ToString());
                }
            }
            catch (Exception ex)
            {
                uiService.DisplayError(ex.Message);
            }
        }
    }
}
