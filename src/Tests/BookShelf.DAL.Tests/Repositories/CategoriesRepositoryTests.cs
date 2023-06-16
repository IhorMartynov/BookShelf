using AutoFixture;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.DAL.Repositories;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Tests.Repositories;

[TestClass]
public sealed class CategoriesRepositoryTests : RepositoryTestsBase
{
    [TestMethod]
    public async Task GIVEN_CategoryId_WHEN_GetCategoryAsyncCalled_THEN_CategoryObjectIsReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var entity = CreateCategoryEntity();
        context.Categories.Add(entity);
        await context.SaveChangesAsync();

        var category = new Category();
        categoryMapperMock.Setup(mapper => mapper.Map(It.Is<CategoryEntity>(x => x.Id == entity.Id)))
            .Returns(category)
            .Verifiable();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var result = await repository.GetCategoryAsync(entity.Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(category);
        categoryMapperMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_NotExistingCategoryId_WHEN_GetCategoryAsyncCalled_THEN_NullIsReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var result = await repository.GetCategoryAsync(1);

        // Assert
        result.Should().BeNull();
    }

    [TestMethod]
    public async Task GIVEN_Categories_WHEN_GetAllCategoriesAsyncCalled_THEN_CategoryObjectsAreReturned()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var entities = Enumerable.Range(1, 10)
            .Select(_ => CreateCategoryEntity())
            .ToArray();
        context.Categories.AddRange(entities);
        await context.SaveChangesAsync();

        var ids = entities.Select(x => x.Id).ToArray();
        categoryMapperMock.Setup(mapper => mapper.Map(It.Is<CategoryEntity>(x => ids.Contains(x.Id))))
            .Returns<CategoryEntity>(x => new Category {Id = x.Id, Name = x.Name, Description = x.Description})
            .Verifiable();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var result = (await repository.GetAllCategoriesAsync())
            ?.ToArray();

        // Assert
        result.Should().NotBeNull()
            .And.HaveSameCount(entities);
        result.Should().Equal(entities, (model, entity) => model.Id == entity.Id
                                                           && model.Name == entity.Name
                                                           && model.Description == entity.Description);
        categoryMapperMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_UpdateCategoryModel_WHEN_AddCategoryAsyncCalled_THEN_CategoryEntityIsAddedToDb()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var model = new UpdateCategoryModel {Name = "Category name", Description = "description"};

        categoryEntityMapperMock.Setup(mapper => mapper.Map(It.Is<UpdateCategoryModel>(x => x == model)))
            .Returns(new CategoryEntity {Name = model.Name, Description = model.Description})
            .Verifiable();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var result = await repository.AddCategoryAsync(model);

        // Assert
        var entity = await context.Categories.FirstOrDefaultAsync();
        entity.Should().NotBeNull();
        entity.Name.Should().Be(model.Name);
        entity.Description.Should().Be(model.Description);
        result.Should().Be(entity.Id);
        categoryEntityMapperMock.Verify();
    }

    [DataTestMethod]
    [DataRow("name", "category", "name 1", "category 1", "name 1", "category 1")]
    [DataRow("name", "category", "", "category 1", "name", "category 1")]
    [DataRow("name", "category", null, "category 1", "name", "category 1")]
    [DataRow("name", "category", "name 1", "", "name 1", "category")]
    [DataRow("name", "category", "name 1", null, "name 1", "category")]
    public async Task GIVEN_UpdateCategoryModel_WHEN_UpdateCategoryAsyncCalled_THEN_CategoryEntityIsUpdatedToDb(
        string initialName, string initialDescription,
        string updatedName, string updatedDescription,
        string expectedName, string expectedDescription)
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var initialEntity = new CategoryEntity {Name = initialName, Description = initialDescription};
        context.Categories.Add(initialEntity);
        await context.SaveChangesAsync();

        var model = new UpdateCategoryModel {Name = updatedName, Description = updatedDescription};

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        await repository.UpdateCategoryAsync(initialEntity.Id, model);

        // Assert
        var entity = await context.Categories.FindAsync(initialEntity.Id);
        entity.Should().NotBeNull();
        entity.Name.Should().Be(expectedName);
        entity.Description.Should().Be(expectedDescription);
    }

    [TestMethod]
    public async Task GIVEN_NotExistingCategoryId_WHEN_UpdateCategoryAsyncCalled_THEN_ExceptionIsThrown()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var model = new UpdateCategoryModel {Name = "updatedName", Description = "updatedDescription"};

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() =>
            repository.UpdateCategoryAsync(1, model));

        // Assert
        exception.Should().NotBeNull();
        exception.EntityName.Should().Be(nameof(CategoryEntity));
    }

    [TestMethod]
    public async Task GIVEN_CategoryId_WHEN_DeleteCategoryAsyncCalled_THEN_CategoryEntityIsDeletedFromDb()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var initialEntity = CreateCategoryEntity();
        context.Categories.Add(initialEntity);
        await context.SaveChangesAsync();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        await repository.DeleteCategoryAsync(initialEntity.Id);

        // Assert
        var entity = await context.Categories.FindAsync(initialEntity.Id);
        entity.Should().BeNull();
    }

    [TestMethod]
    public async Task GIVEN_NotExistingCategoryId_WHEN_DeleteCategoryAsyncCalled_THEN_ExceptionIsThrown()
    {
        // Arrange
        await using var context = await CreateEmptyLibraryContext();
        var categoryMapperMock = new Mock<ICategoryMapper>();
        var categoryEntityMapperMock = new Mock<ICategoryEntityMapper>();

        var repository = new CategoriesRepository(context, categoryMapperMock.Object, categoryEntityMapperMock.Object);

        // Act
        var exception = await Assert.ThrowsExceptionAsync<EntityNotFoundException>(() =>
            repository.DeleteCategoryAsync(1));

        // Assert
        exception.Should().NotBeNull();
        exception.EntityName.Should().Be(nameof(CategoryEntity));
    }

    private CategoryEntity CreateCategoryEntity(long id = default) =>
        Fixture.Build<CategoryEntity>()
            .With(x => x.Id, () => id)
            .Without(x => x.Books)
            .Create();

}