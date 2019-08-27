using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class UserDataBase<User> : IDataBase<Models.Models.User>
    {
        private readonly LibraryContext _context;

        public UserDataBase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Models.Models.User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(Models.Models.User user)
        {
            var userToRemove = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            userToRemove.Status = AccountStatus.Inactive;
            _context.SaveChanges();
        }

        public void Update(Models.Models.User updatedUser)
        {
            throw new NotImplementedException();
        }

        public List<Models.Models.User> Read() => _context.Users.ToList();

        public Models.Models.User Find(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
    }
}
