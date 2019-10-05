using Library.Models.Models;
using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class SearchBooksController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;

        public SearchBooksController(IBookManager bookManager, IAccountManager accountManagager, ILibrarySystem system)
        {
            _bookManager = bookManager;
            _accountManager = accountManagager;
            _system = system;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allBooks = await _bookManager.GetAllBooksAsync();
            var allBooksVM = allBooks.Select(x => x.MapToViewModel());

            var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name);

            var searchVM = new SearchViewModel()
            {
                AllBooks = new List<BookViewModel>(),
                BookSearchResults = new List<BookViewModel>()
            };

            await SearchViewModelMapper(searchVM, allBooksVM, user);

            return View(searchVM);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(SearchViewModel viewModel)
        {
            var bookResult = await _bookManager
                .SearchAsync(viewModel.SearchName.ToLower(), viewModel.ByTitle, viewModel.ByAuthor, viewModel.ByPublisher, viewModel.ByGenre);
            var booksMapped = bookResult
                .Select(x => x.MapToViewModel())
                .ToList();
            var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name);

            await this.SearchResultViewModelMapper(viewModel, booksMapped, user);

            return View(viewModel);
        }

        private async Task SearchViewModelMapper(SearchViewModel searchVM, IEnumerable<BookViewModel> allBookCopies, User user)
        {
            foreach (var book in allBookCopies)
            {
                if (!searchVM.AllBooks.Any(x => x.ISBN == book.ISBN))
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        book.IsBookCheckedout = _system.IsBookCheckedout(user, book.ISBN);
                        book.IsChBooksMaxQuota = _system.IsMaxCheckedoutQuota(user);
                        book.AreAllCopiesChecked = await _system.AreAllCopiesCheckedAsync(book.ISBN);
                    }

                    book.BookCopies = await _bookManager.BookCopiesCountAsync(book.ISBN);
                    book.StatusLoggedUser = user.Status.ToString();
                    searchVM.AllBooks.Add(book);
                }
            }
        }

        private async Task SearchResultViewModelMapper(SearchViewModel searchVM, IEnumerable<BookViewModel> searchResult, User user)
        {
            foreach (var book in searchResult)
            {
                if (!searchVM.BookSearchResults.Any(x => x.ISBN == book.ISBN))
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        book.IsBookCheckedout = _system.IsBookCheckedout(user, book.ISBN);
                        book.IsChBooksMaxQuota = _system.IsMaxCheckedoutQuota(user);
                        book.AreAllCopiesChecked = await _system.AreAllCopiesCheckedAsync(book.ISBN);
                    }

                    book.BookCopies = await _bookManager.BookCopiesCountAsync(book.ISBN);
                    book.StatusLoggedUser = user.Status.ToString();
                    searchVM.BookSearchResults.Add(book);
                }
            }
        }
    }
}