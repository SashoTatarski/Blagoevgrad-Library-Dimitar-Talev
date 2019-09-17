using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Models.Models
{
    public class Rating
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        [Required]
        public int Rate { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }


        public Book Book { get; set; }
        public User User { get; set; }
    }
}
