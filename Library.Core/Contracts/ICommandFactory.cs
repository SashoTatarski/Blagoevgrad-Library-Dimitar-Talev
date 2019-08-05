using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Contracts
{
    public interface ICommandFactory
    {
        ICommand Create(string commandName);
    }
}
