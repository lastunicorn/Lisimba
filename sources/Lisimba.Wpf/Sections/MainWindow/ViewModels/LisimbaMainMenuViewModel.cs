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

using System;
using System.Collections.Generic;
using System.Linq;
using DustInTheWind.Lisimba.Business.GateManagement;
using DustInTheWind.Lisimba.Wpf.Commands;
using DustInTheWind.Lisimba.Wpf.Operations;

namespace DustInTheWind.Lisimba.Wpf.Sections.MainWindow.ViewModels
{
    internal class LisimbaMainMenuViewModel
    {
        public ApplicationExitCommand ApplicationExitCommand { get; private set; }
        public NewAddressBookCommand NewAddressBookCommand { get; private set; }
        public OpenAddressBookCommand OpenAddressBookCommand { get; private set; }
        public List<CustomMenuItem> OpenFromMenuItems { get; private set; }
        public SaveAddressBookCommand SaveAddressBookCommand { get; private set; }
        public SaveAsAddressBookCommand SaveAsAddressBookCommand { get; private set; }
        public CloseAddressBookCommand CloseAddressBookCommand { get; private set; }
        public ShowAddressBookPropertiesCommand ShowAddressBookPropertiesCommand { get; private set; }
        public ShowAboutCommand ShowAboutCommand { get; private set; }
        public NewContactCommand NewContactCommand { get; private set; }
        public DeleteCurrentContactCommand DeleteCurrentContactCommand { get; private set; }
        public UndoCommand UndoCommand { get; private set; }
        public RedoCommand RedoCommand { get; private set; }

        public List<CustomMenuItem> ExportMenuItems { get; private set; }
        public ImportCommand ImportCommand { get; private set; }

        public LisimbaMainMenuViewModel(AvailableCommands availableCommands, AvailableGates availableGates)
        {
            if (availableCommands == null) throw new ArgumentNullException("availableCommands");
            if (availableGates == null) throw new ArgumentNullException("availableGates");

            ApplicationExitCommand = availableCommands.GetCommand<ApplicationExitCommand>();
            NewAddressBookCommand = availableCommands.GetCommand<NewAddressBookCommand>();
            OpenAddressBookCommand = availableCommands.GetCommand<OpenAddressBookCommand>();
            SaveAddressBookCommand = availableCommands.GetCommand<SaveAddressBookCommand>();
            SaveAsAddressBookCommand = availableCommands.GetCommand<SaveAsAddressBookCommand>();
            CloseAddressBookCommand = availableCommands.GetCommand<CloseAddressBookCommand>();
            ImportCommand = availableCommands.GetCommand<ImportCommand>();
            ShowAddressBookPropertiesCommand = availableCommands.GetCommand<ShowAddressBookPropertiesCommand>();
            ShowAboutCommand = availableCommands.GetCommand<ShowAboutCommand>();
            NewContactCommand = availableCommands.GetCommand<NewContactCommand>();
            DeleteCurrentContactCommand = availableCommands.GetCommand<DeleteCurrentContactCommand>();
            UndoCommand = availableCommands.GetCommand<UndoCommand>();
            RedoCommand = availableCommands.GetCommand<RedoCommand>();

            ExportMenuItems = availableGates
                .Select(x => new CustomMenuItem
                {
                    Text = x.Name,
                    Command = availableCommands.GetCommand<ExportAddressBookCommand>(),
                    Icon = x.Icon16 != null ? x.Icon16.ToBitmapSource() : null,
                    Gate = x
                })
                .ToList();

            OpenFromMenuItems = availableGates
                .Select(x => new CustomMenuItem
                {
                    Text = x.Name,
                    Command = availableCommands.GetCommand<OpenFromCommand>(),
                    Icon = x.Icon16 != null ? x.Icon16.ToBitmapSource() : null,
                    Gate = x
                })
                .ToList();
        }
    }
}