using Library.Models.Contracts;
using Library.Models.Enums;
using System;
using System.Collections.Generic;

namespace Library.Models.Models
{
    // OOP: Encapsulation - properties with private set 

    public class Book
    {
        private string _title;
        private int _year;
        private int _rack;
        //private string _author;
        //private string _isbn;
        //private string _genre;
        // private string _publisher;

        public Book() { }

        public Book(Author author, string title, string isbn, List<Genre> genre, Publisher publisher, int year, int rack)
        {
            this.Author = author;
            this.Title = title;
            this.ISBN = isbn;
            this.Genre = genre;
            this.Publisher = publisher;
            this.Year = year;
            this.Rack = rack;
            this.Status = BookStatus.Available;
        }

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentOutOfRangeException("The title should be between 1 and 100 symbols!");
                }
                _title = value;
            }
        }

        public Author Author;
        

        public string ISBN;
        //{
        //    get => _isbn;
        //    set
        //    {
        //        //if (value.Length != 10 && value.Length != 13)
        //        //    throw new ArgumentOutOfRangeException("ISBN should be 10 or 13 characters");

        //        _isbn = value;
        //    }
        //}

        public List<Genre> Genre;
        //{
        //    get => _genre;
        //    set
        //    {
        //        if (value.Length < 1 || value.Length > 40)
        //            throw new ArgumentOutOfRangeException("The genre should be between 1 and 40 symbols!");

        //        _genre = value;
        //    }
        //}

        public Publisher Publisher;
        //{
        //    get => _publisher;
        //    set
        //    {
        //        if (value.Length < 1 || value.Length > 40)
        //            throw new ArgumentOutOfRangeException("The publisher name should be between 1 and 40 symbols!");

        //        _publisher = value;
        //    }
        //}

        public int Year
        {
            get => _year;
            set
            {
                // First book ever printed was in 1629
                if (value < 1629 || value > DateTime.Now.Year)
                    throw new ArgumentOutOfRangeException($"The publication year should be between 1 and {DateTime.Now.Year}");

                _year = value;
            }
        }

        public int Rack
        {
            get => _rack;
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException($"The rack cannot be zero or negative");

                _rack = value;
            }
        }

        public BookStatus Status { get; set; }
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
            this.Year = otherBook.Year;
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
