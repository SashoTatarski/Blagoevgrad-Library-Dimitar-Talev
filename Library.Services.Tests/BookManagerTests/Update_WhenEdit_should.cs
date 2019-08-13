using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Tests.BookManagerTests
{
    [TestClass]
    public class Update_WhenEdit_Should
    {
        [TestMethod]
        public void UpdateAuthorName()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(author, bookToUpdate.Author);
        }

        [TestMethod]
        public void UpdateTitle()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(title, bookToUpdate.Title);
        }

        [TestMethod]
        public void UpdateISBN()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(isbn, bookToUpdate.ISBN);
        }

        [TestMethod]
        public void UpdateGenre()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(genre, bookToUpdate.Genre);
        }

        [TestMethod]
        public void UpdatePublisher()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(publisher, bookToUpdate.Publisher);
        }

        [TestMethod]
        public void NotUpdateID()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(old_id, bookToUpdate.ID);
        }

        [TestMethod]
        public void UpdateYear()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(year, bookToUpdate.Year);
        }

        [TestMethod]
        public void UpdateRack()
        {
            var id = 2;
            var author = "new_Author";
            var title = "new_Title";
            var isbn = "new_ISBN";
            var genre = "new_Genre";
            var publisher = "new_*Pub";
            int year = 2001;
            int rack = 11;

            var old_id = 1;
            var old_author = "Author";
            var old_title = "Title";
            var old_isbn = "ISBN";
            var old_genre = "Genre";
            var old_publisher = "Pub";
            int old_year = 2000;
            int old_rack = 10;

            var bookToUpdate = new Book(old_id, old_author, old_title, old_isbn, old_genre, old_publisher, old_year, old_rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(bookToUpdate);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(x => x.CreateBook(It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>()))
                .Returns(new Book(id, author, title, isbn, genre, publisher, year, rack));

            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.UpdateBook(id, author, title, isbn, genre, publisher, year, rack);

            Assert.AreEqual(rack, bookToUpdate.Rack);
        }
    }
}
