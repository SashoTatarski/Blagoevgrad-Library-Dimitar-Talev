using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Genre_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var genre = "genre";

            var sut = new Book(1, "author", "title", "0123456789", genre, "publisher", 2019, 10);

            Assert.AreEqual("genre", sut.Genre);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Genre_LessThanOne()
        {
            var genre = "";

            var sut = new Book(1, "author", "title", "0123456789", genre, "publisher", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TitleName_MoreThan140()
        {
            var genre = new string('f', 41);

            var sut = new Book(1, "author", "title", "0123456789", genre, "publisher", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Genre_IsNull()
        {
            string genre = null;

            var sut = new Book(1, "author", "title", "0123456789", genre, "publisher", 2019, 10);
        }
    }
}
