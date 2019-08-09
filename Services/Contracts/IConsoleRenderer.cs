using System;

namespace Library.Services.Contracts
{
    public interface IConsoleRenderer
    {
        string Input();
        void Output(string output);
        string InputParameters(string parameterName, Func<string, bool> validator);

        string InputParameters(string parameterName);
    }
}
