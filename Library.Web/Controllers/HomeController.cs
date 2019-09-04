using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Web.Models;
using Library.Services.Contracts;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        public HomeController(IBookManager bookManager, IAccountManager accountManager)
        {
            _bookManager = bookManager;
            _accountManager = accountManager;
        }
        public IActionResult Index()    
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
       

        //public IActionResult Accounts()
        //{
        //    var users = _accountManager.GetAllUsers();

        //    var accountsViewModel = new AccountsViewModel(users);

        //    return View(accountsViewModel);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
