using Library.Core.Contracts;
using Library.Models.Utils;

namespace Library.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute() => GlobalConstants.Goodbye;
    }
}
