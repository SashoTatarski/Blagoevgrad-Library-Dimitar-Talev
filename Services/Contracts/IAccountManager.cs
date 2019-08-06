using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IAccountManager
    {
        IAccount CurrentAccount { get; }

        void LogIn(IAccount account);
        void LogOut();
    }
}
