using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Application.Contracts.Services;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Services
{
    internal sealed class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        /// <inheritdoc/>
        public Task<Category> GetCategoryByIdAsync(long id, CancellationToken cancellationToken) =>
            _categoriesRepository.GetCategoryAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken) =>
            _categoriesRepository.GetAllCategoriesAsync(cancellationToken);

        /// <inheritdoc/>
        public Task<long> AddCategoryAsync(UpdateCategoryModel model, CancellationToken cancellationToken) =>
            _categoriesRepository.AddCategoryAsync(model, cancellationToken);

        /// <inheritdoc/>
        public Task UpdateCategoryAsync(long id, UpdateCategoryModel model,
            CancellationToken cancellationToken) =>
            _categoriesRepository.UpdateCategoryAsync(id, model, cancellationToken);

        /// <inheritdoc/>
        public Task DeleteCategoryAsync(long id, CancellationToken cancellationToken) =>
            _categoriesRepository.DeleteCategoryAsync(id, cancellationToken);
    }
}
