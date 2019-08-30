using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class CheckoutBookDatabase : IDatabase<CheckoutBook>
    {
        private readonly LibraryContext _context;
        public CheckoutBookDatabase(LibraryContext context)
        {
            _context = context;
        }

        public void Create(CheckoutBook item) => throw new NotImplementedException();
        public void Delete(CheckoutBook item) => throw new NotImplementedException();
        public CheckoutBook Find(int id) => throw new NotImplementedException();
        public CheckoutBook Find(string name) => throw new NotImplementedException();
        public List<CheckoutBook> Read() => _context.CheckoutBooks.ToList();
        public void Update(CheckoutBook item) => throw new NotImplementedException();
    }
}
