using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Library.Services.Tests.BookManagerTests
{
    [TestClass]
    public class GetLastBookID_Should
    {
        [TestMethod]
        public void ReturnTheLargestIDinList()
        {
            IBook book1 = new Book(1,
               "xx", "xx", "xx", "xx", "xx", 2000, 1);
            IBook book2 = new Book(2,
               "xx", "xx", "xx", "xx", "xx", 2000, 1);
            IBook book3 = new Book(3,
               "xx", "xx", "xx", "xx", "xx", 2000, 1);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x=>x.Load())
                .Returns(new List<IBook> { book1, book2, book3 });

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            Assert.AreEqual(book3.ID, sut.GetLastBookID());
        }
    }
}
