using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.BookManagement
{
    public class SearchViewModel
    {
        [Required]        
        public string SearchName { get; set; }

        public List<BookViewModel> BookSearchResults { get; set; } = new List<BookViewModel>();

        public List<BookViewModel> AllBooks { get; set; } = new List<BookViewModel>();

        public int BookCopies { get; set; }
    }
}
