using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;

namespace BookShelf.DAL.Tests.Mappers;

[TestClass]
public sealed class CategoryMapperTests
{
    [TestMethod]
    public void GIVEN_CategoryEntity_WHEN_MapCalled_THEN_CategoryObjectCreated()
    {
        // Arrange
        var categoryEntity = new CategoryEntity
        {
            Id = 12,
            Name = "Category name",
            Description = "Category description"
        };

        var mapper = new CategoryMapper();

        // Act
        var result = mapper.Map(categoryEntity);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(categoryEntity.Id);
        result.Name.Should().Be(categoryEntity.Name);
        result.Description.Should().Be(categoryEntity.Description);
    }

}