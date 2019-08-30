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
        private readonly ILibrarySystem _system;
        private readonly IConsoleFormatter _formatter;      

        public CheckOutBookCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IBookManager bookManager, ILibrarySystem system, IConsoleFormatter formatter)
        {
            _authentication = authentication;
            _renderer = renderer;
            _bookManager = bookManager;           
            _system = system;
            _formatter = formatter;           
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.CheckOutBook, GlobalConstants.MiniDelimiterSymbol));

            var user = (User)_authentication.CurrentAccount;

            // If the User has checked out 5 books already
            _system.CheckCheckoutBooksQuota(user);

            // Show all the books user can select from
            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));
            var bookToCheckOut = _bookManager.FindBook(bookID);

            // ASK: How to improve this (maybe extract it in methods?)           
            if (bookToCheckOut.Status == BookStatus.Available)
            {
                _system.AddBookToCheckoutBooks(bookToCheckOut, user);
                _bookManager.UpdateStatus(bookToCheckOut, BookStatus.CheckedOut);
            }
            else if (bookToCheckOut.Status == BookStatus.Reserved)
            {
                if (_system.ReservedByUser(user, bookToCheckOut))
                {
                    _system.AddBookToCheckoutBooks(bookToCheckOut, user);
                    _bookManager.UpdateStatus(bookToCheckOut, BookStatus.CheckedOut);
                }
                else
                    throw new ArgumentException(GlobalConstants.CheckoutBookAlreadyRes);
            }
            else
            {
                throw new ArgumentException();
            }

            return _formatter.FormatCommandMessage(GlobalConstants.CheckoutBookSuccess, _formatter.FormatCheckedoutBook(bookToCheckOut));
        }
    }
}
