using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Properties_Should
    {
        [TestMethod]
        public void BookStatus()
        {            
            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);

            sut.Status = Enums.BookStatus.Available;

            Assert.AreEqual(sut.Status, Enums.BookStatus.Available);
        }

        [TestMethod]
        public void CheckoutDate()
        {
            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);


            sut.CheckoutDate = DateTime.MinValue;

            Assert.AreEqual(sut.CheckoutDate, DateTime.MinValue);
        }

        [TestMethod]
        public void DueDate()
        {
            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);


            sut.DueDate = DateTime.MinValue;

            Assert.AreEqual(sut.DueDate, DateTime.MinValue);
        }

        [TestMethod]
        public void ReserveDate()
        {
            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);


            sut.ResevedDate = DateTime.MinValue;
           
            Assert.AreEqual(sut.ResevedDate, DateTime.MinValue);
        }

        [TestMethod]
        public void ResevationDueDate()
        {
            var sut = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);


            sut.ResevationDueDate = DateTime.MinValue;

            Assert.AreEqual(sut.ResevationDueDate, DateTime.MinValue);
        }
    }
}
