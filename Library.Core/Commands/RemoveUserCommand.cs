using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class RemoveUserCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;
        private readonly IConsoleFormatter _formatter;

        public RemoveUserCommand(IConsoleRenderer renderer, IAccountManager accountManager, IConsoleFormatter formatter)
        {
            _renderer = renderer;
            _accountManager = accountManager;
            _formatter = formatter;
        }

        public string Execute()
        {
            _accountManager.ListAllUsers();

            var username = _renderer.InputParameters("username");

            var userToRemove = (IUser)_accountManager.FindAccount(username);

            if (userToRemove is null)
                throw new ArgumentException();

            if (userToRemove.CheckedOutBooks.Count != 0 || userToRemove.ReservedBooks.Count != 0)
                throw new ArgumentException(GlobalConstants.RemoveUserError);

            _accountManager.RemoveUser(userToRemove);

            return $"The user {_formatter.Format(userToRemove)} is removed!";
        }
    }
}
