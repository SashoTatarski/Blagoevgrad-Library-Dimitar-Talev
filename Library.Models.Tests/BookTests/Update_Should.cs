using Library.Models.Contracts;
using Library.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Models.Tests.BookTests
{
    [TestClass]
    public class Update_Should
    {
        [TestMethod]
        public void Update()
        {
            var book = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);

            var book2 = new Book(1, "author2", "title", "0123456789", "genre", "publisher", 2019, 10);

            book.Update(book2);

            Assert.AreEqual(book2.Author, book.Author);
            
        }

        [TestMethod]
        public void UpdateParameters()
        {
            var book = new Book(1, "author", "title", "0123456789", "genre", "publisher", 2019, 10);

            var book2 = new Book(1, "author2", "title", "0123456789", "genre", "publisher", 2019, 10);

            book.Update(book2);

            Assert.AreEqual(book2.Author, book.Author);

        }
    }
}
