using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
           // var x = _context.BookGenre.ToList().Remove(bg=>bg.BookId==book.Id);
        }
    }
}
