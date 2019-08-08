using Library.Database;
using Library.Models.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Dawn;
using Library.Models.Models;

namespace Library.Services.Contracts
{
    public interface IBookManager
    {
        void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack);
    }

    public class BookManager : IBookManager
    {
        private readonly IBookDatabase _database;
        public BookManager(IBookDatabase database, IBookFactory bookfactory)
        {
            _database = database;
        }
        public void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack)
        {

            var bookToUpdate = _database.Get(bookId);
            Guard.Argument(bookToUpdate, nameof(bookToUpdate)).NotNull(message: "Book to update is null");

            // KiroKaza fix this
            var updated = new Book(bookId, authorName, title, isbn, category, publisher, year, rack);
         
            _database.Update(updated);
        }
    }
}
