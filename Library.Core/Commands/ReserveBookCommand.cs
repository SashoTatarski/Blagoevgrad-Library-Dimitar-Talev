using Library.Core.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class ReserveBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IBookManager _bookManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;
        private readonly ILibrarySystem _system;


        public ReserveBookCommand(IAuthenticationManager authentication, IBookManager bookManager, IConsoleRenderer renderer, IConsoleFormatter formatter, ILibrarySystem system)
        {
            _authentication = authentication;
            _bookManager = bookManager;
            _renderer = renderer;
            _formatter = formatter;
            _system = system;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ReserveBook, GlobalConstants.MiniDelimiterSymbol));
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ChooseBook, '.'));

            var user = (User)_authentication.CurrentAccount;

            // If the User has checked out 5 books already
            _system.CheckReservedBooksQuota(user);

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));
            // BookID validation
            DataValidator.ValidateNumberInList(bookId, _bookManager.GetBooksIDs());

            var bookToReserve = _bookManager.FindBook(bookID);

            _system.AddBookToReservedBooks(bookToReserve, user);

            return _formatter.FormatCommandMessage(GlobalConstants.ReservedBookSuccessMsg, _formatter.FormatReservedBook(bookToReserve));
        }
    }
}
