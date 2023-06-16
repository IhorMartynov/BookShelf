using AutoFixture;
using BookShelf.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Tests.Repositories;

public abstract class RepositoryTestsBase
{
    protected Fixture Fixture = new Fixture();

    protected static async Task<LibraryContext> CreateEmptyLibraryContext()
    {
        var options = new DbContextOptionsBuilder<LibraryContext>()
            .UseInMemoryDatabase("LibraryDb")
            .Options;

        var context = new LibraryContext(options);
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        return context;
    }

}