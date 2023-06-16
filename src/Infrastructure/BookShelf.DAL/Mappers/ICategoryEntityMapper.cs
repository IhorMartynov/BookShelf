using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal interface ICategoryEntityMapper
    {
        /// <summary>
        /// Maps a <see cref="Category"/> object to a <see cref="CategoryEntity"/> object.
        /// </summary>
        /// <param name="category">Category object.</param>
        /// <returns></returns>
        CategoryEntity Map(UpdateCategoryModel category);
    }
}