using Library.Models.Models;
using Library.Web.Models.AccountManagement;
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

            vm.AllBookCopies = new List<BookCopyViewModel>();

            return vm;
        }

        public static UserViewModel MapToViewModel(this User user)
        {
            var vm = new UserViewModel();
            vm.UserId = user.Id.ToString();
            vm.Username = user.Username;
            vm.MembershipEndDate = user.MembershipStartDate;
            vm.MembershipEndDate = user.MembershipEndDate;
            vm.Status = user.Status;
            vm.Wallet = user.Wallet;

            return vm;
        }

        public static BookCopyViewModel MapToCopyViewModel(this Book book)
        {
            var vm = new BookCopyViewModel();
            vm.Title = book.Title;
            vm.Status = book.Status.ToString();

            if (book.ReservedBooks != null)
            {
                var resUsers = new List<User>();
                book.ReservedBooks.ForEach(x => resUsers.Add(x.User));
                vm.ReservedBy = resUsers;
            }

            if (book.CheckedoutBook != null)
            {
                vm.CheckedOutBy = book.CheckedoutBook.User;
            }

            return vm;
        }
    }
}
