using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class ReserveBookDatabase : IDatabase<ReservedBook>
    {
        private readonly LibraryContext _context;
        public ReserveBookDatabase(LibraryContext context)
        {
            _context = context;        }

        public void Create(ReservedBook item) => throw new NotImplementedException();
        public void Delete(ReservedBook item) => throw new NotImplementedException();
        public ReservedBook Find(int id) => throw new NotImplementedException();
        public ReservedBook Find(string name) => throw new NotImplementedException();
        public List<ReservedBook> Read() => _context.ReservedBooks.ToList();
        public void Update(ReservedBook item) => throw new NotImplementedException();
    }
}
