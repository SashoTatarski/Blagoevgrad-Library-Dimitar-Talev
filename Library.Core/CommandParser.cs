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
            this._componentContext = componentContext;
            this._accountManager = accountManager;
        }

        public ICommand ParseCommand(string input)
        {
            return _componentContext.ResolveNamed<ICommand>(input);
        }

        public ICommand GetTheCommandByNumber(int number)
        {
            if (_accountManager.CurrentAccount is null)
            {
                if (number == 1)
                {
                    return _componentContext.ResolveNamed<ICommand>("login");
                }
                else if (number == 2)
                {
                    throw new ArgumentException("Bye!");
                }
                else
                    throw new ArgumentException("Invalid input");
            }
            else
            {
                var commandAsList = (List<string>)_accountManager.CurrentAccount.AllowedCommands;

                if (number < 1 || number > commandAsList.Count)
                {
                    throw new ArgumentException("Invalid input");
                }

                var commandAsString = commandAsList[number - 1].Replace(" ", "").ToLower();

                return _componentContext.ResolveNamed<ICommand>(commandAsString);
            }
        }
    }
}
