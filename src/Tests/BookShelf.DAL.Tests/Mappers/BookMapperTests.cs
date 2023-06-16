using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;

namespace BookShelf.DAL.Tests.Mappers;

[TestClass]
public sealed class BookMapperTests
{
    [TestMethod]
    public void GIVEN_BookEntity_WHEN_MapCalled_THEN_BookObjectCreated()
    {
        // Arrange
        var bookEntity = new BookEntity
        {
            Id = Guid.NewGuid(),
            Title = "Book Title",
            Author = "Some author",
            ISBN = "111-222-333-444-555",
            PublicationYear = 2001,
            Quantity = 123,
            CategoryId = 1,
            Category = new CategoryEntity {Id = 1, Name = "Category name"}
        };

        var mapper = new BookMapper();

        // Act
        var result = mapper.Map(bookEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(bookEntity.Id);
        result.Title.Should().Be(bookEntity.Title);
        result.Author.Should().Be(bookEntity.Author);
        result.ISBN.Should().Be(bookEntity.ISBN);
        result.PublicationYear.Should().Be(bookEntity.PublicationYear);
        result.Quantity.Should().Be(bookEntity.Quantity);
        result.CategoryId.Should().Be(bookEntity.CategoryId);
        result.CategoryName.Should().Be(bookEntity.Category.Name);
    }
}