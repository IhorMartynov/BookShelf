using AutoFixture;
using BookShelf.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Tests.Repositories;

public abstract class RepositoryTestsBase
{
    protected Fixture Fixture = new();

    protected static async Task<LibraryContext> CreateEmptyLibraryContext()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase("LibraryDb")
            .Options;

        var context = new LibraryContext(options);

        context.Categories.RemoveRange(await context.Categories.ToArrayAsync());
        context.Books.RemoveRange(await context.Books.ToArrayAsync());

        return context;
    }

}