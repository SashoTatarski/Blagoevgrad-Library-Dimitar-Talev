using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class PublisherDataBase<Publisher> : IDataBase<Library.Models.Models.Publisher>
    {
        private readonly LibraryContext _context;
        public PublisherDataBase(LibraryContext context)
        {
            _context = context;
        }

        Models.Models.Publisher IDataBase<Models.Models.Publisher>.Find(string name)
        {
            return _context.Publishers.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        // Update ---------
        public void Create(Models.Models.Publisher item)
        {
            _context.Publishers.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Models.Models.Publisher item)
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Models.Publisher item)
        {
            throw new NotImplementedException();
        }

        Models.Models.Publisher IDataBase<Models.Models.Publisher>.Find(int id)
        {
            throw new NotImplementedException();
        }



        List<Models.Models.Publisher> IDataBase<Models.Models.Publisher>.Read()
        {
            throw new NotImplementedException();
        }
    }
}
