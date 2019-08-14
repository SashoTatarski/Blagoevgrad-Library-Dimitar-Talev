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
        // ASK: It this a correct test?
        [TestMethod]
        public void LogInMethod_Proper()
        {
            var autheticMock = new Mock<IAuthenticationManager>();
            
            var sut = autheticMock.Object;

            sut.LogIn(It.IsAny<IAccount>());

            autheticMock.Verify(x => x.LogIn(It.IsAny<IAccount>()), Times.Once);
        }
    }
}
