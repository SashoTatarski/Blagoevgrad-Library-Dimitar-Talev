using Library.Models.Contracts;

namespace Services.Contracts
{
    public interface IAuthenticationManager
    {
        IAccount CurrentAccount { get; }

        void CheckAuthenticationForCommand(string commandAsString);
        void CheckForExistingUsername(string username);
        void LogIn(IAccount account);
        void LogOut();
    }
}
