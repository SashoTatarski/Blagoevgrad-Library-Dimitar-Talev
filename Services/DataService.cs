using Library.Database;
using Library.Database.Contracts;
using Library.Database.JsonHandler;
using Library.Models.Contracts;
using Library.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Services
{
    public interface IDataService
    {
        void SeedDatabase();
        void ClearUpDatabase();
    }
    public class DataService : IDataService
    {
        private readonly IJsonHandler<Book> _jsonHandler;
        private readonly IDataBase<Book> _database;
        private readonly LibraryContext _context;

        public DataService(IJsonHandler<Book> jsonHandler, IDataBase<Book> database, LibraryContext context)
        {
            _jsonHandler = jsonHandler;
            _database = database;
            _context = context;
        }

        public void SeedDatabase()
        {
            var collection = _jsonHandler.Load();

            foreach (var item in collection)
            {
                _database.Create(item);
            }
        }

        public void ClearUpDatabase()
        {
            _context.Database.ExecuteSqlCommand(@"DELETE FROM [Books]");
            _context.Database.ExecuteSqlCommand(@"DELETE FROM [Users]");
            _context.Database.ExecuteSqlCommand(@"DELETE FROM [Librarians]");
            _context.Database.ExecuteSqlCommand(@"DELETE FROM [CheckoutBooks]");
            _context.Database.ExecuteSqlCommand(@"DELETE FROM [ReservedBooks]");
        }
    }
}
