using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "admin, user")]
    public class BookManagementController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;
        public BookManagementController(IBookManager bookManager, IAccountManager accountManager, ILibrarySystem system)
        {
            _bookManager = bookManager;
            _accountManager = accountManager;
            _system = system;
        }

        public BookViewModel BookViewModel { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCopies(string id)
        {
            var vm = new BookViewModel();
            vm.BookId = id;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddCopies(BookViewModel vm)
        {
            await _bookManager.AddBookCopies(vm.BookId, vm.BookCopies);

            TempData["message"] = $"{vm.BookCopies} copies have been added";

            return RedirectToAction("Index", "SearchBooks");
        }

        public async Task<IActionResult> Delete(string id)
        {
            var bookToDelete = await _bookManager.GetBookByIsbnAsync(id);

            await _bookManager.DeleteBookAsync(id);

            TempData["message"] = $"{bookToDelete.Title} has been deleted";

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
                return RedirectToAction("AddBook");
            else
                return RedirectToAction("EditBook", new { id = vm.Id });
        }

        [HttpPost]
        public async Task<IActionResult> AddAuthorRest(string authorName)
        {
            var author = await _bookManager.CreateAuthorAsync(authorName);

            return Json(new { author.Id, author.Name});
        }

        [HttpGet]
        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddPublisher(AddPublisherViewModel vm)
        {
            await _bookManager.CreatePublisherAsync(vm.Name);

            return RedirectToAction("AddBook", "BookManagement");
        }


        [HttpPost]
        public async Task<IActionResult> AddPublisherRest(string publisherName)
        {
            var publisher = await _bookManager.CreatePublisherAsync(publisherName);

            return Json(new { publisher.Id, publisher.Name });
        }



        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGenre(AddGenreViewModel vm)
        {
            var genre = await _bookManager.CreateGenreAsync(vm.Name);

            return RedirectToAction("AddBook", "BookManagement");
        }

        [HttpPost]
        public async Task<IActionResult> AddGenreRest(string genreName)
        {
            var genre = await _bookManager.CreateGenreAsync(genreName);

            return Json(new {genre.Id, genre.Name });
        }

        private async Task CreateSelectListItems()
        {
            var allAuthors = await _bookManager.GetAllAuthorsAsync();
            var allPublisher = await _bookManager.GetAllPublishersAsync();
            var allGenres = await _bookManager.GetAllGenresAsync();

            if (this.BookViewModel == null)
            {
                this.BookViewModel = new BookViewModel();
            }

            this.BookViewModel.Authors = allAuthors.OrderBy(a => a.Name).Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList();
            this.BookViewModel.Publishers = allPublisher.OrderBy(p => p.Name).Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList();
            this.BookViewModel.GenresOptions = allGenres.OrderBy(g => g.Name).Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList();
        }

        [HttpGet]
        public async Task<IActionResult> AddBook()
        {
            await this.CreateSelectListItems();

            return View(this.BookViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel vm)
        {
            if (!_bookManager.isIsbnUnique(vm.ISBN))
            {
                await _bookManager.CreateBookAsync(vm.Title, vm.ISBN, vm.Year, vm.Rack, vm.AuthorId, vm.PublisherId, vm.GenresIds, vm.BookCopies);

                TempData["message"] = $"{vm.Title} has been created";
                return RedirectToAction("Index", "SearchBooks");
            }
            else
            {
                TempData["message"] = $"{vm.ISBN} is associated with another book!";
                return RedirectToAction("AddBook");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> AuthorDetails(AuthorViewModel vm)
        {
            var author = await _bookManager.GetAuthorAsync(vm.Id);

            var books = await _bookManager.GetBooksByAuthorAsync(vm.Id);

            vm.AuthorName = author.Name;
            vm.Books = books.Select(x => x.MapToViewModel()).ToList();

            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BookDetails(string id)
        {
            var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name);
            var books = await _bookManager.GetBooksByIsbnAsync(id);

            var vm = books[0].MapToViewModel();

            foreach (var book in books)
            {
                vm.AllBookCopies.Add(book.MapToCopyViewModel());
            }

            vm.IsBookCheckedout = _system.IsBookCheckedout(user, vm.ISBN);
            vm.AreAllCopiesChecked = await _system.AreAllCopiesCheckedAsync(vm.ISBN);
            vm.IsChBooksMaxQuota = _system.IsMaxCheckedoutQuota(user);
            vm.StatusLoggedUser = user.Status.ToString();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> EditBook(string id)
        {
            var book = await _bookManager.GetBookByIdAsync(id);
            var vm = book.MapToViewModel();

            var allAuthors = await _bookManager.GetAllAuthorsAsync();
            var allPublisher = await _bookManager.GetAllPublishersAsync();
            var allGenres = await _bookManager.GetAllGenresAsync();

            vm.Authors = allAuthors.Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList();
            vm.Publishers = allPublisher.Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList();
            vm.GenresOptions = allGenres.Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList();

            // Both are the same
            vm.Authors.Find(a => a.Value == vm.Author.Id.ToString()).Selected = true;
            vm.Publishers.Find(p => p.Value == vm.Publisher.Id.ToString()).Selected = true;

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
            await _bookManager.EditBookAsync(vm.BookId, vm.Title, vm.ISBN, vm.Year, vm.Rack, vm.AuthorId, vm.PublisherId, vm.GenresIds);


            TempData["message"] = $"{vm.Title} has been editted";
            return RedirectToAction("Index", "Home");
        }
    }
}