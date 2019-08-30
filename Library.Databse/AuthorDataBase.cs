using Library.Database.Contracts;
using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        // Update----------


        public void Delete(Author item)
        {
            throw new NotImplementedException();
        }


        public List<Author> Read()
        {
            var authors = _context.Authors.ToList();               
            return authors;
        }

        public void Update(Author item)
        {
            throw new NotImplementedException();
        }

        public Author Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
