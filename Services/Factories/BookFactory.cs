using Library.Database;
using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Factories
{
    public class BookFactory : IBookFactory
    {
        private readonly LibraryContext _context;
        public BookFactory(LibraryContext context)
        {
            _context = context;
        }
        public async Task<Book> CreateBook(string title, string isbn, int year, int rack, Author author, Publisher publisher, List<int> genresIds)
        {
            var newBook = new Book
            {
                Title = title,
                ISBN = isbn,
                Year = year,
                Rack = rack,
                Author = author,
                Publisher = publisher                
            };

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();

            foreach (var genre in genresIds)
            {
                _context.BookGenre.Add(new BookGenre
                {
                    BookId = newBook.Id,
                    GenreId = genre
                });
            }

            await _context.SaveChangesAsync();

            return newBook;
        }
    }
}
