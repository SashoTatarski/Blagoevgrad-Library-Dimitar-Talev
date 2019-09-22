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

        public SearchBooksController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SearchViewModel searchVM)
        {
            var allBooks = await _bookManager.GetAllBooks();
            var allBooksVM = allBooks.Select(x => x.MapToViewModel());

            searchVM.AllBooks = new List<BookViewModel>(allBooksVM);
            return View(searchVM);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(SearchViewModel viewModel)
        {
            var books = await _bookManager
                .SearchAsync(viewModel.SearchName);

            var booksMapped = books
                .Select(x => x.MapToViewModel())
                .ToList();

            foreach (var book in booksMapped)
            {
                if (!viewModel.BookSearchResults.Any(x => x.ISBN == book.ISBN))
                {
                    viewModel.BookSearchResults.Add(book);
                }
            }

            return View(viewModel);
        }
    }
}