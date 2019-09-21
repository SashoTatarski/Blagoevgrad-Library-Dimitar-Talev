using Library.Models.Models;
using Library.Web.Models.BookManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Mapper
{
    public static class MapperExtensions
    {
        public static BookViewModel MapToViewModel(this Book book)
        {
            var vm = new BookViewModel();
            vm.BookId = book.Id.ToString();
            vm.Title = book.Title;
            vm.Year = book.Year;
            vm.Rack = book.Rack;
            vm.ISBN = book.ISBN;
            vm.Rating = book.Rating;
            vm.Author = book.Author;
            vm.Publisher = book.Publisher;
            vm.Status = book.Status;
            vm.Genres = book.BookGenres.Select(bg => bg.Genre).ToList();

            return vm;
        }
    }
}
