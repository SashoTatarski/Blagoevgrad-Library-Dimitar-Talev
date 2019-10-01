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
        private readonly IAccountManager _accountManager;

        public UserBooksController(ILibrarySystem system, IAccountManager accountManager)
        {
            _system = system;            
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
            try
            {
                await _system.ReturnCheckedBookAsync(id, User.Identity.Name).ConfigureAwait(false);
                TempData["message"] = Constants.RetBookSucc;
            }
            catch (Exception ex)
            {
                TempData["message"] = ex;
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ReserveBook(string id)
        {
            var username = User.Identity.Name;

            var user = await _accountManager.GetUserByUsernameAsync(username).ConfigureAwait(false);

            await _system.AddBookToReservedBooksAsync(id, user);

            TempData["message"] =Constants.ResBookSucc;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckoutBook(string id)
        {
            try
            {
                await _system.AddBookToCheckoutBooksAsync(id, User.Identity.Name);
                TempData["message"] = Constants.ChBookSucc;
            }
            catch (Exception ex)
            {
                TempData["message"] = ex;
            }

            return RedirectToAction("Index");
        }
    }
}