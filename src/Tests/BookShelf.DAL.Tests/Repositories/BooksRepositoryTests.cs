using AutoFixture;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.DAL.Repositories;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Tests.Repositories;

[TestClass]
public sealed class BooksRepositoryTests : RepositoryTestsBase
{
    [TestMethod]
    public async Task GIVEN_BookId_WHEN_GetBookAsyncCalled_THEN_BookObjectIsReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var entity = CreateBookEntity();
        context.Books.Add(entity);
        await context.SaveChangesAsync();

        var category = new Book();
        bookMapperMock.Setup(mapper => mapper.Map(It.Is<BookEntity>(x => x.Id == entity.Id)))
            .Returns(category)
            .Verifiable();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var result = await repository.GetBookAsync(entity.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(category);
        bookMapperMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_NotExistingBookId_WHEN_GetBookAsyncCalled_THEN_NullIsReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var result = await repository.GetBookAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public async Task GIVEN_Books_WHEN_GetAllBooksAsyncCalled_THEN_BookObjectsAreReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var entities = Enumerable.Range(1, 10)
            .Select(_ => CreateBookEntity())
            .ToArray();
        context.Books.AddRange(entities);
        await context.SaveChangesAsync();

        var ids = entities.Select(x => x.Id).ToArray();
        bookMapperMock.Setup(mapper => mapper.Map(It.Is<BookEntity>(x => ids.Contains(x.Id))))
            .Returns<BookEntity>(x => new Book
                {Id = x.Id, Title = x.Title, Author = x.Author})
            .Verifiable();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var result = (await repository.GetAllBooksAsync())
            ?.ToArray();

        // Assert
        result.Should().NotBeNull()
            .And.HaveSameCount(entities);
        result.Should().Equal(entities, (model, entity) => model.Id == entity.Id
                                                           && model.Title == entity.Title
                                                           && model.Author == entity.Author);
        bookMapperMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_AddBookModel_WHEN_AddBookAsyncCalled_THEN_BookEntityIsAddedToDb()
    {
        // Arrange
        const long categoryId = 1;

        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var model = Fixture.Create<AddBookModel>();

        bookEntityMapperMock.Setup(mapper =>
                mapper.Map(It.Is<AddBookModel>(x => x == model), It.Is<long>(x => x == categoryId)))
            .Returns(new BookEntity
            {
                Title = model.Title,
                Author = model.Author,
                CategoryId = categoryId,
                ISBN = model.ISBN,
                PublicationYear = model.PublicationYear,
                Quantity = model.Quantity
            })
            .Verifiable();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var result = await repository.AddBookAsync(model, categoryId);

        // Assert
        var entity = await context.Books.FirstOrDefaultAsync();
        entity.Should().NotBeNull();
        entity.Title.Should().Be(model.Title);
        entity.Author.Should().Be(model.Author);
        entity.ISBN.Should().Be(model.ISBN);
        entity.PublicationYear.Should().Be(model.PublicationYear);
        entity.Quantity.Should().Be(model.Quantity);
        entity.CategoryId.Should().Be(categoryId);
        result.Should().Be(entity.Id);
        bookEntityMapperMock.Verify();
    }

    [DataTestMethod]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", "111-222-333-444", "2023", "90", "2",
        "Title 1", "Author 1", "111-222-333-444", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "", "Author 1", "111-222-333-444", "2023", "90", "2",
        "Title", "Author 1", "111-222-333-444", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        null, "Author 1", "111-222-333-444", "2023", "90", "2",
        "Title", "Author 1", "111-222-333-444", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "", "111-222-333-444", "2023", "90", "2",
        "Title 1", "Author", "111-222-333-444", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", null, "111-222-333-444", "2023", "90", "2",
        "Title 1", "Author", "111-222-333-444", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", "", "2023", "90", "2",
        "Title 1", "Author 1", "111-222-333", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", null, "2023", "90", "2",
        "Title 1", "Author 1", "111-222-333", 2023, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", "111-222-333-444", null, "90", "2",
        "Title 1", "Author 1", "111-222-333-444", 2022, 90, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", "111-222-333-444", "2023", null, "2",
        "Title 1", "Author 1", "111-222-333-444", 2023, 100, 2)]
    [DataRow("Title", "Author", "111-222-333", 2022, 100, 1,
        "Title 1", "Author 1", "111-222-333-444", "2023", "90", null,
        "Title 1", "Author 1", "111-222-333-444", 2023, 90, 1)]
    public async Task GIVEN_UpdateBookModel_WHEN_UpdateBookAsyncCalled_THEN_BookEntityIsUpdatedToDb(
        string initialTitle, string initialAuthor, string initialISBN, int initialPublicationYear, int initialQuantity, long initialCategoryId,
        string updatedTitle, string updatedAuthor, string updatedISBN, string updatedPublicationYear, string updatedQuantity, string updatedCategoryId,
        string expectedTitle, string expectedAuthor, string expectedISBN, int expectedPublicationYear, int expectedQuantity, long expectedCategoryId)
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var initialEntity = new BookEntity
        {
            Title = initialTitle,
            Author = initialAuthor,
            ISBN = initialISBN,
            PublicationYear = initialPublicationYear,
            Quantity = initialQuantity,
            CategoryId = initialCategoryId
        };
        context.Books.Add(initialEntity);
        await context.SaveChangesAsync();

        var model = new UpdateBookModel
        {
            Title = updatedTitle,
            Author = updatedAuthor,
            ISBN = updatedISBN,
            PublicationYear = int.TryParse(updatedPublicationYear, out var year) ? year : null,
            Quantity = int.TryParse(updatedQuantity, out var quantity) ? quantity : null
        };

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        await repository.UpdateBookAsync(initialEntity.Id, model,
            long.TryParse(updatedCategoryId, out var categoryId) ? categoryId : null);

        // Assert
        var entity = await context.Books.FindAsync(initialEntity.Id);
        entity.Should().NotBeNull();
        entity.Title.Should().Be(expectedTitle);
        entity.Author.Should().Be(expectedAuthor);
        entity.ISBN.Should().Be(expectedISBN);
        entity.PublicationYear.Should().Be(expectedPublicationYear);
        entity.Quantity.Should().Be(expectedQuantity);
        entity.CategoryId.Should().Be(expectedCategoryId);
    }

    [TestMethod]
    public async Task GIVEN_NotExistingBookId_WHEN_UpdateBookAsyncCalled_THEN_ExceptionIsThrown()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var model = new UpdateBookModel();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() =>
            repository.UpdateBookAsync(Guid.NewGuid(), model, null));

        // Assert
        exception.Should().NotBeNull();
        exception.EntityName.Should().Be(nameof(BookEntity));
    }

    [TestMethod]
    public async Task GIVEN_BookId_WHEN_DeleteBookAsyncCalled_THEN_BookEntityIsDeletedFromDb()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var initialEntity = CreateBookEntity();
        context.Books.Add(initialEntity);
        await context.SaveChangesAsync();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        await repository.DeleteBookAsync(initialEntity.Id);

        // Assert
        var entity = await context.Books.FindAsync(initialEntity.Id);
        entity.Should().BeNull();
    }

    [TestMethod]
    public async Task GIVEN_NotExistingBookId_WHEN_DeleteBookAsyncCalled_THEN_ExceptionIsThrown()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var bookMapperMock = new Mock<IBookMapper>();
        var bookEntityMapperMock = new Mock<IBookEntityMapper>();

        var repository = new BooksRepository(context, bookMapperMock.Object, bookEntityMapperMock.Object);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() =>
            repository.DeleteBookAsync(Guid.NewGuid()));

        // Assert
        exception.Should().NotBeNull();
        exception.EntityName.Should().Be(nameof(BookEntity));
    }

    private BookEntity CreateBookEntity(Guid id = default) =>
        Fixture.Build<BookEntity>()
            .With(x => x.Id, () => id)
            .With(x => x.Category, () => Fixture.Build<CategoryEntity>()
                .Without(c => c.Books)
                .Create())
            .Create();
}