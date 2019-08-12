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
        // OOP: User class inherits Account base class
        // OOP: Encapsulation - properties with private set 
        public User(string username, string password) : base(username, password)
        {
            this.Status = MemberStatus.Active;
            this.CheckedOutBooks = new List<IBook>();
            this.ReservedBooks = new List<IBook>();
            this.OverdueReservations = new List<IBook>();
            this.OverdueBooks = new List<IBook>();
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
                "Travel In Time",
                "Log Out"
            };
        public MemberStatus Status { get; set; }

        public List<IBook> CheckedOutBooks { get; private set; }

        public List<IBook> ReservedBooks { get; private set; }

        public List<IBook> OverdueReservations { get; set; }

        public List<IBook> OverdueBooks { get; set; }

        public decimal LateFees { get; set; }



        public void RemoveFromCheckedoutBooks(IBook book) => this.CheckedOutBooks.RemoveAll(b => b.ID == book.ID);
        public void RemoveFromReservedBooks(IBook book) => this.ReservedBooks.Remove(book);

        public void RemoveFromOverdueBooks(IBook book) => this.OverdueBooks.RemoveAll(b => b.ID == book.ID);

        public void RemoveFromOverdueReservations(IBook book) => this.OverdueReservations.RemoveAll(b => b.ID == book.ID);

        public void AddBookToCheckoutBooks(IBook book) => this.CheckedOutBooks.Add(book);

        public void AddBookToReservedBooks(IBook book) => this.ReservedBooks.Add(book);

        public void AddToOverdueBooks(List<IBook> overdueBooks)
        {
            foreach (var book in overdueBooks)
            {
                if (this.OverdueBooks.FindAll(b => b.ID == book.ID).SingleOrDefault() is null)
                {
                    this.OverdueBooks.Add(book);
                    this.RemoveFromCheckedoutBooks(book);
                }
            }
        }

        public void AddOverdueReservations(List<IBook> overdueReservations)
        {
            foreach (var book in overdueReservations)
            {
                this.OverdueReservations.Add(book);
                this.RemoveFromReservedBooks(book);
            }
        }

        // TODO Hrisi
        public void RemoveAllOverdueReservations()
        {
            //foreach (var book in this.OverdueReservations)
            //{
            //    if (book.Status == BookStatus.Reserved)
            //    {

            //    }
            //}
            this.OverdueReservations.Clear();
        }

        public void Update(IUser otherUser)
        {
            this.Status = otherUser.Status;
            this.CheckedOutBooks = otherUser.CheckedOutBooks;
            this.ReservedBooks = otherUser.ReservedBooks;
            this.OverdueReservations = otherUser.OverdueReservations;
            this.OverdueBooks = otherUser.OverdueBooks;
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

        public string DisplayOverdueBooks()
        {
            var strBuilder = new StringBuilder();

            foreach (var book in this.OverdueBooks)
            {
                // Printed in red
                Console.ForegroundColor = ConsoleColor.Red;

                strBuilder.AppendLine($"ID: {book.ID} || Title: {book.Title} || Author: {book.Author} || CheckedOut Date: {book.CheckoutDate.ToString("dd MM yyyy")} || Due Date: {book.DueDate.ToString("dd MM yyyy")}");

                Console.ResetColor();
            }
            return strBuilder.ToString();
        }

        public bool HasOverdueBooks()
        {
            if (this.OverdueBooks.Count != 0)
                return true;
            else return false;
        }

        public bool HasOverdueReservations()
        {
            if (this.OverdueReservations.Count != 0)
                return true;
            else return false;
        }
    }
}