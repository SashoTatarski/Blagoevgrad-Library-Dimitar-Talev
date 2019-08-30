using Library.Database.Contracts;
using Library.Models.Models;
using System;
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

        //Update 
        public void Delete(Genre item)
        {
            throw new NotImplementedException();
        }
        public Genre Find(int id)
        {
            throw new NotImplementedException();
        }
        public void Update(Genre item)
        {
            throw new NotImplementedException();
        }
    }
}
