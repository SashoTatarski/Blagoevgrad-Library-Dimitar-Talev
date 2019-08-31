using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using Library.Services.Contracts;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Library.Models.Models;
using System;
using Library.Database.Contracts;
using Library.Database;

namespace Library.Services
{
    public class ConsoleFormatter : IConsoleFormatter
    {
        private readonly IDatabase<Book> _booksDB;
        private readonly IssuedBookDataBase _issuedBookDB;


        public ConsoleFormatter(IDatabase<Book> booksDB, IssuedBookDataBase issuedBookDB)
        {
            _booksDB = booksDB;
            _issuedBookDB = issuedBookDB;
        }

        public string Format(Book book)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author.Name}");
            strBuilder.Append($"Genre: ");
            foreach (var genre in book.BookGenres)
            {
                strBuilder.Append($"{genre.Genre.GenreName} ");
            }
            strBuilder.AppendLine();
            strBuilder.AppendLine($"Publisher: {book.Publisher.Name} || Year: {book.Year} || Location: {book.Rack} rack");

            return strBuilder.ToString();
        }

        public string Format(CheckoutBook book)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"ID: {book.Book.Id} || Status: {book.Book.Status}");
            strBuilder.AppendLine($"Title: {book.Book.Title} || Author: {book.Book.Author.Name}");
            strBuilder.AppendLine($"CheckedOut Date: {book.CheckoutDate.ToString("dd-MM-yyyy")}");
            strBuilder.AppendLine($"Due Date: {book.DueDate.ToString("dd-MM-yyyy")}");

            return strBuilder.ToString();
        }

        public string Format(ReservedBook book)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"ID: {book.Book.Id} || Status: {book.Book.Status}");
            strBuilder.AppendLine($"Title: {book.Book.Title} || Author: {book.Book.Author.Name}");
            strBuilder.AppendLine($"Reservation Date: {book.ReservationDate.ToString("dd-MM-yyyy")}");
            strBuilder.AppendLine($"Due Date: {book.ReservationDueDate.ToString("dd-MM-yyyy")}");

            return strBuilder.ToString();
        }

        public string FormatList(List<CheckoutBook> books)
        {
            var strBuilder = new StringBuilder();

            if (books.Count == 0)
                return "There are no books!";

            foreach (var book in books)
                strBuilder.AppendLine(this.Format(book));

            return strBuilder.ToString();
        }

        public string FormatList(List<ReservedBook> books)
        {
            var strBuilder = new StringBuilder();

            if (books.Count == 0)
                return "There are no books!";

            foreach (var book in books)
                strBuilder.AppendLine(this.Format(book));

            return strBuilder.ToString();
        }

        //Update ---------------

        public string Format(IAccount account)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Username: {account.Username}");

            return strBuilder.ToString();
        }



        public string FormatListOfBooks(List<Book> books)
        {
            var strBuilder = new StringBuilder();

            if (books.Count == 0)
                return "There are no books!";

            foreach (var book in books)
                strBuilder.AppendLine(this.Format(book));

            return strBuilder.ToString();

        }

        public string FormatListOfBooks(List<int> booksIds)
        {
            if (booksIds.Count == 0)
                return "There are no books!";

            var allbooks = _booksDB.Read();

            var strBuilder = new StringBuilder();
            foreach (var book in from book in allbooks
                                 from bookId in booksIds
                                 where book.Id == bookId
                                 select book)
            {
                strBuilder.AppendLine(this.Format(book));
            }

            return strBuilder.ToString();
        }



        public string FormatCommandMessage(string message, string modelInfo)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine();
            strBuilder.AppendLine(GlobalConstants.Delimiter);
            strBuilder.AppendLine(message);
            strBuilder.AppendLine(GlobalConstants.MiniDelimiter);
            strBuilder.Append(modelInfo);
            strBuilder.AppendLine(GlobalConstants.Delimiter);

            return strBuilder.ToString();
        }

        public string FormatCommandMessage(string message)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine();
            strBuilder.AppendLine(GlobalConstants.Delimiter);
            strBuilder.AppendLine(message);
            strBuilder.AppendLine(GlobalConstants.Delimiter);

            return strBuilder.ToString();
        }

        public string Format(User user)
        {
            var chBooks = _issuedBookDB.GetCheckOutBooks(user);
            var resBooks = _issuedBookDB.GetReservedBooks(user);

            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Username: {user.Username}");
            strBuilder.AppendLine($"Checked out books:");
            strBuilder.AppendLine(this.FormatList(chBooks));
            strBuilder.AppendLine($"Reserved books:");
            strBuilder.AppendLine(this.FormatList(resBooks));

            return strBuilder.ToString();
        }

        public string FormatListOfUsers(List<User> users)
        {
            var strBuilder = new StringBuilder();

            if (users.Count == 0)
                return GlobalConstants.NoUsers;

            foreach (var user in users)
            {
                if (user.Status != AccountStatus.Inactive)
                {
                    strBuilder.Append(this.Format(user));
                    strBuilder.AppendLine(GlobalConstants.MiniDelimiter);
                }
            }
            return strBuilder.ToString();
        }

        public string FormatListOfUsersShort(List<User> users)
        {
            var strBuilder = new StringBuilder();

            if (users.Count == 0)
                return GlobalConstants.NoUsers;

            //foreach (var user in users)
            //{
            //    if (user.Status != MemberStatus.Inactive)
            //        strBuilder.Append(this.FormatShort(user));
            //}

            return strBuilder.ToString();
        }

        //private string FormatShort(IUser user) => $"Username: {user.Username} || CheckedOut Books: {user.c.Count} || Reserved Books: {user.ReservedBooks.Count}\r\n";

        public string CenterStringWithSymbols(string text, char symbol)
        {
            int numberOfSymbols = (GlobalConstants.MaxFieldLength - text.Length) / 2;
            string symbols = new String(symbol, numberOfSymbols);
            return GlobalConstants.NewLine + symbols + text + symbols + GlobalConstants.NewLine;
        }

    }
}
