using Library.Core.Contracts;
using Library.Core.Factory;
using Services.Contracts;
using System;

namespace Library.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICommandParser _commandParser;
        private readonly IConsoleRenderer _renderer;
        private readonly IMenuFactory _menuFactory;
        private readonly IAccountManager _account;

        public Engine(IConsoleRenderer renderer, ICommandParser commandParser, IMenuFactory menuFactory, IAccountManager account)
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
