using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class LibrarianDataBase<Librarian> : IDataBase<Models.Models.Librarian>
    {
        private readonly LibraryContext _context;

        public LibrarianDataBase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Models.Models.Librarian librarian)
        {
            _context.Librarians.Add(librarian);
            _context.SaveChanges();
        }

        public void Update(Models.Models.Librarian item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Models.Models.Librarian item)
        {
            throw new NotImplementedException();
        }

        public List<Models.Models.Librarian> Read() => _context.Librarians.ToList();

        public Models.Models.Librarian Find(int id) => _context.Librarians.FirstOrDefault(l => l.Id == id);
    }
}
