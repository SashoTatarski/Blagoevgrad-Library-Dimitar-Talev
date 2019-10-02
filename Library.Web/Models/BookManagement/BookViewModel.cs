using Library.Models.Enums;
using Library.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Web.Models.BookManagement
{
    public class BookViewModel
    {
        public string BookId { get; set; }

        public List<BookCopyViewModel> AllBookCopies { get; set; }

        [Required(ErrorMessage = "You must select number of copies")]
        [Range(1, int.MaxValue, ErrorMessage = "You can only add 1 or more copies!")]
        public int BookCopies { get; set; }

        [Required(ErrorMessage = "You must select an author")]
        public List<SelectListItem> Authors { get; set; }

        public string AuthorId { get; set; }
        public Author Author { get; set; }
        

        [Required(ErrorMessage = "You must select a publisher")]
        public string PublisherId { get; set; }
        public List<SelectListItem> Publishers { get; set; }
        public Publisher Publisher { get; set; } 
        

        [Required(ErrorMessage = "You must select at least one genre")]
        public List<int> GenresIds { get; set; }
        public List<SelectListItem> GenresOptions { get; set; }
        public List<Genre> Genres { get; set; }

        [Required(ErrorMessage = "You must enter a title")]
        public string Title { get; set; }

        //TODO: Add verification that isbn is 13 letters
        [Required(ErrorMessage = "You must enter an ISBN")]
        public string ISBN { get; set; }


        [Required(ErrorMessage = "You must select a valid year")]
        [Range(1629, 2019)]
        public int Year { get; set; }

        public int Rack { get; set; }
        public double Rating { get; set; }
        public BookStatus Status { get; set; }
        public bool IsBookCheckedout { get; set; }
        public bool IsChBooksMaxQuota { get; set; }

        public bool AreAllCopiesChecked { get; set; }
    }
}
