using BookShelf.Application.Services;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Tests.Services;

[TestClass]
public sealed class CategoriesServiceTests
{
    [TestMethod]
    public async Task GIVEN_CategoryId_WHEN_GetCategoryByIdAsyncCalled_THEN_CategoryObjectIsReturned()
    {
        // Arrange
        const long id = 21;
        var cancellationSource = new CancellationTokenSource();
        var category = new Category();

        var repositoryMock = new Mock<ICategoriesRepository>();
        repositoryMock.Setup(repository => repository.GetCategoryAsync(It.Is<long>(x => x == id),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(category)
            .Verifiable();

        var service = new CategoriesService(repositoryMock.Object);

        // Act
        var result = await service.GetCategoryByIdAsync(id, cancellationSource.Token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(category);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_Categories_WHEN_GetAllCategoriesAsyncCalled_THEN_CategoryObjectsAreReturned()
    {
        // Arrange
        var cancellationSource = new CancellationTokenSource();
        var categories = new[] {new Category()};

        var repositoryMock = new Mock<ICategoriesRepository>();
        repositoryMock.Setup(repository => repository.GetAllCategoriesAsync(
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(categories)
            .Verifiable();

        var service = new CategoriesService(repositoryMock.Object);

        // Act
        var result = await service.GetAllCategoriesAsync(cancellationSource.Token);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeSameAs(categories);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_UpdateCategoryModel_WHEN_AddCategoryAsyncCalled_THEN_CategoryEntityIsAddedToDb()
    {
        // Arrange
        const long id = 22;
        var model = new UpdateCategoryModel();
        var cancellationSource = new CancellationTokenSource();

        var repositoryMock = new Mock<ICategoriesRepository>();
        repositoryMock.Setup(repository => repository.AddCategoryAsync(
                It.Is<UpdateCategoryModel>(x => x == model),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .ReturnsAsync(id)
            .Verifiable();

        var service = new CategoriesService(repositoryMock.Object);

        // Act
        var result = await service.AddCategoryAsync(model, cancellationSource.Token);

        // Assert
        result.Should().Be(id);
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_UpdateCategoryModel_WHEN_UpdateCategoryAsyncCalled_THEN_CategoryEntityIsUpdatedToDb()
    {
        // Arrange
        const long id = 22;
        var model = new UpdateCategoryModel();
        var cancellationSource = new CancellationTokenSource();

        var repositoryMock = new Mock<ICategoriesRepository>();
        repositoryMock.Setup(repository => repository.UpdateCategoryAsync(
                It.Is<long>(x => x == id),
                It.Is<UpdateCategoryModel>(x => x == model),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var service = new CategoriesService(repositoryMock.Object);

        // Act
        await service.UpdateCategoryAsync(id, model, cancellationSource.Token);

        // Assert
        repositoryMock.Verify();
    }

    [TestMethod]
    public async Task GIVEN_CategoryId_WHEN_DeleteCategoryAsyncCalled_THEN_CategoryEntityIsDeletedFromDb()
    {
        // Arrange
        const long id = 22;
        var cancellationSource = new CancellationTokenSource();

        var repositoryMock = new Mock<ICategoriesRepository>();
        repositoryMock.Setup(repository => repository.DeleteCategoryAsync(
                It.Is<long>(x => x == id),
                It.Is<CancellationToken>(x => x == cancellationSource.Token)))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var service = new CategoriesService(repositoryMock.Object);

        // Act
        await service.DeleteCategoryAsync(id, cancellationSource.Token);

        // Assert
        repositoryMock.Verify();
    }
}