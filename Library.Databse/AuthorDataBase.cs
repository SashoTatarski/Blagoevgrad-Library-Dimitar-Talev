using Library.Database.Contracts;
using Library.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class AuthorDataBase : IDatabase<Author>
    {
        private readonly LibraryContext _context;
        public AuthorDataBase(LibraryContext context)
        {
            _context = context;
        }

        public Author Find(string name)
        {
            return _context.Authors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
        }

        public void Create(Author item)
        {
            _context.Authors.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Author item)
        {
            _context.Remove(item);
            _context.SaveChanges();
        }

        public List<Author> Read()
        {
            return _context.Authors.ToList();
        }

        public Author Find(int id)
        {
            return _context.Authors.FirstOrDefault(a => a.Id == id);
        }

        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
