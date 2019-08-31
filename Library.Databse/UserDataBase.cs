using Library.Database.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class UserDataBase : IDatabase<User>
    {
        private readonly LibraryContext _context;
        public UserDataBase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            var userToRemove = _context.Users.FirstOrDefault(x => x.Id == user.Id);

            userToRemove.Status = AccountStatus.Inactive;
            _context.SaveChanges();
        }

        public List<User> Read() => _context.Users.ToList();
        public User Find(int id) => _context.Users.FirstOrDefault(u => u.Id == id);
        public User Find(string name) => _context.Users.FirstOrDefault(u => u.Username == name);
        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
