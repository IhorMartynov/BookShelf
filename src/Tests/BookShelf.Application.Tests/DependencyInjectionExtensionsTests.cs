using BookShelf.Application.Contracts.Services;
using BookShelf.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelf.Application.Tests
{
    [TestClass]
    public sealed class DependencyInjectionExtensionsTests
    {
        [DataTestMethod]
        [DataRow(typeof(IBooksService), typeof(BooksService), ServiceLifetime.Scoped)]
        [DataRow(typeof(ICategoriesService), typeof(CategoriesService), ServiceLifetime.Scoped)]
        public void GIVEN_DependencyType_WHEN_AddRepositoriesCalled_THEN_DependencyIsRegistered(
            Type serviceType,
            Type expectedImplementationType,
            ServiceLifetime expectedServiceLifetime)
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddApplicationServices();

            // Assert
            services.Should().Contain(x => x.ServiceType == serviceType
                                           && x.ImplementationType == expectedImplementationType
                                           && x.Lifetime == expectedServiceLifetime);
        }
    }
}