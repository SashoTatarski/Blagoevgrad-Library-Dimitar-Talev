using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Factory
{
    public class LibrarianFactory : ILibrarianFactory
    {
        public ILibrarian CreateLibrarian(string username, string password) => new Librarian(username, password);
    }
}
