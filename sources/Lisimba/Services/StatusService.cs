// Lisimba
// Copyright (C) 2014 Dust in the Wind
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
using System.Diagnostics;
using System.Threading;

namespace DustInTheWind.Lisimba.Services
{
    public class StatusService : IDisposable
    {
        private string statusText;
        private string defaultStatusText;
        private readonly Timer timer;
        private bool disposed;
        private Stopwatch stopwatch;

        public string StatusText
        {
            get { return statusText; }
            set
            {
                if (value == statusText)
                    return;

                Console.WriteLine(DateTime.Now + " - Set StatusText = " + value + " - " + ResetTimeout.ToString());
                stopwatch = Stopwatch.StartNew();

                statusText = value;
                timer.Change(ResetTimeout, new TimeSpan(-1));
                OnStatusTextChanged(EventArgs.Empty);
            }
        }

        public string DefaultStatusText
        {
            get { return defaultStatusText; }
            set
            {
                defaultStatusText = value;

                statusText = value;
                OnStatusTextChanged(EventArgs.Empty);
            }
        }

        public TimeSpan ResetTimeout { get; set; }

        public event EventHandler StatusTextChanged;

        protected virtual void OnStatusTextChanged(EventArgs e)
        {
            EventHandler handler = StatusTextChanged;

            if (handler != null)
                handler(this, e);
        }

        public StatusService()
        {
            timer = new Timer(HandleTimerElapsed);
            defaultStatusText = string.Empty;
            statusText = string.Empty;
            ResetTimeout = TimeSpan.FromSeconds(1);
        }

        private void HandleTimerElapsed(object state)
        {
            Console.WriteLine("asd");
            Console.WriteLine(stopwatch.ElapsedMilliseconds);

            statusText = defaultStatusText;
            OnStatusTextChanged(EventArgs.Empty);
        }

        public void Dispose()
        {
            if (disposed)
                return;

            timer.Dispose();

            disposed = true;
        }
    }
}