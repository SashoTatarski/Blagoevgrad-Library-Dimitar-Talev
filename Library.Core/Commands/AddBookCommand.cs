using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factory;
using System;

namespace Library.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IBookFactory _factory;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IConsoleFormatter _formatter;

        public AddBookCommand(IBookFactory factory, IConsoleRenderer renderer, IBookManager bookManager, IConsoleFormatter formatter)
        {
            _factory = factory;
            _renderer = renderer;
            _bookManager = bookManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.AddBook);

            var authorName = _renderer.InputParameters("author name",
                s => s.Length < 1 || s.Length > 40);

            var title = _renderer.InputParameters("title",
                s => s.Length < 1 || s.Length > 100);

            var isbn = _renderer.InputParameters("ISBN code");

            var category = _renderer.InputParameters("genre",
                g => g.Length < 1 || g.Length > 40);

            var publisher = _renderer.InputParameters("publisher",
                g => g.Length < 1 || g.Length > 40);

            var year = int.Parse(_renderer.InputParameters("year",
                y => int.Parse(y) < 1 || int.Parse(y) > DateTime.Now.Year));

            var rack = int.Parse(_renderer.InputParameters("rack",
                r => int.Parse(r) < 1));

            // Get the ID of the new book
            var bookID = _bookManager.GetLastBookID() + 1;

            // Create book with given parameters
            var bookToCreate = _factory.CreateBook(bookID, authorName, title, isbn, category, publisher, year, rack);

            // Add book to Database
            _bookManager.AddBook(bookToCreate);

            return _formatter.FormatCommandMessage(GlobalConstants.AddBookSuccess, _formatter.Format(bookToCreate));
        }
    }
}
