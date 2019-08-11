using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Yeah_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var year = 2019;

            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", year, 10);

            Assert.AreEqual(2019, sut.Year);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Year_LessThan1629()
        {
            var year = 1628;

            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", year, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Year_MoreThan2019()
        {
            var year = 2020;

            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", year, 10);
        }
    }
}
