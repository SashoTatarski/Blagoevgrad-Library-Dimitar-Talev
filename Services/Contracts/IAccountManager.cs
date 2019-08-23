﻿using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    //SOLID: Interface Segregation: consumer of this interface uses all of its methods. Interface is specific for the needs of the dependency
    public interface IAccountManager
    {
        IAccount FindAccount(string userName);

        void AddLibrarian(ILibrarian librarian);

        void AddUser(IUser user);

        List<User> GetAllUsers();

        void GetListAllUsers();

        void RemoveUser(IUser user);

        void UpdateUser(IUser user);
    }
}
