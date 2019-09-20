﻿using Library.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Factories.Contracts
{
    public interface IBookFactory
    {
        Task<Book> CreateBook(string title, string isbn, int year, int rack, Author author, Publisher publisher, List<int> genres);
    }
}
