namespace Lisimba.Cmd.Common
{
    /// <summary>
    /// Represents a flow that is executed at the user's request
    /// </summary>
    internal interface IFlow
    {
        void Execute(Command command);
    }
}