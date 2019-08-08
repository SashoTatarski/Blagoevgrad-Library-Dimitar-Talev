using Library.Models.Contracts;
using Services.Contracts;

namespace Services
{
    public class AccountManager : IAccountManager
    {
        public IAccount CurrentAccount { get; private set; }

        public void LogIn(IAccount account)
        {
            this.CurrentAccount = account;
        }

        public void LogOut()
        {
            this.CurrentAccount = null;
        }
    }
}
