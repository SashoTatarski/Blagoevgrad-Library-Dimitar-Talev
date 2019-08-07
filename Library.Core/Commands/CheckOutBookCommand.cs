using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class CheckOutBookCommand : ICommand
    {
        private readonly IAccountManager _account;
        private readonly IDatabaseService _service;
        private readonly IConsoleRenderer _renderer;
        public CheckOutBookCommand(IAccountManager account, IDatabaseService service, IConsoleRenderer renderer)
        {
            _account = account;
            _service = service;
            _renderer = renderer;
        }

        public string Execute()
        {
            var currentAccount = (IUser)_account.CurrentAccount;

            var allUsers = _service.ReadUsers();
            var books = _service.ReadBooks();

            // Show all the books so user can select
            _service.ListAllBooks();

            //TODO validation ask instructor
            var input = int.Parse(_renderer.InputParameters("ID"));
            if (input > books.Count || input < 1)
                throw new ArgumentException("Enter proper ID");

            var user = allUsers.FirstOrDefault(u => u.Username == currentAccount.Username);
            var bookToCheckOut = books.FirstOrDefault(b => b.ID == input);

            // Check Book Status 
            // TODO ask trainer how to impore
            if (bookToCheckOut.Status == BookStatus.Available)
            {
                user.AddToCheckoutBooks(bookToCheckOut);
            }
            else if (bookToCheckOut.Status == BookStatus.Reserved)
            {
                if (currentAccount.ReservedBooks.Contains(bookToCheckOut))
                {
                    user.AddToCheckoutBooks(bookToCheckOut);
                    user.RemoveFromReservedBooks(bookToCheckOut);
                }
                else
                    throw new ArgumentException("Book is already reserved");
            }
            else
            {
                throw new ArgumentException("Book is already checked out");
            }

            // Update books database         
            _service.WriteBooks(books);

            // Update user database
            _service.WriteUsers(allUsers);

            return $"You have successfully checked out {bookToCheckOut.Title} - {bookToCheckOut.Author}";
        }
    }
}
