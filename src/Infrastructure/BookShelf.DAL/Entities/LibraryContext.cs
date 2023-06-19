using System;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Entities
{
    public class LibraryContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<SearchBooksWithPaginationResult> SearchBooksWithPaginationResults { get; set; }

        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Ignore<SearchBooksWithPaginationResult>();

            modelBuilder.Entity<CategoryEntity>()
                .HasData(new CategoryEntity {Id = 1, Name = "Fiction", Description = "Fiction"},
                    new CategoryEntity {Id = 2, Name = "Fantasy", Description = "Fantasy"},
                    new CategoryEntity {Id = 3, Name = "Poem", Description = "Poem"},
                    new CategoryEntity {Id = 4, Name = "Classic", Description = "Classic"});

            modelBuilder.Entity<BookEntity>()
                .HasData(
                    new BookEntity
                    {
                        Id = new Guid("DED9E342-EF0D-4C2A-BDD0-8E5FF2A117ED"),
                        Title = "War and Peace (tom 1)", Author = "L. Tolstoy", CategoryId = 4, ISBN = "123456",
                        PublicationYear = 2001, Quantity = 10
                    },
                    new BookEntity
                    {
                        Id = new Guid("36022756-536A-4099-B0D2-65A099D2714F"),
                        Title = "War and Peace (tom 2)", Author = "L. Tolstoy", CategoryId = 4, ISBN = "123457",
                        PublicationYear = 2001, Quantity = 10
                    },
                    new BookEntity
                    {
                        Id = new Guid("D6F5CD92-6FD3-472F-A3B8-0AA15FAA8A83"),
                        Title = "War and Peace (tom 3)", Author = "L. Tolstoy", CategoryId = 4, ISBN = "123458",
                        PublicationYear = 2001, Quantity = 10
                    },
                    new BookEntity
                    {
                        Id = new Guid("567289DB-D2F4-41E7-B417-C292B0378913"),
                        Title = "Amber Chronicles", Author = "Zelazny", CategoryId = 1, ISBN = "123459",
                        PublicationYear = 1998, Quantity = 20
                    },
                    new BookEntity
                    {
                        Id = new Guid("08AC84B7-62BE-40EE-9128-02B3F8FDF967"),
                        Title = "Hobbit", Author = "J.R.R. Tolkien", CategoryId = 2, ISBN = "123460",
                        PublicationYear = 1983, Quantity = 5
                    },
                    new BookEntity
                    {
                        Id = new Guid("1B7652F5-BE71-48E9-9493-FE54976DB20A"),
                        Title = "Hamlet", Author = "W. Shakespeare", CategoryId = 3, ISBN = "123461",
                        PublicationYear = 1985, Quantity = 15
                    });

            base.OnModelCreating(modelBuilder);
        }
    }
}
