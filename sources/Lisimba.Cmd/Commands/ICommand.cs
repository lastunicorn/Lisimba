namespace Lisimba.Cmd.Commands
{
    internal interface ICommand
    {
        void Execute(CommandInfo commandInfo);
    }
}