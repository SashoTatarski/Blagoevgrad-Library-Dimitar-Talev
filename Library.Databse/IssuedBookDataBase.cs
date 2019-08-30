using Library.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Database
{
    public class IssuedBookDataBase
    {
        private readonly LibraryContext _context;

        public IssuedBookDataBase(LibraryContext context)
        {
            _context = context;
        }
        public void AddCheckedOutBook(CheckoutBook book)
        {
            _context.CheckoutBooks.Add(book);
            _context.SaveChanges();
        }

        public void AddReservedBook(ReservedBook book)
        {
            _context.ReservedBooks.Add(book);
            _context.SaveChanges();
        }
        public void RemoveCheckedOutBook(int id)
        {
            var bookToRemove = _context.CheckoutBooks.FirstOrDefault(b => b.BookId == id);
            _context.CheckoutBooks.Remove(bookToRemove);
            _context.SaveChanges();
        }
        public void RemoveReservedBook(ReservedBook book)
        {

        }

        public List<CheckoutBook> GetCheckOutBooks(User user)
        {
            return _context.CheckoutBooks
               .Include(b => b.Book).ThenInclude(b => b.Author)
               .Include(b => b.Book).ThenInclude(b => b.Publisher)
               .Include(b => b.Book).ThenInclude(b => b.BookGenres)
                                         .ThenInclude(bg => bg.Genre)
               .Where(x => x.UserId == user.Id).ToList();
        }

        public List<ReservedBook> GetReservedBooks(User user)
        {
            return _context.ReservedBooks
              .Include(b => b.Book).ThenInclude(b => b.Author)
              .Include(b => b.Book).ThenInclude(b => b.Publisher)
              .Include(b => b.Book).ThenInclude(b => b.BookGenres)
                                        .ThenInclude(bg => bg.Genre)
              .Where(x => x.UserId == user.Id).ToList();
        }
    }
}
