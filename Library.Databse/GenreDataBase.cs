using Library.Database.Contracts;
using Library.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class GenreDataBase : IDatabase<Genre>
    {
        private readonly LibraryContext _context;
        public GenreDataBase(LibraryContext context)
        {
            _context = context;
        }

        public Genre Find(string name)
        {
            return _context.Genres.FirstOrDefault(g => g.GenreName.ToLower() == name.ToLower());
        }

        public List<Genre> Read() => _context.Genres.ToList();

        public void Create(Genre item)
        {
            _context.Genres.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Genre item)
        {
            _context.Genres.Remove(item);
            _context.SaveChanges();
        }
        public Genre Find(int id)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == id);
        }
        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
