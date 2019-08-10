namespace Library.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand GetTheCommandByNumber(int number);
    }
}
