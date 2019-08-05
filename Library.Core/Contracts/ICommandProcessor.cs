namespace Library.Core.Contracts
{
    public interface ICommandProcessor
    {
        string ProcessCommand(ICommand command);

        ICommand ParseCommand(string commandAsString);
    }
}
