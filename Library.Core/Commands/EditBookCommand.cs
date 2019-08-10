using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class EditBookCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IAuthenticationManager _account;
        private readonly IBookManager _bookManager;

        public EditBookCommand(IConsoleRenderer renderer, IAuthenticationManager account, IBookManager bookManager)
        {
            _renderer = renderer;
            _account = account;
            _bookManager = bookManager;
        }

        public string Execute()
        {
            // Show all the books user can select from
            _bookManager.ListAllBooks();

            var currentAccount = (ILibrarian)_account.CurrentAccount;

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException("Invalid ID");
            }           

            // TODO
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

            _bookManager.UpdateBook(bookID, authorName, title, isbn, category, publisher, year, rack);

            var bookToEdit = _bookManager.FindBook(bookID);

            return $"Successfully edited the book {bookToEdit.Title} - {bookToEdit.Author}";
        }
    }
}
