using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal sealed class BookEntityMapper : IBookEntityMapper
    {
        /// <inheritdoc/>
        public BookEntity Map(AddBookModel book, long categoryId) =>
            new BookEntity
            {
                Title = book.Title,
                Author = book.Author,
                ISBN = book.ISBN,
                PublicationYear = book.PublicationYear,
                Quantity = book.Quantity,
                CategoryId = categoryId
            };
    }
}
