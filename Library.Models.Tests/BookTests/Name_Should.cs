using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Name_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {            
            var author = "author";             

            var sut = new Book(1, author, "title", "0123456789", "genre", "publisher", 2019, 10);

            Assert.AreEqual("author", sut.Author);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AuthorName_LessThanOne()
        {            
            var author = "";            

            var sut = new Book(1, author, "title", "isbn", "genre", "0123456789", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void AuthorName_MoreThanForty()
        {
            var author = new string('f', 41);

            var sut = new Book(1, author, "title", "isbn", "genre", "0123456789", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AuthorName_IsNull()
        {
            string author = null;

            var sut = new Book(1, author, "title", "0123456789", "genre", "publisher", 2019, 10);
        }
    }
}
