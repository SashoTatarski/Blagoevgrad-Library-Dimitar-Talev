using Library.Core.Commands;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Tests.AddBookCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void PassParameters()
        {
            var authorName = "testauthor";
            var title = "testtitle";
            var isbn = "testisbn";
            var category = "testcat";
            var publisher = "testpub";
            var year = "1964";
            var expectedYear = int.Parse(year);
            var rack = "5";
            var expectedRack = int.Parse(rack);
            var bookId = 1;
            var expectedBookId = bookId + 1;

            var rendererMock = new Mock<IConsoleRenderer>();
            rendererMock
                .SetupSequence(m => m.InputParameters(It.IsAny<string>(), It.IsAny<Func<string, bool>>()))
                .Returns(authorName)
                .Returns(title)
                .Returns(category)
                .Returns(publisher)
                .Returns(year)
                .Returns(rack);
            rendererMock.Setup(m => m.InputParameters(It.IsAny<string>()))
                .Returns(isbn);

            var bookManagerMock = new Mock<IBookManager>();
            bookManagerMock.Setup(m => m.GetLastBookID()).Returns(bookId);

            var factoryMock = new Mock<IBookFactory>();
            factoryMock
                .Setup(m => m.CreateBook(expectedBookId, authorName, title, isbn, category, publisher, expectedYear, expectedRack))
                .Returns(new Mock<IBook>().Object);

            var formatterMock = new Mock<IConsoleFormatter>();            


            var sut = new AddBookCommand(factoryMock.Object, rendererMock.Object, bookManagerMock.Object, formatterMock.Object);

           
            sut.Execute();

            factoryMock.Verify(m => m.CreateBook(expectedBookId, authorName, title, isbn, category, publisher, expectedYear, expectedRack), Times.Once);
        }

        //[TestMethod]
        //public void ReturnProperMessage()
        //{
        //    var bookTitle = "test title";
        //    var bookAuthor = "Test Author";

        //    var bookMock = new Mock<IBook>();
        //    bookMock
        //        .Setup(b => b.Title)
        //        .Returns(bookTitle);
        //    bookMock
        //        .Setup(b => b.Author)
        //        .Returns(bookAuthor);

        //    var factoryMock = new Mock<IBookFactory>();
        //    factoryMock
        //        .Setup(m => m.CreateBook(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
        //        .Returns(bookMock.Object);

        //    var authorName = "testauthor";
        //    var title = "testtitle";
        //    var isbn = "testisbn";
        //    var category = "testcat";
        //    var publisher = "testpub";
        //    var year = "1964";
        //    var expectedYear = int.Parse(year);
        //    var rack = "5";
        //    var expectedRack = int.Parse(rack);
        //    var bookId = 1;
        //    var expectedBookId = bookId + 1;

        //    var rendererMock = new Mock<IConsoleRenderer>();
        //    rendererMock
        //       .SetupSequence(m => m.InputParameters(It.IsAny<string>(), It.IsAny<Func<string, bool>>()))
        //       .Returns(authorName)
        //       .Returns(title)
        //       .Returns(category)
        //       .Returns(publisher)
        //       .Returns(year)
        //       .Returns(rack);

        //    var bookManagerMock = new Mock<IBookManager>();
        //    bookManagerMock.Setup(m => m.GetLastBookID()).Returns(bookId);

        //    var mockBook = new Book(bookId,authorName,title,)

        //    var formatterMock = new Mock<IConsoleFormatter>();
        //    formatterMock
        //        .Setup(f => f.Format(It.IsAny<IBook>()))
        //        .Returns()

        //    var sut = new AddBookCommand(factoryMock.Object, rendererMock.Object, bookManagerMock.Object, formatterMock.Object);

        //    var message = sut.Execute();
        //    var expectedMessage = $"Successfully added the book {bookTitle} - {bookAuthor}";
        //    Assert.AreEqual(expectedMessage, message);
        //}
    }
}
