using Library.Database.Contracts;
using Library.Models.Contracts;
using Library.Models.Models;
using Library.Services.Contracts;
using Library.Services.Factory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Services.Tests.BookManagerTests
{
    [TestClass]
    public class AddBook_Should
    {
        [TestMethod]
        public void PassParameters()
        {
            var id = 1;
            var author = "Author";
            var title = "Title";
            var isbn = "ISBN";
            var genre = "Genre";
            var publisher = "Pub";
            int year = 2000;
            int rack = 10;

            IBook bookToAdd = new Book(id,
                author, title, isbn, genre, publisher, year, rack);

            var databaseMock = new Mock<IBookDatabase>();
            databaseMock
                .Setup(x => x.Create(bookToAdd));

            var factoryMock = new Mock<IBookFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();

            var sut = new BookManager(databaseMock.Object, factoryMock.Object, formatterMock.Object);

            sut.AddBook(bookToAdd);

            databaseMock.Verify(x => x.Create(bookToAdd), Times.Once);
        }
    }
}
