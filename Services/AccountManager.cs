using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Services.HashProvider;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<User> ActivateUserAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id.ToString() == id).ConfigureAwait(false);
            user.Status = AccountStatus.Active;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return user;
        }

        public async Task DeleteUserAsync(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id.ToString() == id).ConfigureAwait(false);

            user.Status = AccountStatus.Inactive;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();


        public async Task<User> CreateAsync(string username, string password, int membershipMonths)
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
                Role = defaultRole,
                MembershipStartDate = DateTime.Today,
                MembershipEndDate = DateTime.Today.AddMonths(membershipMonths),
                Status = AccountStatus.Active
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync().ConfigureAwait(false);

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
