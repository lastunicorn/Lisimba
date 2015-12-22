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
using System.Threading;

namespace DustInTheWind.Desmond
{
    internal class TaskRunner
    {
        /// <summary>
        /// Time in miliseconds to wait for the working thread to stop by itself before abort it.
        /// </summary>
        private const int THREAD_STOP_TIMEOUT = 1000;

        /// <summary>
        /// Flag used to announce the working thread that it is requested to close itself.
        /// </summary>
        protected volatile bool stopRequested = false;

        /// <summary>
        /// The background thread that is executing the task.
        /// </summary>
        private Thread workingThread;

        /// <summary>
        /// Object used to lock the access of the state field.
        /// </summary>
        private readonly object lockState = new object();

        /// <summary>
        /// The state of the current instance.
        /// </summary>
        private volatile RunnerState state = RunnerState.Stopped;

        /// <summary>
        /// Gets the state of the current instance.
        /// </summary>
        public RunnerState State
        {
            get { return state; }
        }

        /// <summary>
        /// The last error thrown by the working thread.
        /// </summary>
        private volatile Exception error;

        /// <summary>
        /// The task that has to be run.
        /// </summary>
        protected TaskMethod task;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRunner"/> class.
        /// </summary>
        protected TaskRunner()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskRunner"/> class with
        /// the task that has to be run.
        /// </summary>
        /// <param name="task">The task that has to be run.</param>
        public TaskRunner(TaskMethod task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            this.task = task;
        }

        /// <summary>
        /// Starts the working thread.
        /// </summary>
        public void Start()
        {
            lock (lockState)
            {
                if (task == null)
                    throw new Exception("Cannot start because the task was not set yet.");

                if (state == RunnerState.Stopped)
                {
                    stopRequested = false;
                    error = null;

                    workingThread = new Thread(new ThreadStart(Run));
                    workingThread.Start();

                    state = RunnerState.Running;
                }
                else
                {
                    throw new Exception("Cannot start because the state is different then Stopped.");
                }
            }
        }

        /// <summary>
        /// Stops the working thread.
        /// </summary>
        public void Stop()
        {
            lock (lockState)
            {
                if (state != RunnerState.Stopped)
                {
                    stopRequested = true;
                    if (workingThread != null && workingThread.IsAlive && !workingThread.Join(THREAD_STOP_TIMEOUT))
                    {
                        workingThread.Abort();
                    }

                    state = RunnerState.Stopped;
                }
            }
        }

        /// <summary>
        /// The method run by the working thread,
        /// </summary>
        private void Run()
        {
            try
            {
                while (!stopRequested)
                {
                    task();
                }
            }
            catch (Exception ex)
            {
                error = ex;
                throw;
            }
        }
    }
}