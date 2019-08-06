namespace Library.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand ParseCommand(string commandAsString);
    }
}
