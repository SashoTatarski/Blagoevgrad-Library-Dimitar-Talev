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


            while (true)
            {
                _renderer.Output(_menuFactory.GenerateMenu(_authentication.CurrentAccount));

                var input = _renderer.Input().ToLower();

                try
                {
                    _renderer.Output(_commandParser.GetTheCommandByNumber(int.Parse(input)).Execute());
                }
                catch (Exception ex)
                {
                    _renderer.Output(ex.Message);

                }
            }
        }
    }
}
