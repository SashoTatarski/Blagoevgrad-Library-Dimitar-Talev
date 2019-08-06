using Library.Core.Contracts;
using System;

namespace Library.Core
{
    public class ConsoleRenderer : IRenderer
    {
        public string Input()
        {
            var currentLine = Console.ReadLine();

            return currentLine;
        }

        public void Output(string output)
        {
            Console.WriteLine(output);
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
