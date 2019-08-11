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

        public CheckOutBookCommand(IAuthenticationManager authentication,IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager)
        {
            _authentication = authentication;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
        }

        public string Execute()
        {
            var user = (IUser)_authentication.CurrentAccount;

            // If the User has checked out 5 books already
            if (user.CheckedOutBooks.Count == 5)
            {
                throw new ArgumentException("You have reached the max quota of 5 checkedout books!");
            }

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));
            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException("Invalid ID");
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
                    throw new ArgumentException("Book is already reserved!");
            }
            else
            {
                throw new ArgumentException("Book is already checked out!");
            }

            return $"You have successfully checked out {bookToCheckOut.Title} - {bookToCheckOut.Author}";
        }
    }
}
