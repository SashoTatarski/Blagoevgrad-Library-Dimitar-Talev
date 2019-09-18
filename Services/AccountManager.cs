using Library.Database;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.HashProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Library.Services
{
    public class AccountManager : IAccountManager
    {
        private readonly LibraryContext _context;
        private readonly IHasher _hasher;

        public AccountManager(LibraryContext context, IHasher hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public User Create(string username, string password)
        {
            if (_context.Users.Any(u => u.Username == username))
            {
                throw new ArgumentException($"{username} is already taken");
            }

            var defaultRole = _context.Roles.FirstOrDefault(r => r.RoleName == Constants.DefaultRole);

            if (defaultRole is null)
            {
                throw new InvalidOperationException("No default role in DB");
            }

            var user = new User
            {
                Username = username,
                HashPassword = _hasher.Hash(password),
                Role = defaultRole
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Find(string username, string password)
        {
            var user = _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username);

            if (user is null || !_hasher.Verify(password, user.HashPassword))
            {
                throw new ArgumentException("username or password incorrect");
            }

            return user;
        }


    }
}
