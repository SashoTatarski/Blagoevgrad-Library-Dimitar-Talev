using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class ReturnBookCommand : ICommand
    {
        private readonly IAuthenticationManager _account;
        private readonly IConsoleRenderer _renderer;
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;

        public ReturnBookCommand(IAuthenticationManager account, IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager, ILibrarySystem system)
        {
            _account = account;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
            _system = system;
        }

        public string Execute()
        {
            var user = (IUser)_account.CurrentAccount;

            //Check if there sre overdue books
            if (_system.HasOverdueBooks(user))
            {
                _system.AssignFee(user);

                _renderer.Output("Overdue books\r\n");
                _renderer.Output(_system.DisplayOverdueBooks(user));
            }
            else
            {
                _renderer.Output(_system.DisplayCheckedoutBooks(user));
            }

            //Enter ID:
            int bookID;
            do
            {
                bookID = int.Parse(_renderer.InputParameters("ID"));
            }
            while (user.CheckedOutBooks.FindAll(b => b.ID == bookID).Count == 0 && user.OverdueBooks.FindAll(b => b.ID == bookID).Count == 0);

            //Find the book to remove
            var bookToReturn = _bookManager.FindBook(bookID);

            //Check if the book to remove is overdue, in order to know from where to delete it
            if (_system.HasOverdueBooks(user))
            {
                user.RemoveFromOverdueBooks(bookToReturn);
            }
            else
            {
                user.RemoveFromCheckedoutBooks(bookToReturn);
            }

            // Check which status to assign to the book after it get returned
            if (bookToReturn.Status == BookStatus.CheckedOut)
            {
                _bookManager.UpdateBook(bookID, BookStatus.Available, DateTime.MinValue, DateTime.MinValue);
            }
            if (bookToReturn.Status == BookStatus.CheckedOut_and_Reserved)
            {
                _bookManager.UpdateBook(bookID, BookStatus.Reserved, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(5));
            }

            _accountManager.UpdateUser(user);

            return $"Successfully returned : {bookToReturn.Title} - {bookToReturn.Author}";
        }
    }
}
