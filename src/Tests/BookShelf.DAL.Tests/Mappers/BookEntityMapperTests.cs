using BookShelf.DAL.Mappers;
using BookShelf.Domain.Models;


namespace BookShelf.DAL.Tests.Mappers;

[TestClass]
public sealed class BookEntityMapperTests
{
    [TestMethod]
    public void GIVEN_AddBookModel_WHEN_MapCalled_THEN_BookEntityObjectCreated()
    {
        // Arrange
        const long categoryId = 1;
        var addBookModel = new AddBookModel
        {
            Title = "Book Title",
            Author = "Some author",
            ISBN = "111-222-333-444-555",
            PublicationYear = 2001,
            Quantity = 123
        };

        var mapper = new BookEntityMapper();

        // Act
        var result = mapper.Map(addBookModel, categoryId);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeEmpty();
        result.Title.Should().Be(addBookModel.Title);
        result.Author.Should().Be(addBookModel.Author);
        result.ISBN.Should().Be(addBookModel.ISBN);
        result.PublicationYear.Should().Be(addBookModel.PublicationYear);
        result.Quantity.Should().Be(addBookModel.Quantity);
        result.CategoryId.Should().Be(categoryId);
    }
}