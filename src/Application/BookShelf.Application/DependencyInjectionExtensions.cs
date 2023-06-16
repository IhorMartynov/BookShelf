using System.Runtime.CompilerServices;
using BookShelf.Application.Contracts.Services;
using BookShelf.Application.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("BookShelf.Application.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace BookShelf.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBooksService, BooksService>()
                .AddScoped<ICategoriesService, CategoriesService>();

            return services;
        }

    }
}
