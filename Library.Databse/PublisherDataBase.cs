using Library.Database.Contracts;
using Library.Models.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Database
{
    public class PublisherDataBase : IDatabase<Publisher>
    {
        private readonly LibraryContext _context;
        public PublisherDataBase(LibraryContext context)
        {
            _context = context;
        }

        Publisher IDatabase<Publisher>.Find(string name) => _context.Publishers.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        Publisher IDatabase<Publisher>.Find(int id) => _context.Publishers.FirstOrDefault(p => p.Id == id);
        List<Publisher> IDatabase<Publisher>.Read() => _context.Publishers.ToList();

        public void Create(Publisher item)
        {
            _context.Publishers.Add(item);
            _context.SaveChanges();
        }

        public void Delete(Publisher item)
        {
            _context.Publishers.Remove(item);
            _context.SaveChanges();
        }
        public void Update()
        {
            _context.SaveChanges();
        }
    }
}
