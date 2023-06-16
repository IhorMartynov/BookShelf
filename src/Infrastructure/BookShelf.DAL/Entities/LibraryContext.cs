using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Entities
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }
    }
}
