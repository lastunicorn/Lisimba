﻿// Lisimba
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
using DustInTheWind.Lisimba.Commands;
using DustInTheWind.Lisimba.Services;

namespace DustInTheWind.Lisimba.UserControls
{
    class CustomMenuItem : System.Windows.Forms.ToolStripMenuItem
    {
        public StatusService StatusService { get; set; }

        public string ShortDescription { get; set; }

        public ICommand Command { get; set; }

        public object CommandParameter { get; set; }

        public Func<object> CommandParameterProvider { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (StatusService != null)
                StatusService.SetPermanentStatusText(ShortDescription);

            base.OnMouseEnter(e);
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
                object commandParameter = CommandParameterProvider != null
                    ? CommandParameterProvider()
                    : CommandParameter;

                if (commandParameter == null)
                    Command.Execute();
                else
                    Command.Execute(commandParameter);
            }

            base.OnClick(e);
        }
    }
}
