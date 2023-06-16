using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal interface IBookEntityMapper
    {
        /// <summary>
        /// Maps a <see cref="Book"/> object to a <see cref="BookEntity"/> object.
        /// </summary>
        /// <param name="book">Book object.</param>
        /// <param name="categoryId">Category identifier.</param>
        /// <returns></returns>
        BookEntity Map(AddBookModel book, long categoryId);
    }
}