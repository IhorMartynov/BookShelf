using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookShelf.DAL.Entities
{
    [ExcludeFromCodeCoverage]
    public sealed class LibraryContextDesignTimeFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        public LibraryContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer("DesignTimeConnectionString");

            return new LibraryContext(optionsBuilder.Options);
        }
    }
}
