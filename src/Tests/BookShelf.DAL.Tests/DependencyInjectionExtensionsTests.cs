using BookShelf.DAL.Contracts.Repositories;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelf.DAL.Tests;

[TestClass]
public sealed class DependencyInjectionExtensionsTests
{
    [DataTestMethod]
    [DataRow(typeof(LibraryContext), typeof(LibraryContext), ServiceLifetime.Scoped)]
    [DataRow(typeof(IBookMapper), typeof(BookMapper), ServiceLifetime.Singleton)]
    [DataRow(typeof(IBookEntityMapper), typeof(BookEntityMapper), ServiceLifetime.Singleton)]
    [DataRow(typeof(ICategoryMapper), typeof(CategoryMapper), ServiceLifetime.Singleton)]
    [DataRow(typeof(ICategoryEntityMapper), typeof(CategoryEntityMapper), ServiceLifetime.Singleton)]
    [DataRow(typeof(IBooksRepository), typeof(BooksRepository), ServiceLifetime.Scoped)]
    [DataRow(typeof(ICategoriesRepository), typeof(CategoriesRepository), ServiceLifetime.Scoped)]
    public void GIVEN_DependencyType_WHEN_AddRepositoriesCalled_THEN_DependencyIsRegistered(
        Type serviceType,
        Type expectedImplementationType,
        ServiceLifetime expectedServiceLifetime)
    {
        // Arrange
        const string connectionString = "connection-string";

        var services = new ServiceCollection();

        // Act
        services.AddRepositories(connectionString);

        // Assert
        services.Should().Contain(x => x.ServiceType == serviceType
                                       && x.ImplementationType == expectedImplementationType
                                       && x.Lifetime == expectedServiceLifetime);
    }

    [DataTestMethod]
    [DataRow(typeof(LibraryContext))]
    [DataRow(typeof(IBookMapper))]
    [DataRow(typeof(IBookEntityMapper))]
    [DataRow(typeof(ICategoryMapper))]
    [DataRow(typeof(ICategoryEntityMapper))]
    [DataRow(typeof(IBooksRepository))]
    [DataRow(typeof(ICategoriesRepository))]
    public void GIVEN_DependencyType_WHEN_AddRepositoriesCalled_THEN_DependencyCanBeInstantiated(Type serviceType)
    {
        // Arrange
        const string connectionString = "connection-string";

        var services = new ServiceCollection();

        // Act
        services.AddRepositories(connectionString);

        // Assert
        var sp = services.BuildServiceProvider();
        var service = sp.GetService(serviceType);
        service.Should().NotBeNull();
    }
}