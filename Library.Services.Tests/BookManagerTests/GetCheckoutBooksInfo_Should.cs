using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Library.Services.Tests.BookManagerTests
{
    [TestClass]
    public class GetCheckoutBooksInfo_Should
    {
        [TestMethod]
        public void Throws_ArgumentExcepion_WhenThereAreNoBooks()
        {
            var user = new User("name", "pass");

            var databaseMock = new Mock<IBookDatabase>();
            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            Assert.ThrowsException<ArgumentException>(() => sut.GetCheckedoutBooksInfo(user));
        }

        [TestMethod]
        public void ReturnsCorrectInfo()
        {
            var user = new User("name", "pass");

            IBook book = new Book(1,
              "xx", "xx", "xx", "xx", "xx", 2000, 1);

            user.AddBookToCheckoutBooks(book);

            //var userMock = new Mock<IUser>();
            //userMock
            //    .Setup(x => x.CheckedOutBooks = new List<IBook>);

            var databaseMock = new Mock<IBookDatabase>();
            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();
            formatterMock
                .Setup(x => x.FormatCheckedoutBook(new Mock<IBook>().Object));

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.GetCheckedoutBooksInfo(user);

            formatterMock.Verify(x => x.FormatCheckedoutBook(new Mock<IBook>().Object), Times.Once);
        }
    }
}
