using Library.Database.Configurations;
using Library.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Database
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() { }

        // For Tests
        public LibraryContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<ReservedBook> ReservedBooks { get; set; }
        public DbSet<CheckoutBook> CheckoutBooks { get; set; }
        public DbSet<BookGenre> BookGenre { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString =
                @"Server=.\SQLEXPRESS;Database=LibraryDataBase;Trusted_Connection=True;";

            //// Azure Connection
            //const string connectionString =
            //    @"Server=tcp:blagoevgradlibrary.database.windows.net,1433;Initial Catalog=LibraryDB;Persist Security Info=False;User ID=LibraryApp;Password=Telerik2019;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            if (optionsBuilder.IsConfigured)
            {
            }
                optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ReservedBookConfiguration());
            modelBuilder.ApplyConfiguration(new CheckoutBookConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookGenreConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
