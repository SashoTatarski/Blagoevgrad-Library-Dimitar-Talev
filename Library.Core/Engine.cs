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

        public Engine(IConsoleRenderer renderer, ICommandParser commandParser, IMenuFactory menuFactory, IAuthenticationManager authentication)
        {
            _renderer = renderer;
            _commandParser = commandParser;
            _menuFactory = menuFactory;
            _authentication = authentication;
        }

        public void Start()
        {
            VirtualDate.StartVirtualTime();

            while (true)
            {
                _renderer.Output(_menuFactory.GenerateMenu(_authentication.CurrentAccount));

                var input = _renderer.Input().ToLower();

                try
                {
                   // _menuFactory.CheckAuthenticationForCommand(input);
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
