using Library.Models.Contracts;
using System.Collections.Generic;

namespace Services.Contracts
{
    //SOLID: Interface Segregation: consumer of this interface uses all of its methods. Interface is specific for the needs of the dependency
    public interface IAuthenticationManager
    {
        IAccount CurrentAccount { get; }

        List<string> GetAllowedCommands();

        void CheckForExistingUsername(string username);

        void LogIn(IAccount account);

        void LogOut();
    }
}
