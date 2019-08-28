using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserFactory _userFactory;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IAuthenticationManager _authentication;
        private readonly IConsoleFormatter _formatter;

        public RegisterUserCommand(IUserFactory userFactory, IConsoleRenderer renderer, IAccountManager accountManager, IAuthenticationManager authentication, IConsoleFormatter formatter)
        {
            _userFactory = userFactory;
            _renderer = renderer;
            _accountManager = accountManager;
            _authentication = authentication;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.RegisterUser);

            var username = _renderer.InputParameters("username",
                s => s.Length < 1 || s.Length > 30);
            var password = _renderer.InputParameters("password",
                s => s.Length < 3 || s.Length > 20);

            _authentication.CheckForExistingUsername(username);
            var newUser = _userFactory.CreateUser(username, password);

            //_accountManager.AddUser(newUser);

            return _formatter.FormatCommandMessage(GlobalConstants.UserRegisterSuccess, _formatter.Format(newUser));
        }
    }
}
