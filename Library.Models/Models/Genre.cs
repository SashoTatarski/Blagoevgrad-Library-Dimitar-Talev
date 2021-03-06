using Library.Models.Utils;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = GlobalConstants.GenreNameLimit)]
        public string GenreName { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
