using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
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
        private readonly ILibrarySystem _system;

        public RemoveUserCommand(IConsoleRenderer renderer, IAccountManager accountManager, IConsoleFormatter formatter, ILibrarySystem system)
        {
            _renderer = renderer;
            _accountManager = accountManager;
            _formatter = formatter;
            _system = system;
        }

        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.RemoveUser, GlobalConstants.MiniDelimiterSymbol));
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.ChooseUser, '.'));

            _renderer.Output(_formatter.FormatListOfUsers(_accountManager.GetAllUsers()));
            

            var userToRemove = (User)_accountManager.FindAccount(_renderer.InputParameters("username"));

            if (userToRemove is null)
                throw new ArgumentException(GlobalConstants.NoSuchUser);

            if (_system.UserHasCheckedoutBooks(userToRemove) || _system.UserHasReservedBooks(userToRemove))
                throw new ArgumentException(GlobalConstants.RemoveUserError);

            _accountManager.RemoveUser(userToRemove);

            return _formatter.FormatCommandMessage(GlobalConstants.RemoveUserSuccess, _formatter.Format(userToRemove));
        }
    }
}
