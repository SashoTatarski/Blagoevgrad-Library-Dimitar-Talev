using System;

namespace Library.Models.Models
{
    public class CheckoutBook
    {
        public string UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }

        public DateTime CheckoutDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
