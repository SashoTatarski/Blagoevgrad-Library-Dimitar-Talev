using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
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
        private readonly IConsoleFormatter _formatter;
        public Engine(IConsoleRenderer renderer, IConsoleFormatter formatter, ICommandParser commandParser, IMenuFactory menuFactory, IAuthenticationManager authentication, ILibrarySystem system)
        {
            _renderer = renderer;
            _commandParser = commandParser;
            _menuFactory = menuFactory;
            _authentication = authentication;
            _system = system;
            _formatter = formatter;
        }

        public void Start()
        {
            VirtualDate.StartVirtualTime();

            _system.CheckForOverdueReservations();

            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.Welcome, GlobalConstants.DelimiterSymbol));
            string result = string.Empty;

            while (result != GlobalConstants.Goodbye)
            {
                var allowedcommands = _authentication.GetAllowedCommands();
                _renderer.Output(_menuFactory.GenerateMenu(allowedcommands, "command"));

                try
                {
                    ICommand command = _commandParser.GetCommandByNumber(int.Parse(_renderer.Input()), allowedcommands);

                    result = command.Execute();
                    _renderer.Output(result + GlobalConstants.NewLine);
                }
                catch (Exception ex)
                {
                    _renderer.Output(_formatter.CenterStringWithSymbols(ex.Message, '_'));
                }
            }
        }
    }
}
