using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Models
{
    public class Book
    {
        public Book() { }

        public Book(Author author, string title, string isbn, Publisher publisher, int year, int rack)
        {
            this.Author = author;
            this.Title = title;
            this.ISBN = isbn;
            this.Publisher = publisher;
            this.Year = year;
            this.Rack = rack;
            this.Status = BookStatus.Available;
        }

        [Key]
        public int Id { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = GlobalConstants.BookTitleLimit, MinimumLength = 1)]
        public string Title { get; set; }


        public string ISBN { get; set; }


        [Range(1629, 2019, ErrorMessage = GlobalConstants.BookYearLimit)]
        public int Year { get; set; }


        [Range(1, int.MaxValue, ErrorMessage = GlobalConstants.BookRackLimit)]
        public int Rack { get; set; }

        [Required]
        public BookStatus Status { get; set; }
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }

        public Author Author { get; set; }
        public Publisher Publisher { get; set; }
        public List<BookGenre> BookGenres { get; set; }
        public CheckoutBook CheckedoutBook { get; set; }
        public ReservedBook ReservedBook { get; set; }

        //---- Update 

        public void Update(IBook otherBook)
        {
            this.Title = otherBook.Title;
            //this.Author = otherBook.Author;
            this.ISBN = otherBook.ISBN;
            //this.Genre = otherBook.Genre;
            // this.Publisher = otherBook.Publisher;
            // this.Year = otherBook.Year;
            this.Rack = otherBook.Rack;
            this.Status = otherBook.Status;
            //this.CheckoutDate = otherBook.CheckoutDate;
            //this.ResevedDate = otherBook.ResevedDate;
            //this.DueDate = otherBook.DueDate;
        }

        public void Update(BookStatus status, DateTime checkoutDate, DateTime dueDate)
        {
            //this.Status = status;
            //this.CheckoutDate = checkoutDate;
            //this.DueDate = dueDate;
        }

        public void Update(BookStatus status, DateTime reservationDate, DateTime reservationDueDate, bool ifReservation)
        {
            //this.Status = status;
            //this.ResevedDate = reservationDate;
            //this.ResevationDueDate = reservationDueDate;
        }
    }
}
