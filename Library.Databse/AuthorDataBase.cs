using Library.Database.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public class AuthorDataBase<Author> : IDataBase<Author>
    {
        public void Create(Author item)
        {
            throw new NotImplementedException();
        }

        public void Delete(Author item)
        {
            throw new NotImplementedException();
        }

        public Author Find(int id)
        {
            throw new NotImplementedException();
        }

        public List<Author> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(Author item)
        {
            throw new NotImplementedException();
        }
    }
}
