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
    public class CheckOutBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;
        private readonly IConsoleFormatter _formatter;

        public CheckOutBookCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager, ILibrarySystem system,IConsoleFormatter formatter)
        {
            _authentication = authentication;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
            _system = system;
            _formatter = formatter;
        }

        public string Execute()
        {
            var user = (IUser)_authentication.CurrentAccount;

            // If the User has checked out 5 books already
            _system.CheckIfMaxQuotaReached(user.CheckedOutBooks);

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));
            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException(GlobalConstants.CheckoutBookInvalidID);
            }

            var bookToCheckOut = _bookManager.FindBook(bookID);

            // Check Book Status 
            if (bookToCheckOut.Status == BookStatus.Available)
            {
                user.AddBookToCheckoutBooks(bookToCheckOut);
                _bookManager.UpdateBook(bookID, BookStatus.CheckedOut, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(10));
                _accountManager.UpdateUser(user);
            }
            else if (bookToCheckOut.Status == BookStatus.Reserved)
            {
                var suchBookInReservedBooks = user.ReservedBooks.FirstOrDefault(b => b.ID == bookToCheckOut.ID);

                if (suchBookInReservedBooks != null)
                {
                    user.AddBookToCheckoutBooks(bookToCheckOut);
                    user.RemoveFromReservedBooks(suchBookInReservedBooks);
                    _bookManager.UpdateBook(bookID, BookStatus.CheckedOut, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(10));
                    _accountManager.UpdateUser(user);
                }
                else
                    throw new ArgumentException(GlobalConstants.CheckoutBookAlreadyRes);
            }
            else
            {
                throw new ArgumentException();
            }

            return _formatter.Format(bookToCheckOut);
        }
    }
}
