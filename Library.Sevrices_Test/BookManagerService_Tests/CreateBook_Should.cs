using Library.Database;
using Library.Database.Contracts;
using Library.Models.Models;
using Library.Services;
using Library.Services.Contracts;
using Library.Services.Factories.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Library.Sevrices_Test.BookManagerService_Tests
{
    [TestClass]
    class CreateBook_Should
    {
        [TestMethod]
        public void Succeded_BookCreate()
        {
            var bookDBMock = new Mock<IDatabase<Book>>();
            var authorDBMock = new Mock<IDatabase<Author>>();
            var genreDBMock = new Mock<IDatabase<Genre>>();
            var publisherDBMock = new Mock<IDatabase<Publisher>>();
            var bookGenreDBMock = new Mock<BookGenreDataBase>();
            var bookFacMock = new Mock<IBookFactory>();
            var authorFacMock = new Mock<IAuthorFactory>();
            var genreFacMock = new Mock<IGenreFactory>();
            var formatterMock = new Mock<IConsoleFormatter>();
            var rendererMock = new Mock<IConsoleRenderer>();

           // var sut = new BookManager(bookDBMock.Object, authorDBMock.Object, genreDBMock.Object, publisherDBMock.Object, bookGenreDBMock.Object, bookFacMock.Object, authorFacMock.Object, genreFacMock.Object, formatterMock.Object, rendererMock.Object);


        }
    }
}
