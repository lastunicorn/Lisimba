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
using System.Windows.Forms;
using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    class CustomMenuItem : ToolStripMenuItem
    {
        private ICommand command;
        public StatusService StatusService { get; set; }

        public string ShortDescription { get; set; }

        public ICommand Command
        {
            get { return command; }
            set
            {
                if (command != null)
                    command.IsEnabledChanged -= HandleCommandEnabledChanged;

                command = value;

                if (command != null)
                    command.IsEnabledChanged += HandleCommandEnabledChanged;

                Enabled = command == null || command.IsEnabled;
            }
        }

        private void HandleCommandEnabledChanged(object sender, EventArgs eventArgs)
        {
            Enabled = command.IsEnabled;
        }

        public object CommandParameter { get; set; }

        public Func<object> CommandParameterProvider { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (StatusService != null)
            {
                string description = CalculateTextToDisplayAsStatus();
                StatusService.SetPermanentStatusText(description);
            }

            base.OnMouseEnter(e);
        }

        private string CalculateTextToDisplayAsStatus()
        {
            if (ShortDescription != null)
                return ShortDescription;

            if (Command != null && Command.ShortDescription != null)
                return Command.ShortDescription;

            return null;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (StatusService != null)
                StatusService.Reset();

            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (Command != null)
            {
                object commandParameter = CalculateParameterToUseWithCommand();

                if (commandParameter == null)
                    Command.Execute();
                else
                    Command.Execute(commandParameter);
            }

            base.OnClick(e);
        }

        private object CalculateParameterToUseWithCommand()
        {
            return CommandParameterProvider != null
                ? CommandParameterProvider()
                : CommandParameter;
        }
    }
}
