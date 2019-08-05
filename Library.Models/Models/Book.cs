using Library.Models.Contracts;
using Library.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Models
{
    public class Book : IBook
    {
        public Book(int id,
                    string author,
                    string title,
                    string isbn,
                    string subject,
                    string publisher,
                    int year,
                    int rack,
                    BookStatus status)
        {

        }
    }
}
