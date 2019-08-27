using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class EditBookCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IConsoleFormatter _formatter;

        public EditBookCommand(IConsoleRenderer renderer, IBookManager bookManager, IConsoleFormatter formatter)
        {
            _renderer = renderer;
            _bookManager = bookManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.EditBook);

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookId = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            //if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            //    throw new ArgumentException(GlobalConstants.InvalidID);

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

            var bookToEdit = _bookManager.FindBook(bookId);

            //return _formatter.FormatCommandMessage(GlobalConstants.EditBookSuccess, _formatter.Format(bookToEdit));
            return null;
        }
    }
}
