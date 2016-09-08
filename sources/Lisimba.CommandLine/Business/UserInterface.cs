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
using DustInTheWind.ConsoleCommon.ConsoleCommandHandling;
using DustInTheWind.Lisimba.Business;
using DustInTheWind.Lisimba.Business.WorkerModel;

namespace DustInTheWind.Lisimba.CommandLine.Business
{
    internal class UserInterface : IUserInterface
    {
        private readonly Prompter prompter;
        private readonly Lisimba.Business.WorkerModel.Workers workers;
        private readonly Welcomer welcomer;

        public UserInterface(Prompter prompter, Lisimba.Business.WorkerModel.Workers workers, Welcomer welcomer)
        {
            if (prompter == null) throw new ArgumentNullException("prompter");
            if (workers == null) throw new ArgumentNullException("workers");
            if (welcomer == null) throw new ArgumentNullException("welcomer");

            this.prompter = prompter;
            this.workers = workers;
            this.welcomer = welcomer;
        }

        public void Initialize()
        {
            welcomer.SayWelcome();

            workers.Start();
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
        public void Exit()
        {
            prompter.Stop();
            workers.Stop();

            welcomer.SayGoodBye();
        }
    }
}