using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Contracts
{
    public interface IRenderer
    {
        string Input();
        void Output(string output);
    }
}
