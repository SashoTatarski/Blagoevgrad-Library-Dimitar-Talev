using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class BookGenreDatabase : IDatabase<BookGenre>
    {
        private readonly LibraryContext _context;
        public BookGenreDatabase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(BookGenre item) => throw new NotImplementedException();
        public void Delete(BookGenre item) => throw new NotImplementedException();
        public BookGenre Find(int id) => throw new NotImplementedException();
        public BookGenre Find(string name) => throw new NotImplementedException();
        public List<BookGenre> Read() => _context.BookGenre.ToList();
        public void Update(BookGenre item) => throw new NotImplementedException();
    }
}
