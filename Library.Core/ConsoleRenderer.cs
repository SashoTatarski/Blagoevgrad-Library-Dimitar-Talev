using Library.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
