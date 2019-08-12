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
        private readonly IConsoleFormatter _formatter;

        public ReturnBookCommand(IAuthenticationManager account, IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager, ILibrarySystem system, IConsoleFormatter formatter)
        {
            _account = account;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
            _system = system;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.ReturnBook);

            var user = (IUser)_account.CurrentAccount;

            //Check if there are overdue books
            if (user.HasOverdueBooks())
            {
                _system.AssignFee(user);

                _renderer.Output("Overdue books:\r\n");
                _renderer.Output(_bookManager.GetOverdueBooksInfo(user));
            }
            else
            {
                _renderer.Output(_bookManager.GetCheckedoutBooksInfo(user));
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
            if (user.HasOverdueBooks())
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
                _bookManager.UpdateBook(bookID, BookStatus.Reserved, VirtualDate.VirtualToday, VirtualDate.VirtualToday.AddDays(GlobalConstants.MaxReserveDays), true);
            }

            _accountManager.UpdateUser(user);

            return _formatter.FormatCommandMessage(GlobalConstants.ReturnBookSuccessMsg, _formatter.Format(bookToReturn));
        }
    }
}
