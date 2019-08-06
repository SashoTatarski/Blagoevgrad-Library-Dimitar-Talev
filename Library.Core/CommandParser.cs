using Autofac;
using Library.Core.Contracts;

namespace Library.Core
{
    public class CommandParser : ICommandParser
    {
        private readonly IComponentContext _componentContext;

        public CommandParser(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public ICommand ParseCommand(string input)
        {
            return  _componentContext.ResolveNamed<ICommand>(input);
        }
    }
}
