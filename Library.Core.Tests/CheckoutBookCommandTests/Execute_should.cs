using Library.Core.Commands;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Tests.CheckoutBookCommandTests
{
    // ASK: How to propertly do this test?
    [TestClass]
    public class Execute_should
    {
        [TestMethod]
        public void PassParameter()
        {
            var testID = "11";
            var expectedID = 11;
            //var booktoCheckout = new Book(int.Parse(testID), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>());

            var authenticationMock = new Mock<IAuthenticationManager>();

            var rendererMock = new Mock<IConsoleRenderer>();
            rendererMock
                .Setup(i => i.InputParameters(It.IsAny<string>()))
                .Returns(testID);

            var bookManagerMock = new Mock<IBookManager>();
            bookManagerMock
                .Setup(f => f.FindBook(expectedID))
                .Returns(new Mock<IBook>().Object);

            var accountManagerMock = new Mock<IAccountManager>();
            var systemMock = new Mock<ILibrarySystem>();
            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new CheckOutBookCommand(authenticationMock.Object, rendererMock.Object, bookManagerMock.Object, accountManagerMock.Object, systemMock.Object, formatterMock.Object);

            sut.Execute();

            bookManagerMock.Verify(f => f.FindBook(expectedID), Times.Once);
        }
    }
}
