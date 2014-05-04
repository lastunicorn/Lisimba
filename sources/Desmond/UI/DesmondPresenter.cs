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
