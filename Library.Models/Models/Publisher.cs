using Library.Models.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Models
{
    public class Publisher
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength (40, ErrorMessage =GlobalConstants.PublisherNameLimit)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
