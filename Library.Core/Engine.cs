using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factory;
using Services.Contracts;
using System;

namespace Library.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICommandParser _commandParser;
        private readonly IConsoleRenderer _renderer;
        private readonly IMenuFactory _menuFactory;
        private readonly IAuthenticationManager _authentication;
        private readonly ILibrarySystem _system;

        public Engine(IConsoleRenderer renderer, ICommandParser commandParser, IMenuFactory menuFactory, IAuthenticationManager authentication, ILibrarySystem system)
        {
            _renderer = renderer;
            _commandParser = commandParser;
            _menuFactory = menuFactory;
            _authentication = authentication;
            _system = system;
        }

        public void Start()
        {
            VirtualDate.StartVirtualTime();
            _system.CheckForOverdueBooks();
            _system.CheckForOverdueReservations();

            string result = string.Empty;

            while (result != GlobalConstants.Goodbye)
            {
                var allowedcommands = _authentication.GetAllowedCommands();

                _renderer.Output(_menuFactory.GenerateMenu(allowedcommands));

                var input = _renderer.Input();

                try
                {
                    ICommand command = _commandParser.GetCommandByNumber(int.Parse(input), allowedcommands);

                    result = command.Execute();
                    _renderer.Output(result);
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
