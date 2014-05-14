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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DustInTheWind.Desmond.UI
{
    /// <summary>
    /// <para>
    /// The Windows form that instanciates the <see cref="RedEye"/> class.
    /// </para>
    /// <para>
    /// The application may be started as a desktop application or it can be installed as a Windows service.
    /// This is the interface of the desktop application.
    /// </para>
    /// </summary>
    internal partial class DesmondForm : Form, IDesmondView
    {
        private DesmondPresenter presenter;


        #region Constructor

        /// <summary>
        /// Creates a new instance of the DesmondForm class.
        /// </summary>
        public DesmondForm()
        {
            InitializeComponent();

            presenter = new DesmondPresenter(this);
        }

        #endregion


        #region public void DisplayError(Exception ex)

        private delegate void DisplayErrorDelegate(Exception ex);

        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        public void DisplayError(Exception ex)
        {
            if (InvokeRequired)
            {
                Invoke(new DisplayErrorDelegate(DisplayError), new object[] { ex });
            }
            else
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region private void DisplayErrorMessage(string message)

        public delegate void DisplayErrorMessageDelegate(string message);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="ex">The message text to be displayed.</param>
        public void DisplayErrorMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new DisplayErrorMessageDelegate(DisplayErrorMessage), new object[] { message });
            }
            else
            {
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region private void DisplayMessage(string message)

        public delegate void DisplayMessageDelegate(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        public void DisplayMessage(string message)
        {
            if (InvokeRequired)
            {
                Invoke(new DisplayMessageDelegate(DisplayMessage), new object[] { message });
            }
            else
            {
                MessageBox.Show(this, message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region private void buttonStart_Click(object sender, EventArgs e)

        private void buttonStart_Click(object sender, EventArgs e)
        {
            presenter.StartClicked();
        }

        #endregion

        #region private void buttonStop_Click(object sender, EventArgs e)

        private void buttonStop_Click(object sender, EventArgs e)
        {
            presenter.StopClicked();
        }

        #endregion

        public bool ButtonStartEnabled
        {
            set { buttonStart.Enabled = value; }
        }

        public bool ButtonStopEnabled
        {
            set { buttonStop.Enabled = value; }
        }

        public string StatusText
        {
            set { toolStripStatusLabelStartedInfo.Text = value; }
        }

        public LedState LedState
        {
            set
            {
                Color color;
                switch (value)
                {
                    case LedState.Red:
                        color = Color.Red;
                        break;
                    case LedState.Yellow:
                        color = Color.Red;
                        break;
                    case LedState.Green:
                        color = Color.Red;
                        break;
                    default:
                        color = Color.Gray;
                        break;
                }
                toolStripStatusLabelStartedLed.BackColor = color;
            }
        }

        #region protected override void OnClosing(CancelEventArgs e)

        protected override void OnClosing(CancelEventArgs e)
        {
            presenter.StopClicked();
            base.OnClosing(e);
        }

        #endregion
    }
}