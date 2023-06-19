using System;
using System.Configuration;
using BookShelf.Application;
using BookShelf.DAL;
using Microsoft.Extensions.DependencyInjection;

namespace BookShelf.WebForms
{
    public static class ServicesConfig
    {
        public static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddRepositories(ConfigurationManager.ConnectionStrings["LibraryDb"].ConnectionString)
                .AddApplicationServices();

            return services.BuildServiceProvider();
        }
    }
}