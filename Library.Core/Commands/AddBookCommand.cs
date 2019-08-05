using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Database;
using Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IDatabase _database;
        private readonly IBookFactory _factory;
        private readonly IRenderer _renderer;

        public AddBookCommand(IDatabase database, IBookFactory factory, IRenderer renderer)
        {
            _database = database;
            _factory = factory;
            _renderer = renderer;
        }

        public string Execute()
        {

            var authorName = _renderer.InputParameters("author name");
            var title = _renderer.InputParameters("title");
            var isbn = _renderer.InputParameters("ISBN code");
            var category = _renderer.InputParameters("category");
            var publisher = _renderer.InputParameters("publisher");
            var year = int.Parse(_renderer.InputParameters("year"));
            var rack = int.Parse(_renderer.InputParameters("rack"));

            var bookToCreate = _factory.CreateBook(authorName, title, isbn, category, publisher, year, rack);

            _database.AddBookToList(bookToCreate);

            _database.WriteBooksToJson(_database.Books);

            return $"Successfully added a book with ID {bookToCreate.ID}";
        }
    }
}
