using Library.Core.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factory;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace Library.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICommandParser _commandParser;
        private readonly IConsoleRenderer _renderer;
        private readonly IMenuFactory _menuFactory;
        private readonly IAuthenticationManager _authentication;
        private readonly ILibrarySystem _system;
        private readonly IAccountManager _accountManager;

        public Engine(IConsoleRenderer renderer, ICommandParser commandParser, IMenuFactory menuFactory, IAuthenticationManager authentication, ILibrarySystem system, IAccountManager accountManager)
        {
            _renderer = renderer;
            _commandParser = commandParser;
            _menuFactory = menuFactory;
            _authentication = authentication;
            _system = system;
            _accountManager = accountManager;
        }

        public void Start()
        {
            VirtualDate.StartVirtualTime();
            _system.CheckForOverdueBooks();
            _system.CheckForOverdueReservations();


            while (true)
            {
                var allowedcommands = _authentication.GetAllowedCommands();

                if (_authentication.GetCurrentAccountType() == "User")
                {
                    var user = (IUser)_authentication.CurrentAccount;
                    if (_system.HasOverdueBooks(user))
                    {
                        _renderer.Output(_system.GetMessageForOverdueBooks(user));

                        allowedcommands = new List<string> { "Return Book", "Log Out" };
                    }
                    if (_system.HasOverdueReservations(user))
                    {
                        _renderer.Output(_system.GetMessageForOverdueReservations(user));
                    }
                }

                _renderer.Output(_menuFactory.GenerateMenu(allowedcommands));

                var input = _renderer.Input();

                try
                {
                    ICommand command = _commandParser.GetCommandByNumber(int.Parse(input), allowedcommands);

                    _renderer.Output(command.Execute());
                    _renderer.Output("\r\n");
                }
                catch (Exception ex)
                {
                    _renderer.Output(ex.Message);
                    _renderer.Output("\r\n");
                }
            }
        }
    }
}
