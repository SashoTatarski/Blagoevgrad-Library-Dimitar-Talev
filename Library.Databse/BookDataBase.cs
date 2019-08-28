using Library.Database.Contracts;
using Library.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    // SOLID: Liskov - we can substitute JSON with another type of DB
    public class BookDatabase<Book> : IDataBase<Models.Models.Book>
    {
        private readonly LibraryContext _context;

        public BookDatabase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Models.Models.Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(Models.Models.Book book)
        {
            var bookToRemove = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            _context.Books.Remove(bookToRemove);
            _context.SaveChanges();
        }

        public List<Models.Models.Book> Read() => _context.Books.ToList();
        public Models.Models.Book Find(int bookId) => _context.Books.FirstOrDefault(b => b.Id == bookId);


        // Update
        public void Update(Models.Models.Book book)
        {
            throw new System.NotImplementedException();
        }
        public Models.Models.Book Find(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
