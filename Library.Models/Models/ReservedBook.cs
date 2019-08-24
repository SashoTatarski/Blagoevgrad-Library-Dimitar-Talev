using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Models.Models
{
    public class ReservedBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }

        public DateTime ReservationDate { get; set; }
        public DateTime ReservationDueDate { get; set; }
        
    }
}
