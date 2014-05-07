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

namespace DustInTheWind.Desmond
{
    internal class DesmondPresenter
    {
        private IDesmondView view;
        private RedEye redEye;

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DesmondPresenter"/> class.
        /// </summary>
        /// <param name="view">The view used by the presenter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public DesmondPresenter(IDesmondView view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            this.view = view;
            this.redEye = new RedEye();
        }

        #endregion

        #region public void StartClicked()

        public void StartClicked()
        {
            try
            {
                this.DisplayStarting();
                this.redEye.Start();
                this.DisplayStarted();
            }
            catch (Exception ex)
            {
                this.DisplayStopped();
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region public void StopClicked()

        public void StopClicked()
        {
            try
            {
                this.DisplayStopping();
                this.redEye.Stop();
                this.DisplayStopped();
            }
            catch (Exception ex)
            {
                this.DisplayStarted();
                this.view.DisplayError(ex);
            }
        }

        #endregion

        #region public void DisplayStarting()

        public void DisplayStarting()
        {
            this.view.ButtonStartEnabled = false;
            this.view.ButtonStopEnabled = false;
            this.view.LedState = LedState.Yellow;
            this.view.StatusText = "Starting...";
        }

        #endregion

        #region public void DisplayStarted()

        public void DisplayStarted()
        {
            this.view.ButtonStartEnabled = false;
            this.view.ButtonStopEnabled = true;
            this.view.LedState = LedState.Green;
            this.view.StatusText = "Started";
        }

        #endregion

        #region public void DisplayStopping()

        public void DisplayStopping()
        {
            this.view.ButtonStartEnabled = false;
            this.view.ButtonStopEnabled = false;
            this.view.LedState = LedState.Yellow;
            this.view.StatusText = "Stopping...";
        }

        #endregion

        #region public void DisplayStopped()

        public void DisplayStopped()
        {
            this.view.ButtonStartEnabled = true;
            this.view.ButtonStopEnabled = false;
            this.view.LedState = LedState.Red;
            this.view.StatusText = "Stopped";
        }

        #endregion
    }
}
