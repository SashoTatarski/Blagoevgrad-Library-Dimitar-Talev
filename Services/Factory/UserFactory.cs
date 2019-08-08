using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Factory
{
    public class UserFactory : IUserFactory
    {
        public IUser CreateUser(string username, string password) => new User(username, password);
    }
}
