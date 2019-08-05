using Library.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core
{
    public sealed class Engine : IEngine
    {
        private readonly ICommandProcessor _commandProcessor;
        private readonly IRenderer _renderer;

        public Engine(IRenderer renderer, ICommandProcessor commandProcessor)
        {
            _renderer = renderer;
            _commandProcessor = commandProcessor;
        }


        public void Start()
        {
            while (true)
            {
                var input = _renderer.Input();

                if (input.Length == 0) break;

                try
                {
                    _renderer.Output(_commandProcessor.ProcessCommands(input));
                }
                catch (Exception ex)
                {
                    _renderer.Output(ex.Message);
                }
            }
        }
    }
}
