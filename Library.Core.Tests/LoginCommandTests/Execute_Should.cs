using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.Tests.LoginCommandTests
{
    [TestClass]
    public class Execute_Should
    {
        [TestMethod]
        public void PassParameters()
        {
            //var rendererMock = new Mock<IConsoleRenderer>();
            //rendererMock
            //    .Setup(m => m.InputParameters(It.IsAny<string>()))
            //   .Returns(It.IsAny<string>());            

            var accountMock = new Mock<IAccountManager>();
            accountMock
                .Setup(a => a.FindAccount("sasho"))
                .Returns(It.IsAny<IUser>());

            accountMock.Verify(a => a.FindAccount("sasho"), Times.Once);
            
        }
    }
}
