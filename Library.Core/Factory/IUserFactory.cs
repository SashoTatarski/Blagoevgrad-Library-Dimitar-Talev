using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Factory
{
    public interface IUserFactory
    {
        Models.Contracts.IUser CreateUser(string username, string password);
    }
}
