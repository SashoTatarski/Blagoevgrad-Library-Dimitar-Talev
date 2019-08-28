using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class GenreDataBase<Genre> : IDataBase<Models.Models.Genre>
    {
        private readonly LibraryContext _context;
        public GenreDataBase(LibraryContext context)
        {
            _context = context;
        }

        public Models.Models.Genre Find(string name)
        {
            return _context.Genres.FirstOrDefault(g => g.GenreName.ToLower() == name.ToLower());
        }

        //Update 
        public void Create(Models.Models.Genre item)
        {
            _context.Genres.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Models.Models.Genre item)
        {
            throw new NotImplementedException();
        }

        public Models.Models.Genre Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Models.Models.Genre> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Models.Genre item)
        {
            throw new NotImplementedException();
        }
    }
}
