using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Database;
using Library.Models.Enums;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class AddBookCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IBookFactory _factory;
        private readonly IConsoleRenderer _renderer;

        public AddBookCommand(IDatabaseService database, IBookFactory factory, IConsoleRenderer renderer)
        {
            _service = database;
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

            var year = int.Parse(_renderer.InputParameters("year", y => int.Parse(y) < 1 || int.Parse(y) > DateTime.Now.Year));

            var rack = int.Parse(_renderer.InputParameters("rack", r => int.Parse(r) < 1));

            // Read books ids
            var currentId = _service.ReadBooks().Max(x => x.ID);
            var bookToCreate = _factory.CreateBook(++currentId, authorName, title, isbn, category, publisher, year, rack);

            _service.AddBook(bookToCreate);      

            return $"Successfully added a book {bookToCreate.Title} - {bookToCreate.Author}";
        }        
    }
}
