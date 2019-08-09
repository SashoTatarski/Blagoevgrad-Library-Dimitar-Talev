using Library.Core.Contracts;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;

        public LoginCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IAccountManager accountManager)
        {
            _authentication = authentication;
            _renderer = renderer;
            _accountManager = accountManager;
        }

        public string Execute()
        {
            var userName = _renderer.InputParameters("username");
            var password = _renderer.InputParameters("password");

            var loggedUser = _accountManager.FindAccount(userName);

            if (loggedUser == null)
                throw new ArgumentException("No such username!");

            if (loggedUser.Password != password)
                throw new ArgumentException("Wrong password!");

            _authentication.LogIn(loggedUser);

            return $"{loggedUser.Username} succefully logged!";
        }
    }
}
