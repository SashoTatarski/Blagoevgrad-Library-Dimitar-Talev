using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class EditBookCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _account;
        private readonly IBookFactory _factory;

        public EditBookCommand(IDatabaseService database, IConsoleRenderer renderer, IAccountManager account, IBookFactory factory)
        {
            _service = database;
            _renderer = renderer;
            _account = account;
            _factory = factory;
        }

        public string Execute()
        {
            var currentAccount = (ILibrarian)_account.CurrentAccount;

            var books = _service.ReadBooks();

            // Show all the books so user can select
            _service.ListAllBooks();

            var input = int.Parse(_renderer.InputParameters("ID"));
            if (input > books.Count || input < 1)
                throw new ArgumentException("Enter proper ID");

            var bookToEdit = books.Find(b => b.ID == input);

            // TODO: ask how to optimize all that shit
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

            var bookToRemove = books.Find(b => b.ID == input);
            _service.RemoveBook(bookToRemove);        

            var bookToCreate = _factory.CreateBook(bookToRemove.ID, authorName, title, isbn, category, publisher, year, rack);
            _service.AddBook(bookToCreate);

            return $"Successfully edited a book {bookToCreate.Title} - {bookToCreate.Author}";
        }
    }
}
