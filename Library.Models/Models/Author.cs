using Library.Models.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = GlobalConstants.AuthorNameLimit)]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
