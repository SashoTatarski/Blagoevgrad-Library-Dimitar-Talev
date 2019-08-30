using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class PublisherDataBase : IDataBase<Publisher>
    {
        private readonly LibraryContext _context;
        public PublisherDataBase(LibraryContext context)
        {
            _context = context;
        }

        Models.Models.Publisher IDataBase<Publisher>.Find(string name)
        {
            return _context.Publishers.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        // Update ---------
        public void Create(Publisher item)
        {
            _context.Publishers.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Publisher item)
        {
            throw new NotImplementedException();
        }

        public void Update(Publisher item)
        {
            throw new NotImplementedException();
        }

        Publisher IDataBase<Publisher>.Find(int id)
        {
            throw new NotImplementedException();
        }

        List<Publisher> IDataBase<Publisher>.Read() => _context.Publishers.ToList();
       
    }
}
