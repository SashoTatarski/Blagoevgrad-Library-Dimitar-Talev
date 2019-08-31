using Library.Database.Contracts;
using Library.Models.Models;
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
        public Librarian Find(string name) => _context.Librarians.FirstOrDefault(l => l.Username == name);
        public void Update()
        {
            _context.SaveChanges();
        }
        public void Delete(Librarian item)
        {
            _context.Librarians.Remove(item);
            _context.SaveChanges();
        }
    }
}
