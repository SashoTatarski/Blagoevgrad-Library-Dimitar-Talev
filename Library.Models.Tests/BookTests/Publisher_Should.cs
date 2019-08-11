using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Publisher_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var publisher = "publisher";

            var sut = new Book(1, "author", "title", "0123456789", "genre", publisher, 2019, 10);

            Assert.AreEqual("publisher", sut.Publisher);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Publisher_LessThanOne()
        {
            var publisher = "";

            var sut = new Book(1, "author", "title", "0123456789", "genre", publisher, 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Publisher_MoreThan140()
        {
            var publisher = new string('f', 41);

            var sut = new Book(1, "author", "title", "0123456789", "genre", publisher, 2019, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Publisher_IsNull()
        {
            string publisher = null;

            var sut = new Book(1, "author", "title", "0123456789", "genre", publisher, 2019, 10);
        }
    }
}
