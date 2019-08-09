﻿using Library.Models.Contracts;
using Library.Models.Enums;
using System;

namespace Library.Services.Contracts
{
    public interface IBookManager
    {
        void UpdateBook(int bookId, string authorName, string title, string isbn, string category, string publisher, int year, int rack);
        void UpdateBook(int bookId, BookStatus status, DateTime today, DateTime dueDate);
        int GetLastBookID();
        void AddBook(IBook book);
        void ListAllBooks();
        IBook FindBook(int id);
        void RemoveBook(IBook book);
    }
}