using Library.Models.Contracts;
using Library.Models.Enums;
using System;

namespace Library.Models.Models
{
    public class Book : IBook
    {
        private static int currentId = 1;
        private string author;
        private string title;
        private string genre;
        private string publisher;
        private int year;

        public Book(string author, string title, string isbn, string genre, string publisher, int year, int rack, BookStatus status)
        {
            this.ID = currentId++;
            this.Author = author;
            this.Title = title;
            this.ISBN = isbn;
            this.Genre = genre;
            this.Publisher = publisher;
            this.Year = year;
            this.Rack = rack;
            this.Status = status;
        }

        public int ID { get; }

        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                if (value.Length < 1 || value.Length > 40)
                {
                    throw new ArgumentOutOfRangeException("The author name should be between 1 and 40 symbols!");
                }
                this.author = value;
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentOutOfRangeException("The title should be between 1 and 100 symbols!");
                }
                this.title = value;
            }
        }

        public string ISBN { get; }

        public string Genre
        {
            get
            {
                return this.genre;
            }
            set
            {
                if (value.Length < 1 || value.Length > 40)
                {
                    throw new ArgumentOutOfRangeException("The genre should be between 1 and 40 symbols!");
                }
                this.genre = value;
            }
        }

        public string Publisher
        {
            get
            {
                return this.publisher;
            }
            set
            {
                if (value.Length < 1 || value.Length > 40)
                {
                    throw new ArgumentOutOfRangeException("The publisher name should be between 1 and 40 symbols!");
                }
                this.publisher = value;
            }
        }

        public int Year
        {
            get
            {
                return this.year;
            }
            set
            {
                if (value < 1 || value > 2019)
                {
                    throw new ArgumentOutOfRangeException("The publication year should be between 1 and 2019");
                }
                this.year = value;
            }
        }

        public int Rack { get; }

        public BookStatus Status { get; set; }

        DateTime CheckoutDate { get; set; }
        DateTime DueDate { get; set; }
        DateTime ResevedDate { get; set; }
    }
}
