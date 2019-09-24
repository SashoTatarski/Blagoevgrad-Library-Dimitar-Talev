using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Services.Contracts;
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

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CheckoutBook(string id)
        {
            var user = User.Identity.Name;

            await _system.AddBookToCheckoutBooksAsync(id, user).ConfigureAwait(false);

            return RedirectToAction("Index");
        }
    }
}