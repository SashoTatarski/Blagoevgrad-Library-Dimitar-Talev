using Library.Core.Contracts;
using Library.Models.Contracts;
using System.Collections.Generic;

using System.Text;

namespace Library.Core
{
    public class Formatter : IFormatter
    {
        public string Format(IBook book)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"ID: {book.ID}");
            strBuilder.AppendLine($"Title: {book.Title} || Author: {book.Author}");
            strBuilder.AppendLine($"Genre: {book.Genre}");
            strBuilder.AppendLine($"Publisher: {book.Publisher} || Year: {book.Year} || Location: {book.Rack} rack");
            strBuilder.AppendLine($"Status: {book.Status}");

            return strBuilder.ToString();
        }

        public string Format(IUser user)
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine($"Username: {user.Username}");
            strBuilder.AppendLine($"Checked out books:"); 
            strBuilder.AppendLine(this.FormatListOfBooks(user.CheckedOutBooks));
            strBuilder.AppendLine($"Reserved books:");
            strBuilder.AppendLine(this.FormatListOfBooks(user.ReservedBooks));

            return strBuilder.ToString();
        }

        public string FormatListOfBooks(List<IBook> books)
        {
            var strBuilder = new StringBuilder();

            if (books.Count==0)
            {
                return "There are no books!";
            }
            foreach (var book in books)
            {
                strBuilder.AppendLine(this.Format(book));
            }
            return strBuilder.ToString();
        }
    }
}
