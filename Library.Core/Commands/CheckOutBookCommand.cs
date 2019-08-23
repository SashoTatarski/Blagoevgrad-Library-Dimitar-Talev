using Library.Core.Contracts;
using Library.Database;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Linq;

namespace Library.Core.Commands
{
    // SOLID: DI principle - we program against Interfaces. High-level modules, which provide complex logic, should be easily reusable and unaffected by changes in low-level modules
    public class CheckOutBookCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;
        private readonly IConsoleFormatter _formatter;
        private readonly LibraryContext _context;

        public CheckOutBookCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager, ILibrarySystem system, IConsoleFormatter formatter, LibraryContext context)
        {
            _authentication = authentication;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
            _system = system;
            _formatter = formatter;
            _context = context;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.CheckOutBook);

            var user = (IUser)_authentication.CurrentAccount;

            // If the User has checked out 5 books already
            //_system.CheckIfMaxQuotaReached(user.CheckedOutBooks);

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));

            //// BookID validation
            //if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            //    throw new ArgumentException(GlobalConstants.InvalidID);

            var bookToCheckOut = _bookManager.FindBook(bookID);

            // ASK: How to improve this (maybe extract it in methods?)
            // Check Book Status 
            if (bookToCheckOut.Status == BookStatus.Available)
            {
                _system.AddBookToCheckoutBooks(bookToCheckOut, user);

                _bookManager.UpdateStatus(bookToCheckOut, BookStatus.CheckedOut);               
            }
            //else if (bookToCheckOut.Status == BookStatus.Reserved)
            //{
            //    var suchBookInReservedBooks = _context.ReservedBooks.FirstOrDefault(x => x.UserId == user.Id && x.BookId == bookToCheckOut.Id); // user.ReservedBooks.Find(b => b.Id == bookToCheckOut.Id);

            //    if (suchBookInReservedBooks != null)
            //    {
            //        _system.AddBookToCheckoutBooks(bookToCheckOut, user); // user.AddBookToCheckoutBooks(bookToCheckOut);
            //       //user.RemoveFromReservedBooks(suchBookInReservedBooks);
            //        _bookManager.UpdateBook(bookID, BookStatus.CheckedOut, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(GlobalConstants.MaxCheckoutDays));
            //        _accountManager.UpdateUser(user);
            //    }
            //    else
            //        throw new ArgumentException(GlobalConstants.CheckoutBookAlreadyRes);
            //}
            else
            {
                throw new ArgumentException();
            }

            return _formatter.FormatCommandMessage(GlobalConstants.CheckoutBookSuccess, _formatter.FormatCheckedoutBook(bookToCheckOut));
        }
    }
}
