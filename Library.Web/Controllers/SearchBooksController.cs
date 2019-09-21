using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SearchViewModel viewModel)
        {
            var books = _bookManager
                .Search(viewModel.SearchName)
                .Select(x => x.MapToViewModel())
                .ToList();

            foreach (var book in books)
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