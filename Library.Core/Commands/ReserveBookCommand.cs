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
        public ReserveBookCommand(IAuthenticationManager authentication, IBookManager bookManager, IConsoleRenderer renderer, IAccountManager accounManager, IConsoleFormatter formatter)

        {
            _authentication = authentication;
            _bookManager = bookManager;
            _renderer = renderer;
            _accountManager = accounManager;
            _formatter = formatter;
        }
        public string Execute()
        {
            var user = (IUser)_authentication.CurrentAccount;

            if (user.ReservedBooks.Count == 5)
            {
                throw new ArgumentException("You have reached the max quota of 5 reserved books!");
            }

            _bookManager.ListAllBooks();

            // BookID Input
            var bookID = int.Parse(_renderer.InputParameters("ID"));

            // BookID validation
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException("Invalid ID");
            }

            var bookToReserve = _bookManager.FindBook(bookID);

            // Check Book Status 
            // if the status is Available
            if (bookToReserve.Status == BookStatus.Available)
            {
                user.AddBookToReservedBooks(bookToReserve);
                _bookManager.UpdateBook(bookID, BookStatus.Reserved, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(5));
                _accountManager.UpdateUser(user);

                return $"You have reserved the book:\r\n{_formatter.Format(bookToReserve)}";
            }
            // if the book is already checked out
            else if (bookToReserve.Status == BookStatus.CheckedOut)
            {
                //after the other user return the book, this user will have 5 days to take it
                user.AddBookToReservedBooks(bookToReserve);
                _bookManager.UpdateBook(bookID, BookStatus.CheckedOut_and_Reserved, VirtualDate.VirtualToday, DateTime.MaxValue, true);
                _accountManager.UpdateUser(user);

                return $"You have reserved the book:\r\n{_formatter.Format(bookToReserve)}";
            }
            else if (bookToReserve.Status == BookStatus.Reserved)
            {
                var suchBookInReservedBooks = user.ReservedBooks.FirstOrDefault(b => b.ID == bookToReserve.ID);

                if (suchBookInReservedBooks != null)
                {
                    return  "You have already reserved this book!";
                }
                else
                    return "Book is already reserved by another user!";
            }
            else
            {
                return "Book is already checked out and reserved for another user afterwards!";
            }
        }
    }
}
