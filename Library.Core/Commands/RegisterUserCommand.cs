using Library.Core.Contracts;
using Library.Services.Contracts;
using Library.Services.Factory;
using Services.Contracts;

namespace Library.Core.Commands
{
    public class RegisterUserCommand : ICommand
    {
        private readonly IUserFactory _userFactory;
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IAuthenticationManager _authentication;

        public RegisterUserCommand(IUserFactory userFactory, IConsoleRenderer renderer, IAccountManager accountManager, IAuthenticationManager authentication)
        {
            _userFactory = userFactory;
            _renderer = renderer;
            _accountManager = accountManager;
            _authentication = authentication;
        }

        public string Execute()
        {
            var username = _renderer.InputParameters("username",
                s => s.Length < 1 || s.Length > 30);
            var password = _renderer.InputParameters("password",
                s => s.Length < 3 || s.Length > 20);

            _authentication.CheckForExistingUsername(username);
            var newUser = _userFactory.CreateUser(username, password);

            _accountManager.AddUser(newUser);

            return $"Successfully created new user account: {newUser.Username}";
        }
    }
}
