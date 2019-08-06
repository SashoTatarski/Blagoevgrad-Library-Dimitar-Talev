using System;

namespace Library.Core.Contracts
{
    public interface IRenderer
    {
        string Input();
        void Output(string output);
        string InputParameters(string parameterName, Func<string, bool> validator);

        string InputParameters(string parameterName);
    }
}
