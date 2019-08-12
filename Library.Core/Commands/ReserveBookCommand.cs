using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Linq;

namespace Library.Core.Commands
{
    public class ReserveBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IBookManager _bookManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IConsoleFormatter _formatter;
        private readonly ILibrarySystem _system;

        public ReserveBookCommand(IAuthenticationManager authentication, IBookManager bookManager, IConsoleRenderer renderer, IAccountManager accounManager, IConsoleFormatter formatter, ILibrarySystem system)

        {
            _authentication = authentication;
            _bookManager = bookManager;
            _renderer = renderer;
            _accountManager = accounManager;
            _formatter = formatter;
            _system = system;
        }

        public string Execute()
        {
            var user = (IUser)_authentication.CurrentAccount;

            // check if user has reserved 5 books already
            _system.CheckIfMaxQuotaReached(user.ReservedBooks);

            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
                throw new ArgumentException("Invalid ID");

            var bookToReserve = _bookManager.FindBook(bookID);

            // Check Book Status 
            // if the status is Available
            if (bookToReserve.Status == BookStatus.Available)
            {
                user.AddBookToReservedBooks(bookToReserve);
                _bookManager.UpdateBook(bookID, BookStatus.Reserved, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(5));
                _accountManager.UpdateUser(user);

                return $"{GlobalConstants.ReservedBookSuccessMsg}{_formatter.Format(bookToReserve)}";
            }
            // if the book is already checked out
            else if (bookToReserve.Status == BookStatus.CheckedOut)
            {
                //after the other user return the book, this user will have 5 days to take it
                user.AddBookToReservedBooks(bookToReserve);
                _bookManager.UpdateBook(bookID, BookStatus.CheckedOut_and_Reserved, VirtualDate.VirtualToday, DateTime.MaxValue, true);
                _accountManager.UpdateUser(user);

                return $"{GlobalConstants.ReservedBookSuccessMsg}{_formatter.Format(bookToReserve)}";
            }
            else if (bookToReserve.Status == BookStatus.Reserved)
            {
                var suchBookInReservedBooks = user.ReservedBooks.FirstOrDefault(b => b.ID == bookToReserve.ID);

                if (suchBookInReservedBooks != null)
                    return GlobalConstants.ReservedBookAlreadyReserved;
                else
                    return GlobalConstants.ReservedBookAlreadyReservedOther;
            }
            else
            {
                return GlobalConstants.ReserveBookAlreadyCheckedout;
            }
        }
    }
}
