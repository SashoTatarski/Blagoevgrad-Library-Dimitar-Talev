﻿using Library.Core.Factory;
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
        public IBook CreateBook(string author, string title, string isbn, string subject, string publisher, int year, int rack) => new Book(author, title, isbn, subject, publisher, year, rack);
    }
}
