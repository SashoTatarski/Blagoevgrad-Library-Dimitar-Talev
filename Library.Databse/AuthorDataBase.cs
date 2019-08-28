﻿using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class AuthorDataBase<Author> : IDataBase<Models.Models.Author>
    {
        private readonly LibraryContext _context;
        public AuthorDataBase(LibraryContext context)
        {
            _context = context;
        }

        public Models.Models.Author Find(string name)
        {
            return _context.Authors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower());
        }

        public void Create(Models.Models.Author item)
        {
            _context.Authors.Add(item);
            _context.SaveChanges();
        }

        // Update----------


        public void Delete(Models.Models.Author item)
        {
            throw new NotImplementedException();
        }


        public List<Models.Models.Author> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(Models.Models.Author item)
        {
            throw new NotImplementedException();
        }

        public Models.Models.Author Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
