using Library.Database.Configurations;
using Library.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<ReservedBook> ReservedBooks { get; set; }
        public DbSet<CheckoutBook> CheckoutBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString =
                @"Server=.\SQLEXPRESS;Database=LibraryDataBase;Trusted_Connection=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservedBookConfiguration());
            modelBuilder.ApplyConfiguration(new CheckoutBookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
