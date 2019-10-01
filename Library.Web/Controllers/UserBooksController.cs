using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.AccountManagement;
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
        private readonly IAccountManager _accountManager;

        public UserBooksController(ILibrarySystem system, IBookManager bookManager, IAccountManager accountManager)
        {
            _system = system;
            _bookManager = bookManager;
            _accountManager = accountManager;
        }

        public async Task<IActionResult> Index(UserViewModel vm)
        {
            var userName = User.Identity.Name;            

            var user = await _accountManager.GetUserByUsernameAsync(userName).ConfigureAwait(false);

            vm.CheckedoutBooks = user.CheckedoutBooks.Select(x => x.MapToViewModel()).ToList();

            vm.ReservedBooks = user.ReservedBooks.Select(x => x.MapToViewModel()).ToList();

            return View(vm);
        }

        public async Task<IActionResult> ReturnBook(string id)
        {
            var user = User.Identity.Name;

            await _system.ReturnCheckedBookAsync(user, id).ConfigureAwait(false);

            TempData["message"] = Constants.RetBookSucc;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReserveBook(string id)
        {
            var username = User.Identity.Name;

            var user = await _accountManager.GetUserByUsernameAsync(username).ConfigureAwait(false);



            TempData["message"] =Constants.ResBookSucc;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckoutBook(string id)
        {
            var username = User.Identity.Name;

            var user = await _accountManager.GetUserByUsernameAsync(username).ConfigureAwait(false);

            // TODO: Да не може да взима книга, която вече е взел, т.е. Reserve бутона да не се показва в Search
            // TODO: We need to move this to the View, so it doesn't show the checkout button at all if user has already reserved 5 books
            
                await _system.AddBookToCheckoutBooksAsync(id, username).ConfigureAwait(false);
                TempData["message"] = Constants.ChBookSucc;
            
            return RedirectToAction("Index");
        }
    }
}