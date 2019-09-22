using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public AccountManagementController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public async Task <IActionResult> Index(AccountViewModel vm)
        {
            var users = await _accountManager.GetAllUsersAsync();
            
            var usersMapped = users
                .Select(u => u.MapToViewModel())
                .ToList();

            foreach (var user in usersMapped)
            {
                vm.User.Add(user);
            }
            
            
            return View(vm);
        }

    }
}