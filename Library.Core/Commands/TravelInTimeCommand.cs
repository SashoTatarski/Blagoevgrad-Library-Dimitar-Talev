using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;

namespace Library.Core.Commands
{
    public class TravelInTimeCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
<<<<<<< HEAD

        public TravelInTimeCommand(IConsoleRenderer renderer)
=======
        private readonly ILibrarySystem _system;
        public TravelInTimeCommand(IConsoleRenderer renderer, ILibrarySystem system)
>>>>>>> 577c83ee1bf19f6522d03062f01fbb692e35f41d
        {
            _renderer = renderer;
            _system = system;
        }
        public string Execute()
        {
            var days = int.Parse(_renderer.InputParameters("how many days you want to skip", d => int.Parse(d) < 1));

            VirtualDate.SkipDays(days);

            _system.CheckForOverdueBooks();
            _system.CheckForOverdueReservations();

            return $"Success!\r\nToday is {VirtualDate.VirtualToday.ToString("dd-MM-yyyy")}";
        }
    }
}
