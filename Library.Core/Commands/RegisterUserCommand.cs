using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleFormatter _formatter;

        public RegisterUserCommand(IConsoleRenderer renderer, IAccountManager accountManager, IAuthenticationManager authentication, IConsoleFormatter formatter)
        {
            _renderer = renderer;
            _accountManager = accountManager;
            _authentication = authentication;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.RegisterUser, GlobalConstants.MiniDelimiterSymbol));

            var username = _renderer.InputParameters("username",
                s => s.Length < 3 || s.Length > 20);
            var password = _renderer.InputParameters("password",
                s => s.Length < 3 || s.Length > 20);

            _authentication.CheckForExistingUsername(username);
            var newUser = _accountManager.AddUser(username, password);

            return _formatter.FormatCommandMessage(GlobalConstants.UserRegisterSuccess, _formatter.Format(newUser));
        }
    }
}
