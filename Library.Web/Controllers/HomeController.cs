using Library.Services.Contracts;
using Library.Web.Models;
using Library.Web.Models.HomeModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using Library.Web.Mapper;
using Library.Web.Models.BookViewModels;
using System.Collections.Generic;

namespace Library.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookManager _bookManager;

        public HomeController(IBookManager bookManager)
        {
            _bookManager = bookManager;
        }
        public async Task<IActionResult> Index()
        {
            var topRatedBooks = await _bookManager.GetTopRatedBooks(6).ConfigureAwait(false);

            var vm = new HomeBooksViewModel()
            {
                Books = new List<GenericBookViewModel>()
            };

            topRatedBooks.ForEach(b => vm.Books.Add(b.MapToGenericViewModel()));

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
