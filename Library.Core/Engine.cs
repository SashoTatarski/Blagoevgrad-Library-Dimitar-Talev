using Library.Core.Contracts;
using System;

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
                var input = _renderer.Input().ToLower();

                if (input.Length == 0)
                    break;

                try
                {
                    // ASK
                    //var command = _commandProcessor.ParseCommand(input);
                    //_renderer.Output(_commandProcessor.ProcessCommand(command));


                    _renderer.Output(_commandProcessor.ParseCommand(input).Execute();
                }
                catch (Exception ex)
                {
                    _renderer.Output(ex.Message);
                }
            }
        }
    }
}
