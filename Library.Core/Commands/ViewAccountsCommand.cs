using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;

namespace Library.Core.Commands
{
    public class ViewAccountsCommand : ICommand
    {
        private readonly IAccountManager _accountManager;
        private readonly IConsoleRenderer _renderer;
        private readonly IConsoleFormatter _formatter;

        public ViewAccountsCommand(IConsoleRenderer renderer, IAccountManager accountManager, IConsoleFormatter formatter)
        {
            _accountManager = accountManager;
            _renderer = renderer;
            _formatter = formatter;
        }

        public string Execute()
        {
            _renderer.Output(GlobalConstants.View);

            var users = _accountManager.GetAllUsers();
            return _formatter.FormatListOfUsers(users);
        }
    }
}
