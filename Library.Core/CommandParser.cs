using Autofac;
using Library.Core.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Core
{
    public class CommandParser : ICommandParser
    {
        private readonly IComponentContext _componentContext;
        private readonly IAuthenticationManager _accountManager;

        public CommandParser(IComponentContext componentContext, IAuthenticationManager accountManager)
        {
            _componentContext = componentContext;
            _accountManager = accountManager;
        }

        public ICommand GetCommandByNumber(int number, List<string> commands)
        {
                if (number < 1 || number > commands.Count)
                {
                    throw new ArgumentException("Invalid input");
                }

                var commandAsString = commands[number - 1].Replace(" ", "").ToLower();

                return _componentContext.ResolveNamed<ICommand>(commandAsString);
        }
    }
}
