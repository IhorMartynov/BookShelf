using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Repositories
{
    internal sealed class CategoriesRepository : ICategoriesRepository
    {
        private readonly LibraryContext _libraryContext;
        private readonly ICategoryMapper _categoryMapper;
        private readonly ICategoryEntityMapper _categoryEntityMapper;

        public CategoriesRepository(LibraryContext libraryContext,
            ICategoryMapper categoryMapper,
            ICategoryEntityMapper categoryEntityMapper)
        {
            _libraryContext = libraryContext;
            _categoryMapper = categoryMapper;
            _categoryEntityMapper = categoryEntityMapper;
        }

        /// <inheritdoc/>
        public async Task<Category> GetCategoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return entity is null
                ? null
                : _categoryMapper.Map(entity);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _libraryContext.Categories
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);

            return entities.Select(_categoryMapper.Map)
                .ToArray();
        }

        /// <inheritdoc/>
        public async Task<long> AddCategoryAsync(UpdateCategoryModel category, CancellationToken cancellationToken = default)
        {
            var entity = _categoryEntityMapper.Map(category);

            _libraryContext.Categories.Add(entity);

            await _libraryContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateCategoryAsync(long id, UpdateCategoryModel category, CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Categories
                             .FindAsync(new object[] { id }, cancellationToken)
                         ?? throw new EntityNotFoundException(nameof(CategoryEntity));

            if (!string.IsNullOrEmpty(category.Name))
                entity.Name = category.Name;
            if (!string.IsNullOrEmpty(category.Description))
                entity.Description = category.Description;

            await _libraryContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteCategoryAsync(long id, CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Categories
                             .FindAsync(new object[] { id }, cancellationToken)
                         ?? throw new EntityNotFoundException(nameof(CategoryEntity));

            _libraryContext.Categories.Remove(entity);

            await _libraryContext.SaveChangesAsync(cancellationToken);
        }
    }
}
