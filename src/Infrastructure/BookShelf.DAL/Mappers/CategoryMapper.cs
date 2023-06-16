using BookShelf.DAL.Entities;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Mappers
{
    internal sealed class CategoryMapper : ICategoryMapper
    {
        /// <inheritdoc/>
        public Category Map(CategoryEntity category) =>
            new Category
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
    }
}
