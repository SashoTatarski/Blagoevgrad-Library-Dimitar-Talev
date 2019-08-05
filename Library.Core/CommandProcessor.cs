using Library.Core.Contracts;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core
{
    public class CommandProcessor : ICommandProcessor
    {
        private const char SplitCommandSymbol = ',';

        private readonly ICommandFactory _commandFactory;
        private string _name;
        private List<string> _parameters;

        public CommandProcessor(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
        }

        public string Name
        {
            get => _name;

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(GlobalConstants.NullOrEmptyNameErrorMessage);
                }

                _name = value;
            }
        }

        public List<string> Parameters
        {
            get => new List<string>(_parameters);

            private set
            {
                if (value == null)
                    throw new ArgumentNullException(GlobalConstants.NullCollectionOfParameters);

                _parameters = value;
            }
        }

        public string ProcessCommands(string input)
        {
            this.TranslateInput(input);
            var command = _commandFactory.Create(this.Name);
            var commandResult = command.Execute(Parameters);

            return commandResult;
        }

        private void TranslateInput(string input)
        {
            var indexOfFirstSeparator = input.IndexOf(SplitCommandSymbol);

            this.Name = input.Substring(0, indexOfFirstSeparator);
            this.Parameters = input.Substring(indexOfFirstSeparator + 1).Split(new[] { SplitCommandSymbol }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

    }
}
