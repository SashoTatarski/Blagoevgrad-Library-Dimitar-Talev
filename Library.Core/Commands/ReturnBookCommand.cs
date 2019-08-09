using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
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

        public ReturnBookCommand(IAuthenticationManager account, IConsoleRenderer renderer, IBookManager bookManager, IAccountManager accountManager)
        {
            _account = account;
            _renderer = renderer;
            _bookManager = bookManager;
            _accountManager = accountManager;
        }

        public string Execute()
        {
            var currentAccount = (IUser)_account.CurrentAccount;

            _renderer.Output(currentAccount.DisplayCheckedoutBooks());

            var bookID = int.Parse(_renderer.InputParameters("ID"));
            if (bookID < 1 || bookID > _bookManager.GetLastBookID())
            {
                throw new ArgumentException("Invalid ID");
            }

            var bookToReturn = _bookManager.FindBook(bookID);

            currentAccount.RemoveFromCheckedoutBooks(bookToReturn);
            _bookManager.UpdateBook(bookID, BookStatus.Available, DateTime.MinValue, DateTime.MinValue);
            _accountManager.UpdateUser(currentAccount);

            return $"Successfully returned : {bookToReturn.Title} - {bookToReturn.Author}";
        }
    }
}
