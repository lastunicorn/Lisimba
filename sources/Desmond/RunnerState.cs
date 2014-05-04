namespace DustInTheWind.Desmond
{
    /// <summary>
    /// The state of a <see cref="Checker"/> instance.
    /// </summary>
    internal enum RunnerState
    {
        /// <summary>
        /// The <see cref="Checker"/> is stopped.
        /// </summary>
        Stopped,

        /// <summary>
        /// The <see cref="Checker"/> is in the process of starting.
        /// </summary>
        Starting,

        /// <summary>
        /// The <see cref="Checker"/> is running.
        /// </summary>
        Running,

        /// <summary>
        /// The <see cref="Checker"/> is in the process of stopping.
        /// </summary>
        Stopping
    }
}
