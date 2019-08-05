using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Contracts
{
    public interface ICommand
    {
        string Execute(List<string> arguments);
    }
}
