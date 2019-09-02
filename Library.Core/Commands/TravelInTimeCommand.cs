using Library.Core.Contracts;
using Library.Models.Utils;
using Library.Services.Contracts;

namespace Library.Core.Commands
{
    public class TravelInTimeCommand : ICommand
    {
        private readonly IConsoleRenderer _renderer;
        private readonly ILibrarySystem _system;
        private readonly IConsoleFormatter _formatter;

        public TravelInTimeCommand(IConsoleRenderer renderer, ILibrarySystem system, IConsoleFormatter formatter)
        {
            _renderer = renderer;
            _system = system;
            _formatter = formatter;
        }
        public string Execute()
        {
            _renderer.Output(_formatter.CenterStringWithSymbols(GlobalConstants.Travel, GlobalConstants.MiniDelimiterSymbol));

            var days = int.Parse(_renderer.InputParameters(GlobalConstants.DaysToSkip, d => int.Parse(d) < 1));

            VirtualDate.SkipDays(days);

            _system.ManageOverdueReservations();

            return _formatter.FormatCommandMessage(GlobalConstants.TravelSuccess, VirtualDate.VirtualToday.ToString("dd-MM-yyyy") + GlobalConstants.NewLine);
        }
    }
}
