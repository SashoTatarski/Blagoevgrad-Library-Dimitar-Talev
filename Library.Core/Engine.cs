using Library.Core.Contracts;
using Library.Core.Factory;
using Services.Contracts;
using System;

namespace Library.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICommandParser _commandParser;
        private readonly IRenderer _renderer;
        private readonly IMenuFactory _menuFactory;
        private readonly IAccountManager _account;

        public Engine(IRenderer renderer, ICommandParser commandParser, IMenuFactory menuFactory, IAccountManager account)
        {
            _renderer = renderer;
            _commandParser = commandParser;
            _menuFactory = menuFactory;
            _account = account;
        }

        public void Start()
        {
            while (true)
            {
                _renderer.Output(_menuFactory.GenerateMenu(_account.CurrentAccount));

                var input = _renderer.Input().ToLower();

                if (input.Length == 0)
                    break;

                try
                {
                    _menuFactory.CheckAuthenticationForCommand(input);
                    _renderer.Output(_commandParser.ParseCommand(input).Execute());
                }
                catch (Exception ex)
                {
                    _renderer.Output(ex.Message);
                }
            }
        }
    }
}
