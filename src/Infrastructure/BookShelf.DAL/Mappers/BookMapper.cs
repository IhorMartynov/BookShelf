using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal sealed class BookMapper : IBookMapper
    {
        /// <inheritdoc/>
        public Book Map(BookEntity bookEntity) =>
            new Book
            {
                Id = bookEntity.Id,
                Title = bookEntity.Title,
                Author = bookEntity.Author,
                ISBN = bookEntity.ISBN,
                PublicationYear = bookEntity.PublicationYear,
                Quantity = bookEntity.Quantity,
                CategoryId = bookEntity.CategoryId,
                CategoryName = bookEntity.Category?.Name
            };
    }
}
