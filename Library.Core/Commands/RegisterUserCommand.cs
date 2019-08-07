using Library.Core.Contracts;
using Library.Core.Factory;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {        
        private readonly IDatabaseService _service;    
        private readonly IUserFactory _userfactory;

        public RegisterUserCommand(IUserFactory userfactory, IDatabaseService service)
        {
            _userfactory = userfactory;           
            _service = service;            
        }

        public string Execute()
        {
            // Both of these should be useless
            // var currentAccount = (ILibrarian)_account.CurrentAccount;
            // var users = _service.ReadUsers();

            Console.WriteLine("Enter new username: ");
            var username = Console.ReadLine();
            Console.WriteLine("Enter new password: ");
            var password = Console.ReadLine();

            var newUser = _userfactory.CreateUser(username, password);

            _service.AddUser(newUser);

            return $"Successfully created {newUser.Username}";
        }
    }
}
