using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Contracts;
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
            var username = "sasho";
            var password = "pass";



            //var accountManagerMock = new Mock<IAccountManager>();
            //accountManagerMock
            //    .Setup(a => a.FindAccount())
            //    .Returns(new Mock<IAccount>().Object);




            //var loggedUserMock = new Mock<IAcc>();
            //loggedUserMock
            //    .Setup(u => u.CreateUser(It.IsAny<string>(), It.IsAny<string>()))
            //    .Returns(new Mock<IAccount>().Object);


            //var accountMock = new Mock<IAccountManager>();
            //accountMock
            //    .Setup(a => a.FindAccount("sasho"))
            //    .Returns(new Mock<IAccount>().Object);

            //var autheticMock = new Mock<IAuthenticationManager>();
            //autheticMock.Setup(a => a.LogIn(It.IsAny<IAccount>()));

            //autheticMock.Verify(a => a.LogIn(accountMock), Times.Once);

        }
    }
}
