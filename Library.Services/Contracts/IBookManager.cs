using Library.Models.Enums;
using Library.Models.Models;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IBookManager
    {
        Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack);
        void ListAllBooks();
        List<int> GetBooksIDs();
        Book FindBook(int id);
        void RemoveBook(Book book);
        List<Book> GetSearchResult(string searchByParameter, string searchByText);
        void UpdateBookAuthor(int bookId, string newAuthorName);
        void UpdateBookTitle(int bookId, string newTitle);
        void UpdateBookISBN(int bookId, string newISBN);
        void UpdateBookRack(int bookId, int newRack);
        void UpdateBookYear(int bookId, int newYear);
        void UpdateBookPublisher(int bookId, string newPublisherName);
        void UpdateBookGenre(int bookId, string newGenres);
        void ChangeBookStatus(Book book, BookStatus status);
        List<Book> GetAllBooks();
    }
}