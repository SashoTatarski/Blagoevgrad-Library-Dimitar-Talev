using Library.Models.Enums;
using Library.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models.BookManagement
{
    public class AddBookViewModel
    {
        public List<SelectListItem> Authors { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }

        public List<SelectListItem> Publishers { get; set; }
        public string PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public List<SelectListItem> GenresOptions { get; set; }
        public List<int> GenresIds { get; set; }
        public List<Genre> Genres { get; set; }


        public string Title { get; set; }

        public string ISBN { get; set; }    
       
        public int Year { get; set; }       

        public int Rack { get; set; }

        public int BookCopies { get; set; }

        public double Rating { get; set; }
        public BookStatus Status { get; set; }

    }
}
