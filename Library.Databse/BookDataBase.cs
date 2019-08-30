using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    // SOLID: Liskov - we can substitute JSON with another type of DB
    public class BookDatabase : IDatabase<Book>
    {
        private readonly LibraryContext _context;

        public BookDatabase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            var bookToRemove = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            _context.Books.Remove(bookToRemove);
            _context.SaveChanges();
        }

        public List<Book> Read()
        {
            var books = _context.Books
               .Include(b => b.Author)
               .Include(b => b.Publisher)
               .Include(b => b.BookGenres)
               .ThenInclude(bg => bg.Genre)
               .ToList();

            return books;
        }
        public Book Find(int bookId) => _context.Books.FirstOrDefault(b => b.Id == bookId);


        // Update
        public void Update(Book book)
        {
            throw new System.NotImplementedException();
        }
        public Book Find(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
