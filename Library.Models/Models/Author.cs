using System.Collections.Generic;

namespace Library.Models.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //{
        //    //get => _author;
        //    //set
        //    //{
        //    //    if (value.Length < 1 || value.Length > 40)
        //    //        throw new ArgumentOutOfRangeException("The author name should be between 1 and 40 symbols!");

        //    //    _author = value;
        //    //}
        //}
        public List<Book> Books { get; set; }
    }
}
