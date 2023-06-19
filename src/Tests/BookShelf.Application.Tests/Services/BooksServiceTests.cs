using BookShelf.Application.Services;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Tests.Services;

[TestClass]
public sealed class BooksServiceTests
{
    [TestMethod]
    public async Task GIVEN_BookId_WHEN_GetBookByIdAsyncCalled_THEN_BookObjectIsReturned()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationSource = new CancellationTokenSource();
        var book = new Book();

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.GetBookAsync(It.Is<Guid>(x => x == id),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(book)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        var result = await service.GetBookByIdAsync(id, cancellationSource.Token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(book);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_Books_WHEN_GetAllBooksAsyncCalled_THEN_BookObjectsAreReturned()
    {
        // Arrange
        var cancellationSource = new CancellationTokenSource();
        var books = new[] {new Book()};

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.GetAllBooksAsync(
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(books)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        var result = await service.GetAllBooksAsync(cancellationSource.Token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(books);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_Books_WHEN_GetBooksAsyncCalled_THEN_BookObjectsAreReturned()
    {
        // Arrange
        const string searchText = "text to search";
        const string columnName = "Title";
        const bool sortAscending = true;
        const int pageNumber = 1;
        const int pageSize = 33;
        var cancellationSource = new CancellationTokenSource();
        var books = new BooksPage();

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.GetBooksAsync(
                It.Is<string>(x => x == searchText),
                It.Is<string>(x => x == columnName),
                It.Is<bool>(x => x == sortAscending),
                It.Is<int>(x => x == pageNumber),
                It.Is<int>(x => x == pageSize),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(books)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        var result = await service.GetBooksAsync(searchText, columnName, sortAscending, pageNumber, pageSize,
            cancellationSource.Token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(books);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_AddBookModel_WHEN_AddBookAsyncCalled_THEN_BookEntityIsAddedToDb()
    {
        // Arrange
        var addBookModel = new AddBookModel();
        const long categoryId = 1;
        var cancellationSource = new CancellationTokenSource();
        var id = Guid.NewGuid();

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.AddBookAsync(
                It.Is<AddBookModel>(x => x == addBookModel),
                It.Is<long>(x => x == categoryId),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(id)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        var result = await service.AddBookAsync(addBookModel, categoryId, cancellationSource.Token);

        // Assert
        result.Should().Be(id);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_UpdateBookModel_WHEN_UpdateBookAsyncCalled_THEN_BookEntityIsUpdatedToDb()
    {
        // Arrange
        var id = Guid.NewGuid();
        var updateBookModel = new UpdateBookModel();
        const long categoryId = 1;
        var cancellationSource = new CancellationTokenSource();

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.UpdateBookAsync(
                It.Is<Guid>(x => x == id),
                It.Is<UpdateBookModel>(x => x == updateBookModel),
                It.Is<long>(x => x == categoryId),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        await service.UpdateBookAsync(id, updateBookModel, categoryId, cancellationSource.Token);

        // Assert
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_BookId_WHEN_DeleteBookAsyncCalled_THEN_BookEntityIsDeletedFromDb()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cancellationSource = new CancellationTokenSource();

        var repositoryMock = new Mock<IBooksRepository>();
        repositoryMock.Setup(repository => repository.DeleteBookAsync(
                It.Is<Guid>(x => x == id),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var service = new BooksService(repositoryMock.Object);

        // Act
        await service.DeleteBookAsync(id, cancellationSource.Token);

        // Assert
        repositoryMock.Verify();
    }
}