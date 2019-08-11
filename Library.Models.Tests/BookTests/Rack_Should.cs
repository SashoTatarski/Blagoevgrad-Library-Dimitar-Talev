using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Rack_Should
    {
        [TestMethod]
        public void CorrectlyAssignedValues()
        {
            var rack = 10;

            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, rack);

            Assert.AreEqual(10, sut.Rack);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Rack_LessThanOne()
        {
            var rack = 0;

            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, rack);
        }
    }
}
