using Library.Database;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Models.Utils;
using Library.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Services
{
    public class ConsoleFormatter : IConsoleFormatter
    {
        private readonly LibraryContext _context;
        public ConsoleFormatter(LibraryContext context)
        {
            _context = context;
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

        public string Format(User user)
        {
            var chBooks = _context.CheckoutBooks.Where(chb=>chb.UserId == user.Id).ToList();
            var resBooks = _context.ReservedBooks.Where(chb => chb.UserId == user.Id).ToList();

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

        public string CenterStringWithSymbols(string text, char symbol)
        {
            int numberOfSymbols = (GlobalConstants.MaxFieldLength - text.Length) / 2;
            string symbols = new String(symbol, numberOfSymbols);
            return GlobalConstants.NewLine + symbols + text + symbols + GlobalConstants.NewLine;
        }
    }
}
