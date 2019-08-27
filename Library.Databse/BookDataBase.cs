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
        public List<Models.Models.Book> Read() => _context.Books.ToList();

        public void Update(Models.Models.Book book) { }

        public void Delete(Models.Models.Book book)
        {
            var bookToRemove = _context.Books.FirstOrDefault(b => b.Id == book.Id);

            _context.Books.Remove(bookToRemove);
            _context.SaveChanges();
        }

        public Models.Models.Book Find(int bookId) => _context.Books.FirstOrDefault(b => b.Id == bookId);



        public Author CreateAuthor(string authorName)
        {
            if (FindAuthor(authorName) is null)
            {
                var newAuthor = new Author { Name = authorName };
                _context.Authors.Add(newAuthor);
                return newAuthor;
            }
            else
            {
                return FindAuthor(authorName);
            }
        }

        public Author FindAuthor(string authorName)
        {
            return _context.Authors.FirstOrDefault(a => a.Name.ToLower() == authorName);
        }

        public Publisher CreatePublisher(string publisher)
        {
            if (FindPublisher(publisher) is null)
            {
                var newPublisher = new Publisher { Name = publisher };
                _context.Publishers.Add(newPublisher);
                return newPublisher;
            }
            else
            {
                return FindPublisher(publisher);
            }
        }

        public Publisher FindPublisher(string publisher)
        {
            return _context.Publishers.FirstOrDefault(a => a.Name.ToLower() == publisher);
        }
    }
}
