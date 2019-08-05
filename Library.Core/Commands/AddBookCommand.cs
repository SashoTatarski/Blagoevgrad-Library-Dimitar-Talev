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

        public string Execute(List<string> arguments)
        {

            var authorName = _renderer.InputParametersParse("author name");
            var title = _renderer.InputParametersParse("title");
            var isbn = _renderer.InputParametersParse("isbn");
            var category = _renderer.InputParametersParse("category");
            var publisher = _renderer.InputParametersParse("publisher");
            var year = int.Parse(_renderer.InputParametersParse("year"));
            var rack = int.Parse(_renderer.InputParametersParse("rack"));
            var bookStatus = BookStatus.Available;

            var bookToCreate = _factory.CreateBook(authorName, title, isbn, category, publisher, year, rack, bookStatus);

            _database.AddBookToList(bookToCreate);

            _database.WriteBooksToJson(_database.Books);

            return $"Successfully added a book";
        }
    }
}
