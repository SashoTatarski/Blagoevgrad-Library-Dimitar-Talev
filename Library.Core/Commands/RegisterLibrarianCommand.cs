using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class RegisterLibrarianCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly ILibrarianFactory _librarianfactory;

        public RegisterLibrarianCommand(ILibrarianFactory librarianfactory, IDatabaseService service)
        {
            _librarianfactory = librarianfactory;
            _service = service;
        }

        public string Execute()
        {
            // Both of these should be useless
            // var currentAccount = (ILibrarian)_account.CurrentAccount;
            // var users = _service.ReadUsers();

            Console.WriteLine("Enter new Librarian username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter new Librarian password: ");
            var password = Console.ReadLine();

            var newLibrarian = _librarianfactory.CreateLibrarian(username, password);

            _service.AddLibrarian(newLibrarian);

            return $"Successfully created {newLibrarian.Username}";
        }
    }
}
