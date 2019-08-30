using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class LibrarianDataBase : IDatabase<Librarian>
    {
        private readonly LibraryContext _context;

        public LibrarianDataBase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(Librarian librarian)
        {
            _context.Librarians.Add(librarian);
            _context.SaveChanges();
        }

        public List<Librarian> Read() => _context.Librarians.ToList();
        public Librarian Find(int id) => _context.Librarians.FirstOrDefault(l => l.Id == id);

        // Update 
        public Librarian Find(string name)
        {
            throw new NotImplementedException();
        }
        public void Update(Librarian item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Librarian item)
        {
            throw new NotImplementedException();
        }
    }
}
