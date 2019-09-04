using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models
{
    public class LibraryViewModel
    {
        public List<BookViewModel> Books { get; set; }
        public LibraryViewModel(IEnumerable<Book> books)
        {
            this.Books = new List<BookViewModel>();

            foreach (var book in books)
            {
                this.Books.Add(new BookViewModel(book));
            }
        }
    }
}
