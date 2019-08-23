using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            this.Status = AccountStatus.Active;            
            this.OverdueReservations = new List<CheckoutBook>();
            this.OverdueBooks = new List<CheckoutBook>();
        }

        [NotMapped]
        public override IEnumerable<string> AllowedCommands => new List<string>
            {
                "Check Out Book",
                "Return Book",
                "Renew Book",
                "Reserve Book",
                "Search",
                "Travel In Time",
                "Log Out"
            };

        public int Id { get; set; }

        public AccountStatus Status { get; set; }

        public decimal LateFees { get; set; }

        public IList<CheckoutBook> CheckedoutBooks { get; set; }

        public IList<ReservedBook> ReservedBooks { get; set; }        
       
        [NotMapped]
        public List<CheckoutBook> OverdueReservations { get; set; }

        [NotMapped]
        public List<CheckoutBook> OverdueBooks { get; set; }



        
        //public void RemoveFromReservedBooks(IBook book) => this.ReservedBooks.Remove(book);

        //public void RemoveFromOverdueBooks(IBook book) => this.OverdueBooks.RemoveAll(b => b.Id == book.Id);

        //public void RemoveFromOverdueReservations(IBook book) => this.OverdueReservations.RemoveAll(b => b.Id == book.Id);

        //public void AddBookToCheckoutBooks(IBook book) => this.CheckedOutBooks.Add(book);

        //public void AddBookToReservedBooks(IBook book) => this.ReservedBooks.Add(book);

        public void AddToOverdueBooks(List<CheckoutBook> overdueBooks)
        {
            //foreach (var book in overdueBooks)
            //{
            //    if (this.OverdueBooks.FindAll(b => b.BookId == book.BookId).SingleOrDefault() is null)
            //    {
            //        this.OverdueBooks.Add(book);
            //        this.RemoveFromCheckedoutBooks(book);
            //    }
            //}
        }

       // public void RemoveFromCheckedoutBooks(CheckoutBook book) =>   //this.CheckedOutBooks.RemoveAll(b => b.Id == book.Id);

        //public void AddOverdueReservations(List<IBook> overdueReservations)
        //{
        //    foreach (var book in overdueReservations)
        //    {
        //        this.OverdueReservations.Add(book);
        //        this.RemoveFromReservedBooks(book);
        //    }
        //}

        //public void RemoveAllOverdueReservations() => this.OverdueReservations.Clear();

        //public void Update(IUser otherUser)
        //{
        //    this.Status = otherUser.Status;
        //    this.CheckedOutBooks = otherUser.CheckedOutBooks;
        //    this.ReservedBooks = otherUser.ReservedBooks;
        //    this.OverdueReservations = otherUser.OverdueReservations;
        //    this.OverdueBooks = otherUser.OverdueBooks;
        //    this.LateFees = otherUser.LateFees;
        //}

        //public string DisplayCheckedoutBooks()
        //{
        //    var strBuilder = new StringBuilder();

        //    if (this.CheckedOutBooks.Count == 0)
        //        throw new ArgumentException("There are no checked out books!");

        //    else
        //    {
        //        strBuilder.AppendLine("Books you have checked out:");

        //        foreach (var book in this.CheckedOutBooks)
        //        {
        //            // If a book is overdue it's printed in red
        //            if (book.DueDate < VirtualDate.VirtualToday)
        //                Console.ForegroundColor = ConsoleColor.Red;

        //            strBuilder.AppendLine($"ID: {book.Id} || Title: {book.Title} || Author: {book.Author} || CheckedOut Date: {book.CheckoutDate.ToString("dd MM yyyy")} || Due Date: {book.DueDate.ToString("dd MM yyyy")}");

        //            Console.ResetColor();
        //        }
        //    }
        //    return strBuilder.ToString();
        //}

        //public string DisplayOverdueBooks()
        //{
        //    var strBuilder = new StringBuilder();

        //    foreach (var book in this.OverdueBooks)
        //    {
        //        // Printed in red
        //        Console.ForegroundColor = ConsoleColor.Red;

        //        strBuilder.AppendLine($"ID: {book.Id} || Title: {book.Title} || Author: {book.Author} || CheckedOut Date: {book.CheckoutDate.ToString("dd MM yyyy")} || Due Date: {book.DueDate.ToString("dd MM yyyy")}");

        //        Console.ResetColor();
        //    }
        //    return strBuilder.ToString();
        //}

        //public bool HasOverdueBooks() => this.OverdueBooks.Count != 0 ? true : false;

        //public bool HasOverdueReservations() => this.OverdueReservations.Count != 0 ? true : false;
    }
}