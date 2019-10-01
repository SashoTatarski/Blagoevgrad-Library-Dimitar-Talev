using Library.Models.Enums;
using Library.Models.Models;
using System;
using System.Linq;

namespace Library.Database.Seed
{
    public static class Seeder
    {
        public static void Init(LibraryContext context)
        {
            SeedRoles(context);
            SeedAdmin(context);
        }

        private static void SeedRoles(LibraryContext context)
        {
            if (context.Roles.Any())
                return;

            var roleNames = new[] { "user", "admin" };
            context.Roles.AddRange(
                roleNames.Select(name => new Role { RoleName = name })
            );
            context.SaveChanges();
        }

        private static void SeedAdmin(LibraryContext context)
        {
            // check if teams are already in the db
            if (context.Users.Any(u => u.Username == "admin"))
                return;

            var adminRole = context.Roles.Where(r => r.RoleName == "admin").FirstOrDefault();

            context.Users.Add(new User
            {
                Username = "admin",
                HashPassword = "M8GXPIckpkXfuYusx9e1xzZt4eTqCim2gtd5xmsHk4ds+BePHFMN9+DItyR7IiaJ",
                RoleId = adminRole.Id,
                MembershipStartDate = DateTime.Today,
                MembershipEndDate = DateTime.Today.AddYears(1),
                Status = AccountStatus.Active
            });

            context.SaveChanges();
        }
    }
}
