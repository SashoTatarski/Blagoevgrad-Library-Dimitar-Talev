﻿using System.Collections.Generic;

namespace Library.Models.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
        public List<BookGenre> BookGenres { get; set; }
    }
}
