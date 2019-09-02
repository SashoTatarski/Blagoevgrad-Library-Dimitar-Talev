using Library.Core.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticationManager _authentication;
        private readonly IAccountManager _accountManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;

        public LoginCommand(IAuthenticationManager authentication, IConsoleRenderer renderer, IAccountManager accountManager, IConsoleFormatter formatter)
        {
            _authentication = authentication;
            _accountManager = accountManager;
            _renderer = renderer;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.LogIn, GlobalConstants.MiniDelimiterSymbol));

            var username = _renderer.InputParameters("username");
            var password = _renderer.InputParameters("password");

            var loggedUser = _accountManager.FindAccount(username);

            if (loggedUser == null)
                throw new ArgumentException(GlobalConstants.NoSuchUserName);

            if (loggedUser.Password != password)
                throw new ArgumentException(GlobalConstants.InvalidPassword);

            _authentication.LogIn(loggedUser);

            return _formatter.FormatCommandMessage(GlobalConstants.SuccessLogIn, _formatter.Format(loggedUser));
        }
    }
}
