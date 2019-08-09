using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Services.Contracts;
using Library.Services.Factory;
using System;
using System.Linq;

namespace Library.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IBookFactory _factory;
        private readonly IConsoleRenderer _renderer;

        public AddBookCommand(IDatabaseService service, IBookFactory factory, IConsoleRenderer renderer)
        {
            _service = service;
            _factory = factory;
            _renderer = renderer;
        }

        public string Execute()
        {
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
            var bookID = _service.ReadBooks().Max(x => x.ID) + 1;

            var bookToCreate = _factory.CreateBook(bookID, authorName, title, isbn, category, publisher, year, rack);

            _service.AddBook(bookToCreate);

            return $"Successfully added the book {bookToCreate.Title} - {bookToCreate.Author}";
        }
    }
}
