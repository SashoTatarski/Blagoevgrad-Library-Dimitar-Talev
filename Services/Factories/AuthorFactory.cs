using Library.Models.Models;
using Library.Services.Factories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Factories
{
    public class AuthorFactory : IAuthorFactory
    {
        public Author CreateAuthor(string authorName)
        {

            return new Author { Name = authorName };
        }
    }
}
