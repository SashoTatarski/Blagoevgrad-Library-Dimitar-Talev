using System.Collections.Generic;

namespace Library.Models.Contracts
{
    public interface IAccount
    {
        string Username { get; }
        string Password { get; }
        IEnumerable<string> AllowedCommands { get; }
    }
}
