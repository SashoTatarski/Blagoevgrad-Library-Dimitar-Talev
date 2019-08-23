using Library.Models.Contracts;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Database.Contracts
{
    public interface IUserDataBase
    {
        void Create(IUser user);
        IUser Get(string username);
        void Update(IUser user);
        void Delete(IUser user);
        List<User> Load();
        
    }
}
