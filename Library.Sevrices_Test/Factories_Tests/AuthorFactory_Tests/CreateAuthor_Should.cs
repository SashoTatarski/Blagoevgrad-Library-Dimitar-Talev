using Library.Database.Contracts;
using Library.Models.Models;
using Library.Services.Factories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Sevrices_Test.Factories_Tests.AuthorFactory_Tests
{
    [TestClass]
    class CreateAuthor_Should
    {
        [TestMethod]
        public void CreateNewAuthor()
        {
            var DBMock = new Mock<IDatabase<Author>>();
            
            var sut = new AuthorFactory(DBMock.Object);
        }
    }
}
