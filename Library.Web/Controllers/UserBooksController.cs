﻿using System;
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
            
                await _system.AddBookToCheckoutBooksAsync(id, username).ConfigureAwait(false);
                TempData["message"] = Constants.ChBookSucc;
            
            return RedirectToAction("Index");
        }
    }
}