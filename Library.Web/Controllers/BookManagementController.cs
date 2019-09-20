using Library.Services.Contracts;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class BookManagementController : Controller
    {
        private readonly IBookManager _bookManager;
        public BookManagementController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public async Task<IActionResult> Delete(string id)
        {
            var bookToDelete = await _bookManager.GetBook(id);

            await _bookManager.DeleteAsync(id);
            
            TempData["message"] = $"{bookToDelete.Title} has been deleted";

            return RedirectToAction("Index", "SearchBooks");
        }


        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AddAuthorViewModel vm)
        {
            var author = await _bookManager.CreateAuthorAsync(vm.Name);

            return RedirectToAction("AddBook", "BookManagement");
        }

        [HttpGet]
        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPublisher(AddPublisherViewModel vm)
        {
            var author = _bookManager.CreatePublisherAsync(vm.Name);

            return RedirectToAction("AddBook", "BookManagement");
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel vm)
        {
            var genre = _bookManager.CreateGenreAsync(vm.Name);

            return RedirectToAction("AddBook", "BookManagement");
        }
        private AddBookViewModel BookViewModel { get; set; }

        public async Task<IActionResult> AddBook()
        {
            var allAuthors = await _bookManager.GetAllAuthorsAsync();
            var allPublisher = await _bookManager.GetAllPublishersAsync();
            var allGenres = await _bookManager.GetAllGenresAsync();

            if(this.BookViewModel == null)
            {
                this.BookViewModel = new AddBookViewModel();
            }
            this.BookViewModel.Authors = allAuthors.Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList();
            this.BookViewModel.Publishers = allPublisher.Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList();
            this.BookViewModel.GenresOptions = allGenres.Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList();
            
            return View(this.BookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookViewModel vm)
        {
            await _bookManager.CreateBookAsync(vm.Title, vm.ISBN, vm.Year, vm.Rack, vm.AuthorId, vm.PublisherId, vm.GenresIds, vm.BookCopies);
            
            TempData["message"] = $"{vm.Title} has been created";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RemoveBook()
        {
            return null;
        }
    }
}