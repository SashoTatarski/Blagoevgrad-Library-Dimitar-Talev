using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models.BookManagement
{
    public class BookIssuedViewModel
    {
        public string BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public Author Author { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
