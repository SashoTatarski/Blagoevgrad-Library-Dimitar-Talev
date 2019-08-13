using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Library.Services.Tests.BookManagerTests
{
    [TestClass]
    public class Update_WhenCheckoutBook_Should
    {
        [TestMethod]
        public void NotChangeBookID()
        {
            var id = 1;
            var author = "Author";
            var title = "Title";
            var isbn = "ISBN";
            var genre = "Genre";
            var publisher = "Pub";
            int year = 2000;
            int rack = 10;

            var status = BookStatus.CheckedOut;
            var checkoutDate = DateTime.Today;
            var dueDate = DateTime.Today.AddDays(5);

            IBook bookToUpdate = new Book(id,
                author, title, isbn, genre, publisher, year, rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();


            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);
            sut.UpdateBook(id, status, checkoutDate, dueDate);

            Assert.AreEqual(id, bookToUpdate.ID);
        }

        [TestMethod]
        public void UpdateStatus()
        {
            var id = 1;
            var author = "Author";
            var title = "Title";
            var isbn = "ISBN";
            var genre = "Genre";
            var publisher = "Pub";
            int year = 2000;
            int rack = 10;

            var status = BookStatus.CheckedOut;
            var checkoutDate = DateTime.Today;
            var dueDate = DateTime.Today.AddDays(5);

            IBook bookToUpdate = new Book(id,
                author, title, isbn, genre, publisher, year, rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();


            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);
            sut.UpdateBook(id, status, checkoutDate, dueDate);

            Assert.AreEqual(status, bookToUpdate.Status);
        }

        [TestMethod]
        public void UpdateCheckOutDate()
        {
            var id = 1;
            var author = "Author";
            var title = "Title";
            var isbn = "ISBN";
            var genre = "Genre";
            var publisher = "Pub";
            int year = 2000;
            int rack = 10;

            var status = BookStatus.CheckedOut;
            var checkoutDate = DateTime.Today;
            var dueDate = DateTime.Today.AddDays(5);

            IBook bookToUpdate = new Book(id,
                author, title, isbn, genre, publisher, year, rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();


            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);
            sut.UpdateBook(id, status, checkoutDate, dueDate);

            Assert.AreEqual(checkoutDate, bookToUpdate.CheckoutDate);
        }

        [TestMethod]
        public void UpdateDueDate()
        {
            var id = 1;
            var author = "Author";
            var title = "Title";
            var isbn = "ISBN";
            var genre = "Genre";
            var publisher = "Pub";
            int year = 2000;
            int rack = 10;

            var status = BookStatus.CheckedOut;
            var checkoutDate = DateTime.Today;
            var dueDate = DateTime.Today.AddDays(5);

            IBook bookToUpdate = new Book(id,
                author, title, isbn, genre, publisher, year, rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();


            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);
            sut.UpdateBook(id, status, checkoutDate, dueDate);

            Assert.AreEqual(dueDate, bookToUpdate.DueDate);
        }
    }
}
