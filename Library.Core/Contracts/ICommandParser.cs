using System.Collections.Generic;

namespace Library.Core.Contracts
{
    public interface ICommandParser
    {
        ICommand GetCommandByNumber(int number, List<string> commandsAsString);
    }
}
