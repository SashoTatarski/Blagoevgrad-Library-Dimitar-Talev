using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Services.Contracts;
using System;

namespace Library.Core.Commands
{
    public class RemoveUserCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly IAccountManager _accountManager;

        public RemoveUserCommand(IConsoleRenderer renderer, IAccountManager accountManager)
        {
            _renderer = renderer;
            _accountManager = accountManager;
        }

        public string Execute()
        {
            _accountManager.ListAllUsers();

            var username = _renderer.InputParameters("username");

            var userToRemove = (IUser)_accountManager.FindAccount(username);
            if (userToRemove is null)
            {
                throw new ArgumentException("Invalid username!");
            }
            if (userToRemove.CheckedOutBooks.Count != 0 || userToRemove.ReservedBooks.Count != 0)
            {
                throw new ArgumentException("You cannot remove user who has checkedout/reserved books");
            }

            _accountManager.RemoveUser(userToRemove);

            return $"The user {userToRemove.Username} is removed!";
        }
    }
}
