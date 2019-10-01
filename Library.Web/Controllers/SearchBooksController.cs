﻿using Library.Services.Contracts;
using Library.Web.Mapper;
using Library.Web.Models.BookManagement;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Controllers
{
    public class SearchBooksController : Controller
    {
        private readonly IBookManager _bookManager;
        private readonly IAccountManager _accountManager;
        private readonly ILibrarySystem _system;


        public SearchBooksController(IBookManager bookManager, IAccountManager accountManagager, ILibrarySystem system)
        {
            _bookManager = bookManager;
            _accountManager = accountManagager;
            _system = system;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SearchViewModel searchVM)
        {
            var allBooks = await _bookManager.GetAllBooksAsync().ConfigureAwait(false);
            var allBooksVM = allBooks.Select(x => x.MapToViewModel());

            var user = await _accountManager.GetUserByUsernameAsync(User.Identity.Name).ConfigureAwait(false);

            searchVM.AllBooks = new List<BookViewModel>();          
                       

            foreach (var book in allBooksVM)
            {
                if (!searchVM.AllBooks.Any(x => x.ISBN == book.ISBN))
                {
                    book.IsBookCheckedout = _system.IsBookCheckedout(user, book.ISBN);
                    book.IsChBooksMaxQuota = _system.IsMaxCheckedoutQuota(user);
                    book.BookCopies = await _bookManager.BookCopiesCountAsync(book.ISBN).ConfigureAwait(false);
                    searchVM.AllBooks.Add(book);
                }
            }

            return View(searchVM);
        }

        [HttpPost]
        public async Task<IActionResult> SearchResults(SearchViewModel viewModel)
        {
            var books = await _bookManager
                .SearchAsync(viewModel.SearchName).ConfigureAwait(false);
            var booksMapped = books
                .Select(x => x.MapToViewModel())
                .ToList();

            foreach (var book in booksMapped)
            {
                if (!viewModel.BookSearchResults.Any(x => x.ISBN == book.ISBN))
                {
                    viewModel.BookSearchResults.Add(book);
                }
            }

            return View(viewModel);
        }
    }
}