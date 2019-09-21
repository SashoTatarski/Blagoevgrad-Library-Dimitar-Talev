using Library.Services.Contracts;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Library.Web.Mapper;

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
            var bookToDelete = await _bookManager.GetBookAsync(id);

            await _bookManager.DeleteAsync(id);

            TempData["message"] = $"{bookToDelete.Title} - {bookToDelete.Author.Name} has been deleted";

            return RedirectToAction("Index", "SearchBooks");
        }


        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthor(AuthorViewModel vm)
        {
            var author = await _bookManager.CreateAuthorAsync(vm.AuthorName);

            if (vm.Id == null)
                return RedirectToAction("AddBook", "BookManagement");
            else
                return RedirectToAction("EditBook", "BookManagement", new { id = vm.Id });
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
        private BookViewModel BookViewModel { get; set; }

        public async Task<IActionResult> AddBook()
        {
            var allAuthors = await _bookManager.GetAllAuthorsAsync();
            var allPublisher = await _bookManager.GetAllPublishersAsync();
            var allGenres = await _bookManager.GetAllGenresAsync();

            if (this.BookViewModel == null)
            {
                this.BookViewModel = new BookViewModel();
            }
            this.BookViewModel.Authors = allAuthors.Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList();
            this.BookViewModel.Publishers = allPublisher.Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList();
            this.BookViewModel.GenresOptions = allGenres.Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList();

            return View(this.BookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel vm)
        {
            await _bookManager.CreateBookAsync(vm.Title, vm.ISBN, vm.Year, vm.Rack, vm.AuthorId, vm.PublisherId, vm.GenresIds, vm.BookCopies);

            TempData["message"] = $"{vm.Title} has been created";
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AuthorDetails(AuthorViewModel vm)
        {
            var author = await _bookManager.GetAuthorAsync(vm.Id);
            var books = await _bookManager.GetBooksByAuthorAsync(vm.Id);

            vm.AuthorName = author.Name;
            vm.Books = books.Select(x => x.MapToViewModel()).ToList();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> BookDetails(string id)
        {
            var book = await _bookManager.GetBookAsync(id);

            var vm = book.MapToViewModel();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(string id)
        {
            var book = await _bookManager.GetBookAsync(id);
            var vm = book.MapToViewModel();

            var allAuthors = await _bookManager.GetAllAuthorsAsync();
            var allPublisher = await _bookManager.GetAllPublishersAsync();
            var allGenres = await _bookManager.GetAllGenresAsync();

            vm.Authors = allAuthors.Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList();
            vm.Publishers = allPublisher.Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList();
            vm.GenresOptions = allGenres.Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList();

            vm.Authors.Where(a => a.Value == vm.Author.Id.ToString()).FirstOrDefault().Selected = true;
            vm.Publishers.Where(p => p.Value == vm.Publisher.Id.ToString()).FirstOrDefault().Selected = true;

            foreach (var listItem in vm.GenresOptions)
            {
                foreach (var genre in vm.Genres)
                {
                    if (listItem.Value == genre.Id.ToString())
                    {
                        listItem.Selected = true;
                    }
                }
            }

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(BookViewModel vm)
        {



            TempData["message"] = $"{vm.Title} has been editted";
            return RedirectToAction("Index", "Home");
        }
    }
}