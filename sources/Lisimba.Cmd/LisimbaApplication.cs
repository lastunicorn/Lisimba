// Lisimba
// Copyright (C) 2007-2015 Dust in the Wind
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
using DustInTheWind.Lisimba.Cmd.Business;

namespace DustInTheWind.Lisimba.Cmd
{
    internal class LisimbaApplication
    {
        private readonly Prompter prompter;
        private readonly ActiveObservers activeObservers;

        public event EventHandler Started;
        public event EventHandler Ended;

        public LisimbaApplication(Prompter prompter, ActiveObservers activeObservers)
        {
            if (prompter == null) throw new ArgumentNullException("prompter");
            if (activeObservers == null) throw new ArgumentNullException("activeObservers");

            this.prompter = prompter;
            this.activeObservers = activeObservers;
        }

        public void Run()
        {
            activeObservers.Start();

            OnStarted();

            prompter.Run();

            OnEnded();
        }

        public void Exit()
        {
            prompter.Stop();
        }

        protected virtual void OnStarted()
        {
            EventHandler handler = Started;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        protected virtual void OnEnded()
        {
            EventHandler handler = Ended;

            if (handler != null)
                handler(this, EventArgs.Empty);
        }
    }
}