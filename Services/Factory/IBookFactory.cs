using Library.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Factory
{
    public interface IBookFactory
    {
        IBook CreateBook(int currentId, string author, string title, string isbn, string subject, string publisher, int year, int rack);
    }
}
