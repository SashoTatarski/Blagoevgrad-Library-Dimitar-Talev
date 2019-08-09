using Library.Core.Commands;
using Library.Models.Contracts;
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
        //var authorName = _renderer.InputParameters("author name",
        //     s => s.Length < 1 || s.Length > 40);

        //var title = _renderer.InputParameters("title",
        //    s => s.Length < 1 || s.Length > 100);

        //var isbn = _renderer.InputParameters("ISBN code");

        //var category = _renderer.InputParameters("genre",
        //    g => g.Length < 1 || g.Length > 40);

        //var publisher = _renderer.InputParameters("publisher",
        //    g => g.Length < 1 || g.Length > 40);

        //var year = int.Parse(_renderer.InputParameters("year",
        //    y => int.Parse(y) < 1 || int.Parse(y) > DateTime.Now.Year));

        //var rack = int.Parse(_renderer.InputParameters("rack",

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


            var sut = new AddBookCommand(factoryMock.Object, rendererMock.Object, bookManagerMock.Object);

            // Act

            var message = sut.Execute();

            factoryMock.Verify(m => m.CreateBook(expectedBookId, authorName, title, isbn, category, publisher, expectedYear, expectedRack), Times.Once);
        }
    }
}
