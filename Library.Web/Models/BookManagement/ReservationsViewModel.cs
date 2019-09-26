using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models.BookManagement
{
    public class ReservationsViewModel
    {
        public List<BookViewModel> CheckedoutBooks { get; set; }

        public string BookId { get; set; }
        public Book Book { get; set; }

        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public string Title { get; set; }
        public string Status { get; set; }
        public User CheckedOutBy { get; set; }
        public List<User> ReservedBy { get; set; }

        public string ISBN { get; set; }
        public int Year { get; set; }
        public int Rack { get; set; }
        public double Rating { get; set; }


    }
}
