using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal sealed class CategoryEntityMapper : ICategoryEntityMapper
    {
        /// <inheritdoc/>
        public CategoryEntity Map(UpdateCategoryModel category) =>
            new CategoryEntity
            {
                Name = category.Name,
                Description = category.Description
            };
    }
}
