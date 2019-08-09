using Library.Core.Contracts;
using Library.Models.Enums;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class RemoveBookCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;

        public RemoveBookCommand(IConsoleRenderer renderer, IBookManager bookManager)
        {
            _renderer = renderer;
            _bookManager = bookManager;
        }

        public string Execute()
        {
            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));
            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException("Invalid ID");
            }

            var bookToRemove = _bookManager.FindBook(bookID);

            if (bookToRemove.Status== BookStatus.CheckedOut || bookToRemove.Status == BookStatus.Reserved)
            {
                throw new ArgumentException("Cannot remove chechedout/reserved book!");
            }

            _bookManager.RemoveBook(bookToRemove);

            return $"You have successfully removed the book: {bookToRemove.Title} - {bookToRemove.Author}";
        }
    }
}
