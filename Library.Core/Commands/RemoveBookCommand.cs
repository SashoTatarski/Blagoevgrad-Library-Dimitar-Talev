using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Models.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.Commands
{
    public class RemoveBookCommand : ICommand
    {
        private readonly IDatabaseService _service;        
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _account;

        public RemoveBookCommand(IDatabaseService database, IConsoleRenderer renderer, IAccountManager account)
        {
            _service = database;         
            _renderer = renderer;
            _account = account;
        }

        public string Execute()
        {
            var currentAccount = (ILibrarian)_account.CurrentAccount;
            
            var books = _service.ReadBooks();

            // Show all the books so user can select
            _service.ListAllBooks();

            var input = int.Parse(_renderer.InputParameters("ID"));
            if (input > books.Count || input < 1)
                throw new ArgumentException("Enter proper ID");
            
            var bookToRemove = books.Find(b => b.ID == input);
            _service.RemoveBook(bookToRemove);

            return $"You have successfully removed {bookToRemove.Title} - {bookToRemove.Author}";
        }
    }
}
