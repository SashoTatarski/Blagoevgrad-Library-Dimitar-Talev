using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Title_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var title = "title";

            var sut = new Book(1, "author", title, "0123456789", "genre", "publisher", 2019, 10);

            Assert.AreEqual("title", sut.Title);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Title_LessThanOne()
        {
            var title = "";

            var sut = new Book(1, "author", title, "0123456789", "genre", "publisher", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TitleName_MoreThan100()
        {
            var title = new string('f', 101);

            var sut = new Book(1, "author", title, "0123456789", "genre", "publisher", 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Title_IsNull()
        {
            string title = null;

            var sut = new Book(1, "author", title, "0123456789", "genre", "publisher", 2019, 10);
        }
    }
}
