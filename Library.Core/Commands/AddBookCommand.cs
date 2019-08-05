using Library.Core.Contracts;
using Library.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IDatabase _database;

        public AddBookCommand(IDatabase database)
        {
            _database = database;
        }

        public string Execute(List<string> arguments)
        {
            return null;
        }
    }
}
