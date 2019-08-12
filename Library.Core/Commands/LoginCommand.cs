using Library.Core.Contracts;
using Library.Models.Utils;
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
        private readonly IConsoleFormatter _formatter;

        public LoginCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IAccountManager accountManager,IConsoleFormatter formatter)
        {
            _authentication = authentication;
            _renderer = renderer;
            _accountManager = accountManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.LogIn);

            var userName = _renderer.InputParameters("username");
            var password = _renderer.InputParameters("password");

            var loggedUser = _accountManager.FindAccount(userName);

            if (loggedUser == null)
                throw new ArgumentException(GlobalConstants.NoSuchUserName);

            if (loggedUser.Password != password)
                throw new ArgumentException(GlobalConstants.InvalidPassword);

            _authentication.LogIn(loggedUser);

            return _formatter.FormatCommandMessage(GlobalConstants.SuccessLogIn, _formatter.Format(loggedUser));
        }
    }
}
