using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Isbn_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var isbn = "0123456789";

            var sut = new Book(1, "author", "title", isbn, "genre", "publisher", 2019, 10);

            Assert.AreEqual("0123456789", sut.ISBN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Isbn_DifferentThan10()
        {
            var isbn = "01234567891";

            var sut = new Book(1, "author", "title", isbn, "genre", "publisher", 2019, 10);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Isbn_IsNull()
        {
            string isbn = null;

            var sut = new Book(1, "author", "title", isbn, "genre", "publisher", 2019, 10);
        }
    }
}
