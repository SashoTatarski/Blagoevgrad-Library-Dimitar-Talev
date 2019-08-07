using Library.Core.Contracts;
using Library.Database;
using Library.Services.Contracts;
using Services;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IAccountManager _account;

        public LoginCommand(IDatabaseService service, IAccountManager account)
        {
            _service = service;
            _account = account;
        }

        public string Execute()
        {
            Console.WriteLine("Enter username: ");
            var userName = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            var password = Console.ReadLine();

            var loggedUser = _service.FindAccount(userName);

            if (loggedUser == null)
                throw new ArgumentException("No such username, douchebag");

            if (loggedUser.Password != password)
                throw new ArgumentException("Wrong password");

            _account.LogIn(loggedUser);

            return $"{loggedUser.Username} succefully logged!";

        }
    }
}
