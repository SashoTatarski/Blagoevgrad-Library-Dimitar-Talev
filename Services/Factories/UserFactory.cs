﻿using Library.Models.Models;
using Library.Services.Factories.Contracts;

namespace Library.Services.Factory
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string username, string password) => new User(username, password);
    }
}
