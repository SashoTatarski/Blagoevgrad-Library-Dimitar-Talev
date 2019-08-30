using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class BookGenreDataBase
    {
        private readonly LibraryContext _context;
        public BookGenreDataBase(LibraryContext context)
        {
            _context = context;
        }
        public void Create(Book book, List<Genre> genres)
        {
            foreach (var genre in genres)
            {
                _context.BookGenre.Add(new BookGenre { BookId = book.Id, GenreId = genre.Id });
            }
            _context.SaveChanges();
        }

        public void Update(Book book, List<Genre> genres)
        {
            foreach (var bookGenre in _context.BookGenre)
            {
                if (bookGenre.BookId == book.Id)
                {
                    _context.BookGenre.Remove(bookGenre);
                }
            }
            _context.SaveChanges();
            this.Create(book, genres);
        }

        public List<BookGenre> Read() => _context.BookGenre.ToList();
    }
}