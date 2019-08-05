using Library.Models.Contracts;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Database
{
    public interface IJson
    {
        List<Book> ReadBooks();
        void WriteBooks(IEnumerable<IBook> books);
    }
}
