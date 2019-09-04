using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class BookViewModel
    {
        public BookViewModel(Book book)
        {
            this.Title = book.Title;
            this.Author = book.Author.Name;
            this.ISBN = book.ISBN;
        }

        public string Title { get; set; }

        public string Author { get; set; }

        public string ISBN { get; set; }
    }
}
