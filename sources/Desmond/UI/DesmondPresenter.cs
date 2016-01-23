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

namespace DustInTheWind.Desmond.UI
{
    internal class DesmondPresenter
    {
        private readonly IDesmondView view;
        private readonly RedEye redEye;

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
            redEye = new RedEye();
        }

        #endregion

        #region public void StartClicked()

        public void StartClicked()
        {
            try
            {
                DisplayStarting();
                redEye.Start();
                DisplayStarted();
            }
            catch (Exception ex)
            {
                DisplayStopped();
                view.DisplayError(ex);
            }
        }

        #endregion

        #region public void StopClicked()

        public void StopClicked()
        {
            try
            {
                DisplayStopping();
                redEye.Stop();
                DisplayStopped();
            }
            catch (Exception ex)
            {
                DisplayStarted();
                view.DisplayError(ex);
            }
        }

        #endregion

        #region public void DisplayStarting()

        public void DisplayStarting()
        {
            view.ButtonStartEnabled = false;
            view.ButtonStopEnabled = false;
            view.LedState = LedState.Yellow;
            view.StatusText = "Starting...";
        }

        #endregion

        #region public void DisplayStarted()

        public void DisplayStarted()
        {
            view.ButtonStartEnabled = false;
            view.ButtonStopEnabled = true;
            view.LedState = LedState.Green;
            view.StatusText = "Started";
        }

        #endregion

        #region public void DisplayStopping()

        public void DisplayStopping()
        {
            view.ButtonStartEnabled = false;
            view.ButtonStopEnabled = false;
            view.LedState = LedState.Yellow;
            view.StatusText = "Stopping...";
        }

        #endregion

        #region public void DisplayStopped()

        public void DisplayStopped()
        {
            view.ButtonStartEnabled = true;
            view.ButtonStopEnabled = false;
            view.LedState = LedState.Red;
            view.StatusText = "Stopped";
        }

        #endregion
    }
}