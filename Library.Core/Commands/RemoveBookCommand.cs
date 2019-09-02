using Library.Core.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class RemoveBookCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IConsoleFormatter _formatter;

        public RemoveBookCommand(IConsoleRenderer renderer, IBookManager bookManager, IConsoleFormatter formatter)
        {
            _renderer = renderer;
            _bookManager = bookManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.RemoveBook, GlobalConstants.MiniDelimiterSymbol));
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ChooseBook, '.'));

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            DataValidator.ValidateNumberInList(bookID, _bookManager.GetBooksIDs());

            var bookToRemove = _bookManager.FindBook(bookID);

            if (bookToRemove.Status == BookStatus.CheckedOut || bookToRemove.Status == BookStatus.Reserved)
                throw new ArgumentException(GlobalConstants.CannotRemoveIssuedBook);

            _bookManager.RemoveBook(bookToRemove);

            return _formatter.FormatCommandMessage(GlobalConstants.RemoveBookSuccess, _formatter.Format(bookToRemove));
        }
    }
}
