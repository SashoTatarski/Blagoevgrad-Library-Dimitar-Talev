using Autofac;
using Library.Core.Contracts;

namespace Library.Core
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IComponentContext _componentContext;

        public CommandProcessor(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public ICommand ParseCommand(string input)
        {
            return  _componentContext.ResolveNamed<ICommand>(input);
        }

        public string ProcessCommand(ICommand command)
        {
            return command.Execute();
        }
    }
}
