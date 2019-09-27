using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Models.Utils;
using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.AccountManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class AccountManagementController : Controller
    {
        private readonly IAccountManager _accountManager;
        private readonly IBookManager _bookManager;

        public AccountManagementController(IAccountManager accountManager, IBookManager bookManager)
        {
            _accountManager = accountManager;
            _bookManager = bookManager;
        }

        public async Task<IActionResult> Index(AccountViewModel vm)
        {
            var users = await _accountManager.GetAllUsersAsync().ConfigureAwait(false);

            var usersMapped = users
                .Select(u => u.MapToViewModel())
                .ToList();

            foreach (var user in usersMapped)
            {
                vm.User.Add(user);
            }

            return View(vm);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _accountManager.DeleteUserAsync(id).ConfigureAwait(false);

            TempData["message"] = Constants.AcctDeact;

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Activate(string id)
        {
            var accountToActivate = await _accountManager.ActivateUserAsync(id).ConfigureAwait(false);

            TempData["message"] = $"{accountToActivate.Username} has been activated";

            return RedirectToAction("Index", "AccountManagement");
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _accountManager.GetUserByIdAsync(id).ConfigureAwait(false);

            var vm = user.MapToViewModel();

            return View(vm);
        }
    }
}