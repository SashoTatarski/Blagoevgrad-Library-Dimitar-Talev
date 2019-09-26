using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class UserBooksController : Controller
    {
        private readonly ILibrarySystem _system;
        private readonly IBookManager _bookManager;
        public UserBooksController(ILibrarySystem system, IBookManager bookManager)
        {
            _system = system;
            _bookManager = bookManager;
        }

        public async Task<IActionResult> Index(ReservationsViewModel vm)
        {
            var user = User.Identity.Name;

            var chBooks = await _system.GetCheckeoutBooks(user).ConfigureAwait(false);

            vm.CheckedoutBooks = chBooks.Select(x => x.MapToViewModel()).ToList();

            return View(vm);
        }

        public async Task<IActionResult> ReturnBook(string id)
        {
            var user = User.Identity.Name;

            await _system.ReturnBook(user, id);

            TempData["message"] = $"Book Successfully returned";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckoutBook(string id)
        {
            var user = User.Identity.Name;

            await _system.AddBookToCheckoutBooksAsync(id, user).ConfigureAwait(false);

            return RedirectToAction("Index");
        }
    }
}