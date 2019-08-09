using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Models.Models
{
    public class User : Account, IUser
    {
        public User(string username, string password) : base(username, password)
        {
            this.Status = MemberStatus.Active;
            this.CheckedOutBooks = new List<IBook>();
            this.ReservedBooks = new List<IBook>();
            this.ReservedBookMessages = new List<string>();
            this.OverdueMessages = new List<string>();
        }

        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "Check Out Book",
                "Return Book",
                "Renew Book",
                "Reserve Book",
                "Remove Reservation",
                "Search",
                "View Account",
                "Log Out"
            };
        public MemberStatus Status { get; set; }

        public List<IBook> CheckedOutBooks { get; private set; }

        public List<IBook> ReservedBooks { get; private set; }

        public List<string> ReservedBookMessages { get; set; }

        public List<string> OverdueMessages { get; set; }

        public decimal LateFees { get; set; }

        public void AddBookToCheckoutBooks(IBook book)
        {
            this.CheckedOutBooks.Add(book);
        }

        public void RemoveFromReservedBooks(IBook book)
        {
            this.ReservedBooks.Remove(book);
        }

        public void RemoveFromCheckedoutBooks(IBook book)
        {
            this.CheckedOutBooks.RemoveAll(b => b.ID == book.ID);
        }

        public void Update(IUser otherUser)
        {
            this.Status = otherUser.Status;
            this.CheckedOutBooks = otherUser.CheckedOutBooks;
            this.ReservedBooks = otherUser.ReservedBooks;
            this.ReservedBookMessages = otherUser.ReservedBookMessages;
            this.OverdueMessages = otherUser.OverdueMessages;
            this.LateFees = otherUser.LateFees;
        }

        public string DisplayCheckedoutBooks()
        {
            var strBuilder = new StringBuilder();

            if (this.CheckedOutBooks.Count == 0)
            {
                throw new ArgumentException("There are no checked out books!");
            }

            else
            {
                strBuilder.AppendLine("Books you have checked out:");

                foreach (var book in this.CheckedOutBooks)
                {
                    // If a book is overdue it's printed in red
                    if (book.DueDate < VirtualDate.VirtualToday)
                        Console.ForegroundColor = ConsoleColor.Red;

                    strBuilder.AppendLine($"ID: {book.ID} || Title: {book.Title} || Author: {book.Author} || CheckedOut Date: {book.CheckoutDate.ToString("dd MM yyyy")} || Due Date: {book.DueDate.ToString("dd MM yyyy")}");

                    Console.ResetColor();
                }
            }
            return strBuilder.ToString();
        }
    }
}