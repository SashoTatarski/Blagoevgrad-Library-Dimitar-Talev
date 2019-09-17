using Library.Database;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Services
{
    public class LibrarySystem : ILibrarySystem
    {
        private readonly IBookManager _bookManager;
        private readonly LibraryContext _context;


        public LibrarySystem(IBookManager bookManager, LibraryContext context)
        {
            _bookManager = bookManager;
            _context = context;
        }

        public CheckoutBook AddBookToCheckoutBooks(Book book, User user) => throw new NotImplementedException();
        public ReservedBook AddBookToReservedBooks(Book book, User user) => throw new NotImplementedException();
        public void CheckCheckoutBooksQuota(User user) => throw new NotImplementedException();
        public void CheckReservedBooksQuota(User user) => throw new NotImplementedException();
        public List<CheckoutBook> GetCheckedOutBooks(User user) => throw new NotImplementedException();
        public bool HasIssuedBooks(User user) => throw new NotImplementedException();
        public void ManageOverdueReservations() => throw new NotImplementedException();
        public void RemoveBookFromCheckoutBooks(Book book) => throw new NotImplementedException();
    }
}

