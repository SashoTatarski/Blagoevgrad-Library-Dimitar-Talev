using Library.Core.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System.Linq;

namespace Library.Core.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly ILibrarySystem _system;
        private readonly IConsoleFormatter _formatter;

        public ReturnBookCommand(IAuthenticationManager account, IConsoleRenderer renderer, IBookManager bookManager, ILibrarySystem system, IConsoleFormatter formatter)
        {
            _authentication = account;
            _renderer = renderer;
            _bookManager = bookManager;
            _system = system;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ReturnBook, GlobalConstants.MiniDelimiterSymbol));
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ChooseBook, '.'));

            var user = (User)_authentication.CurrentAccount;

            // Show all the books user has checked out
            var books = _system.GetCheckedOutBooks(user);
            _renderer.Output(_formatter.FormatList(books));

            // BookID Input
            var bookId = int.Parse(_renderer.InputParameters("ID"));
            // BookID validation
            DataValidator.ValidateNumberInList(bookId, books.Select(b => b.BookId).ToList());

            var bookToReturn = _bookManager.FindBook(bookId);

            _system.RemoveBookFromCheckoutBooks(bookToReturn);

            return _formatter.FormatCommandMessage(GlobalConstants.ReturnBookSuccessMsg, _formatter.Format(bookToReturn));
        }
    }
}
