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
using System.Collections.Generic;
using DustInTheWind.Lisimba.Common;

namespace DustInTheWind.Lisimba.Cmd.Business
{
    class UserInterface
    {
        private readonly Prompter prompter;
        private readonly ObserverFactory observerFactory;
        private readonly Welcomer welcomer;

        private List<IObserver> observers;

        public UserInterface(Prompter prompter, ObserverFactory observerFactory, Welcomer welcomer)
        {
            if (prompter == null) throw new ArgumentNullException("prompter");
            if (observerFactory == null) throw new ArgumentNullException("observerFactory");
            if (welcomer == null) throw new ArgumentNullException("welcomer");

            this.prompter = prompter;
            this.observerFactory = observerFactory;
            this.welcomer = welcomer;
        }

        public void Initialize()
        {
            welcomer.SayWelcome();

            if (observers == null)
                observers = observerFactory.CreateObservers();

            foreach (IObserver observer in observers)
                observer.Start();
        }

        /// <summary>
        /// Starts to process the user input.
        /// </summary>
        public void Start()
        {
            prompter.Run();
        }

        /// <summary>
        /// Stops processing the user input.
        /// </summary>
        public void Stop()
        {
            prompter.Stop();

            if (observers != null)
            {
                foreach (IObserver observer in observers)
                    observer.Stop();
            }

            welcomer.SayGoodBye();
        }
    }
}