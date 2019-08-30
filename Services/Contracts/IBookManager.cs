using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using System;
using System.Collections.Generic;

namespace Library.Services.Contracts
{
    public interface IBookManager
    {
        Book CreateBook(string authorName, string title, string isbn, string genres, string publisher, int year, int rack);
        void ListAllBooks();

        // ------- Need update ↓ -------
        void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack);
        void UpdateBook(int bookId, BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool isReservation);
        void UpdateBook(int bookId, BookStatus status, DateTime today, DateTime dueDate);        
        Book FindBook(int id);
        void RemoveBook(Book book);        
        int GetLastBookID();
        List<Book> GetSearchResult(string searchByParameter, string searchByText);
        string GetCheckedoutBooksInfo(IUser user);
        string GetOverdueBooksInfo(IUser user);
        void UpdateStatus(IBook book, BookStatus status);
        void UpdateBookAuthor(int bookId, string newAuthorName);
        void UpdateBookTitle(int bookId, string newTitle);
        void UpdateBookISBN(int bookId, string newISBN);
        void UpdateBookRack(int bookId, int newRack);
        void UpdateBookYear(int bookId, int newYear);
        void UpdateBookPublisher(int bookId, string newPublisherName);
    }
}
