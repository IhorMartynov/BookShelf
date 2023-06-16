using System.Runtime.CompilerServices;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("BookShelf.DAL.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
namespace BookShelf.DAL
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddSingleton<IBookMapper, BookMapper>()
                .AddSingleton<IBookEntityMapper, BookEntityMapper>()
                .AddSingleton<ICategoryMapper, CategoryMapper>()
                .AddSingleton<ICategoryEntityMapper, CategoryEntityMapper>();

            services.AddScoped<IBooksRepository, BooksRepository>()
                .AddScoped<ICategoriesRepository, CategoriesRepository>();

            return services;
        }
    }
}
