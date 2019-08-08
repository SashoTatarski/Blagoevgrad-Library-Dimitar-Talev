using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Library.Services.Factory;
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
        private readonly IBookManager _bookManager;

        public EditBookCommand(IDatabaseService database, IConsoleRenderer renderer, IAccountManager account, IBookFactory factory, IBookManager bookManager)
        {
            _service = database;
            _renderer = renderer;
            _account = account;
            _factory = factory;
            _bookManager = bookManager;
        }

        public string Execute()
        {
            var currentAccount = (ILibrarian)_account.CurrentAccount;

            var books = _service.ReadBooks();

            // Show all the books so user can select
            _service.ListAllBooks();

            var bookId = int.Parse(_renderer.InputParameters("ID"));
            if (bookId > books.Count || bookId < 1)
                throw new ArgumentException("Enter proper ID");

            var bookToEdit = books.Find(b => b.ID == bookId);
            
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

            _bookManager.UpdateBook(bookId, authorName, title, isbn, category, publisher, year, rack);

            var bookToRemove = books.Find(b => b.ID == bookId);
            _service.RemoveBook(bookToRemove);        

            var bookToCreate = _factory.CreateBook(bookToRemove.ID, authorName, title, isbn, category, publisher, year, rack);

            _service.AddBook(bookToCreate);

            return $"Successfully edited a book {bookToCreate.Title} - {bookToCreate.Author}";
        }
    }
}
