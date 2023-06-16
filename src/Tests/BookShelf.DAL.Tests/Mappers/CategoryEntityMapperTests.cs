using BookShelf.DAL.Mappers;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Tests.Mappers;

[TestClass]
public sealed class CategoryEntityMapperTests
{
    [TestMethod]
    public void GIVEN_UpdateCategoryModel_WHEN_MapCalled_THEN_CategoryEntityObjectCreated()
    {
        // Arrange
        var updateCategoryModel = new UpdateCategoryModel
        {
            Name = "Category name",
            Description = "Category description"
        };

        var mapper = new CategoryEntityMapper();

        // Act
        var result = mapper.Map(updateCategoryModel);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(default);
        result.Name.Should().Be(updateCategoryModel.Name);
        result.Description.Should().Be(updateCategoryModel.Description);
    }
}