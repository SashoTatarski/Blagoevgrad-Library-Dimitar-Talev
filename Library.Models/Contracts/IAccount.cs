using System.Collections.Generic;

namespace Library.Models.Contracts
{
    // Interface Segregation: consumer of this interface uses all of its methods. Interface is specific for the needs of the dependency
    public interface IAccount
    {
        string Username { get; }

        string Password { get; }

        IEnumerable<string> AllowedCommands { get; }
    }
}
