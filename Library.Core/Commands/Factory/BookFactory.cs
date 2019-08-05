using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Commands.Factory
{
    public class BookFactory : IBookFactory
    {
        public IBook CreateBook(int id, string author, string title, string isbn, string subject, string publisher, int year, int rack, BookStatus status) => new Book(id, author, title, isbn, subject, publisher, year, rack, status);
    }
}
