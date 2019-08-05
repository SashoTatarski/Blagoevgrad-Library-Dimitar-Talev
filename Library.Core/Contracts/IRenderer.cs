using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Contracts
{
    public interface IRenderer
    {
        string Input();
        string InputParametersParse(string parameterName);
        void Output(string output);
    }
}
