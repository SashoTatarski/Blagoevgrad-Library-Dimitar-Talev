using Library.Services.Contracts;
using System;

namespace Library.Services
{
    public class ConsoleRenderer : IConsoleRenderer
    {
        public string Input()
        {
            var currentLine = Console.ReadLine();

            return currentLine;
        }

        public void Output(string output)
        {
            Console.Write(output);
        }

        public string InputParameters(string parameterName, Func<string, bool> validator)
        {
            this.Output($"Enter {parameterName}: ");

            var input = this.Input();

            while (validator(input))
            {
                input = this.Input();
            }

            return input;
        }

        public string InputParameters(string parameterName)
        {
            this.Output($"Enter {parameterName}: ");
            return this.Input();
        }
    }
}
