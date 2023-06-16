using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal interface ICategoryMapper
    {
        /// <summary>
        /// Maps a <see cref="CategoryEntity"/> object to a <see cref="Category"/> object.
        /// </summary>
        /// <param name="category">Category entity.</param>
        /// <returns></returns>
        Category Map(CategoryEntity category);
    }
}