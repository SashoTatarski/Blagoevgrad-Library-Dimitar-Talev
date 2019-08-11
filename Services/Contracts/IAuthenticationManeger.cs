using Library.Models.Contracts;
using System.Collections.Generic;

namespace Services.Contracts
{
    public interface IAuthenticationManager
    {
        IAccount CurrentAccount { get; }
        List<string> GetAllowedCommands();
        void CheckForExistingUsername(string username);
        void LogIn(IAccount account);
        void LogOut();
        string GetCurrentAccountType();
    }
}
