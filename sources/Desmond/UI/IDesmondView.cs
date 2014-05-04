using System;
using System.Collections.Generic;
using System.Text;

namespace DustInTheWind.Desmond
{
    internal interface IDesmondView
    {
        /// <summary>
        /// Displays the exception in a frendlly way for the user.
        /// </summary>
        /// <param name="ex">The <see cref="Exception"/> instance containing data about the error.</param>
        void DisplayError(Exception ex);

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="ex">The message text to be displayed.</param>
        void DisplayErrorMessage(string message);

        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message text to be displayed.</param>
        void DisplayMessage(string message);

        bool ButtonStartEnabled { set; }
        bool ButtonStopEnabled { set; }
        string StatusText { set; }
        LedState LedState { set; }
    }
}
