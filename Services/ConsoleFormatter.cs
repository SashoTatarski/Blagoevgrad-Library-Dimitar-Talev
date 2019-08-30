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

namespace Library.Services
{
    public class ConsoleFormatter : IConsoleFormatter
    {
        private readonly IDatabase<CheckoutBook> _checkoutBooks;
        private readonly IDatabase<ReservedBook> _reservedBooks;
        private readonly IDatabase<Book> _books;


        public ConsoleFormatter(IDatabase<CheckoutBook> checkoutBooks, IDatabase<ReservedBook> reservedBooks, IDatabase<Book> books)
        {
            _checkoutBooks = checkoutBooks;
            _reservedBooks = reservedBooks;
            _books = books;
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

        public string FormatCheckedoutBook(Book book)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author.Name}");
            strBuilder.AppendLine($"CheckedOut Date: {book.CheckedoutBook.CheckoutDate.ToString("dd MM yyyy")}");
            strBuilder.AppendLine($"Due Date: {book.CheckedoutBook.DueDate.ToString("dd MM yyyy")}");

            return strBuilder.ToString();
        }

        public string FormatReservedBook(Book book)
        {
            var strBuilder = new StringBuilder();

            strBuilder.AppendLine($"ID: {book.Id} || Status: {book.Status}");
            strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author.Name}");
            strBuilder.AppendLine($"Reservation Date: {book.ReservedBook.ReservationDate.ToString("dd MM yyyy")}");
            strBuilder.AppendLine($"Due Date: {book.ReservedBook.ReservationDueDate.ToString("dd MM yyyy")}");

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

            var allbooks = _books.Read();

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
            var chBooks = _checkoutBooks.Read();
            var userHasCheckedBooks = chBooks.Where(x => x.UserId == user.Id)
                                             .Select(x => x.BookId)
                                             .ToList();

            var resBooks = _reservedBooks.Read();
            var userHasResBooks = resBooks.Where(x => x.UserId == user.Id)
                                          .Select(x => x.BookId)
                                          .ToList();


            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Username: {user.Username}");
            strBuilder.AppendLine($"Checked out books:");
            strBuilder.AppendLine(this.FormatListOfBooks(userHasCheckedBooks));
            strBuilder.AppendLine($"Reserved books:");
            strBuilder.AppendLine(this.FormatListOfBooks(userHasResBooks));

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
