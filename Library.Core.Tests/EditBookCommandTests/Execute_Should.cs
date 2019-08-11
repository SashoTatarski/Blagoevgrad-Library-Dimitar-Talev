using Library.Core.Commands;
using Library.Models.Contracts;
using Library.Models.Enums;
using Library.Models.Models;
using Library.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Tests.EditBookCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        //[TestMethod]
        //public void PassParameters()
        //{
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
        //        .SetupSequence(m => m.InputParameters(It.IsAny<string>(), It.IsAny<Func<string, bool>>()))
        //        .Returns(authorName)
        //        .Returns(title)
        //        .Returns(category)
        //        .Returns(publisher)
        //        .Returns(year)
        //        .Returns(rack);
        //    rendererMock.Setup(m => m.InputParameters(It.IsAny<string>()))
        //        .Returns(isbn);

        //    var bookManagerMock = new Mock<IBookManager>();
        //    bookManagerMock
        //        .Setup(m => m.UpdateBook(bookId, authorName, title, isbn, category, publisher, expectedYear, expectedRack));

        //    var accountManagerMock = new Mock<IAuthenticationManager>();
        //    accountManagerMock
        //        .Setup(a => a.CurrentAccount)
        //        .Returns(It.IsAny<IAccount>());
                

        //    var sut = new EditBookCommand(rendererMock.Object, accountManagerMock.Object, bookManagerMock.Object);

        //    sut.Execute();

        //    bookManagerMock.Verify(b => b.UpdateBook(bookId, authorName, title, isbn, category, publisher, expectedYear, expectedRack), Times.Once);
        //}
    }
}
