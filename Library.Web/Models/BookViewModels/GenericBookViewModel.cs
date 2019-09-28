﻿using System.Collections.Generic;

namespace Library.Web.Models.BookViewModels
{
    public class GenericBookViewModel
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Publisher { get; set; }
        public List<string> Genres { get; set; }
        public string ISBN { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
    }
}
