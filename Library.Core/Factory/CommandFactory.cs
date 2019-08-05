using Autofac;
using Library.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Factory
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IComponentContext _componentContext;

        public CommandFactory(IComponentContext context)
        {
            _componentContext = context;
        }

        public ICommand Create(string commandName)
        {
            var command = _componentContext
                .ResolveNamed<ICommand>(commandName);

            return command;
        }
    }
}
