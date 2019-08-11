using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;

namespace Library.Core.Commands
{
    public class TravelInTimeCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;

        public TravelInTimeCommand(IConsoleRenderer renderer)
        {
            _renderer = renderer;
        }
        public string Execute()
        {
            var days = int.Parse(_renderer.InputParameters("how many days you want to skip", d => int.Parse(d) < 1));

            VirtualDate.SkipDays(days);

            return $"Success!\r\nToday is {VirtualDate.VirtualToday.ToString("dd-MM-yyyy")}";
        }
    }
}
