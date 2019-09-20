using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.Contracts;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddAuthor(AddAuthorViewModel vm)
        {
            var author = _bookManager.CreateAuthor(vm.Name);

            return Redirect("AddBook");
        }

        [HttpGet]
        public IActionResult AddPublisher()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPublisher(AddPublisherViewModel vm)
        {
            var author = _bookManager.CreatePublisher(vm.Name);

            return Redirect("AddBook");
        }

        [HttpGet]
        public IActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel vm)
        {
            var genre = _bookManager.CreateGenre(vm.Name);

            return Redirect("AddBook");
        }
        public async Task<IActionResult> AddBook()
        {
            var allAuthors = await _bookManager.GetAllAuthors();
            var allPublisher = await _bookManager.GetAllPublishers();
            var allGenres = await _bookManager.GetAllGenres();
            var vm = new AddBookViewModel()
            {
                Authors = allAuthors.Select(author => new SelectListItem(author.Name, author.Id.ToString())).ToList(),
                Publishers = allPublisher.Select(pub => new SelectListItem(pub.Name, pub.Id.ToString())).ToList(),
                Genres = allGenres.Select(genre => new SelectListItem(genre.Name, genre.Id.ToString())).ToList()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult AddBook(AddBookViewModel vm)
        {
            var newBook = _bookManager.CreateBook(vm.Title, vm.ISBN, vm.Year, vm.Rack, vm.AuthorId, vm.PublisherId, vm.GenresIds);

            return RedirectToAction("Index", "Home");
        }
    }
}