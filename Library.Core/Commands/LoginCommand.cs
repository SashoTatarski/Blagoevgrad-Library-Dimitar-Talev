using Library.Core.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IDatabaseService _service;
        private readonly IAccountManager _account;
        private readonly IConsoleRenderer _renderer;

        public LoginCommand(IDatabaseService service, IAccountManager account, IConsoleRenderer renderer)
        {
            _service = service;
            _account = account;
            _renderer = renderer;
        }

        public string Execute()
        {

            var userName = _renderer.InputParameters("username");
            var password = _renderer.InputParameters("password");

            var loggedUser = _service.FindAccount(userName);

            if (loggedUser == null)
                throw new ArgumentException("No such username!");

            if (loggedUser.Password != password)
                throw new ArgumentException("Wrong password!");

            _account.LogIn(loggedUser);

            return $"{loggedUser.Username} succefully logged!";
        }
    }
}
