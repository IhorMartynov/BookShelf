using System;
using BookShelf.Application;
using BookShelf.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelf.WebForms.App_Start
{
    public static class ServicesConfig
    {
        public static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddRepositories("Server=localhost,1433;Database=LibraryDb;User Id=sa;Password=Qwerty@123;Encrypt=False;TrustServerCertificate=True")
                .AddApplicationServices();

            return services.BuildServiceProvider();
        }
    }
}