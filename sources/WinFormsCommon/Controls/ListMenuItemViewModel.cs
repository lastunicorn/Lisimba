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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using DustInTheWind.WinFormsCommon.Operations;

namespace DustInTheWind.WinFormsCommon.Controls
{
    public class ListMenuItemViewModel : CustomButtonViewModel
    {
        public ObservableCollection<CustomButtonViewModel> Items { get; private set; }

        private IOperation childrenOpertion;

        public IOperation ChildrenOpertion
        {
            get { return childrenOpertion; }
            set
            {
                childrenOpertion = value;
                RepopulateItems();
            }
        }

        public ListMenuItemViewModel(ApplicationStatus applicationStatus, IWindowSystem windowSystem, IOperation operation)
            : base(applicationStatus, windowSystem, operation)
        {
            Items = new ObservableCollection<CustomButtonViewModel>();
        }

        protected void RepopulateItems()
        {
            Items.Clear();

            if (ChildrenOpertion == null)
                return;

            foreach (CustomButtonViewModel item in GetItems())
                Items.Add(item);
        }

        protected virtual IEnumerable<CustomButtonViewModel> GetItems()
        {
            yield break;
        }
    }
}