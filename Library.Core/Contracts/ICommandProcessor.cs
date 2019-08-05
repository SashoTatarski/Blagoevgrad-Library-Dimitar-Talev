using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Contracts
{
    public interface ICommandProcessor
    {
        string Name { get; }

        List<string> Parameters { get; }

        string ProcessCommands(string input);
    }
}
