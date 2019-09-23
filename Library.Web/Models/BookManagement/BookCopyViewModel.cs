using Library.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Web.Models.BookManagement
{
    public class BookCopyViewModel
    {
        public string Title { get; set; }

        public string Status { get; set; }
        public User CheckedOutBy { get; set; }
        public List<User> ReservedBy { get; set; }
    }
}
