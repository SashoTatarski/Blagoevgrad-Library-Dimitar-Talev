using Library.Models.Contracts;
using Library.Models.Enums;
using System;

namespace Library.Models.Models
{
    public class Book : IBook
    {
        private string author;
        private string title;
        private string genre;
        private string publisher;
        private int year;
        private int rack;

        public Book(int Id,string author, string title, string isbn, string genre, string publisher, int year, int rack)
        {
            this.ID = Id;
            this.Author = author;
            this.Title = title;
            this.ISBN = isbn;
            this.Genre = genre;
            this.Publisher = publisher;
            this.Year = year;
            this.Rack = rack;
            this.Status = BookStatus.Available;
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
                if (value < 1 || value > DateTime.Now.Year)
                {
                    throw new ArgumentOutOfRangeException($"The publication year should be between 1 and {DateTime.Now.Year}");
                }
                this.year = value;
            }
        }

        public int Rack
        {
            get
            {
                return this.rack;
            }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException($"The rack cannot be zero or negative");
                }
                this.rack = value;
            }
        }

        public BookStatus Status { get; set; }

        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ResevedDate { get; set; }
    }
}
