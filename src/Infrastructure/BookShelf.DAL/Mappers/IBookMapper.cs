using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal interface IBookMapper
    {

        /// <summary>
        /// Maps a BookEntity to a Book object.
        /// </summary>
        /// <param name="bookEntity">Book entity.</param>
        /// <returns></returns>
        Book Map(BookEntity bookEntity);
    }
}