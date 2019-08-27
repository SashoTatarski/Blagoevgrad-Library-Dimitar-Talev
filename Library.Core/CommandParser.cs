using Autofac;
using Library.Core.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Core
{
    // SOLID: Single Responsibility
    public class CommandParser : ICommandParser
    {
        private readonly IComponentContext _componentContext;

        public CommandParser(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public ICommand GetCommandByNumber(int number, List<string> commands)
        {
            if (number < 1 || number > commands.Count)
                throw new ArgumentException("Invalid input");

            var commandAsString = commands[number - 1].Replace(" ", "").ToLower();

            return _componentContext.ResolveNamed<ICommand>(commandAsString);
        }
    }
}
