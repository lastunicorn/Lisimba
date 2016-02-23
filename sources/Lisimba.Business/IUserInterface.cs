namespace DustInTheWind.Lisimba.Business
{
    public interface IUserInterface
    {
        void Initialize();

        /// <summary>
        /// Starts to process the user input.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops processing the user input.
        /// </summary>
        void Exit();
    }
}