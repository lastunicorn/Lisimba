namespace Lisimba.Cmd.CommandSystem
{
    internal interface ICommand
    {
        void Execute(CommandInfo commandInfo);
    }
}