using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Contracts.Services
{
    public interface ICategoriesService
    {
        /// <summary>
        /// Gets a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<Category> GetCategoryByIdAsync(long id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Adds a category to a storage.
        /// </summary>
        /// <param name="model">Category details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Category identifier.</returns>
        Task<long> AddCategoryAsync(UpdateCategoryModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a category.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="model">Category details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task UpdateCategoryAsync(long id, UpdateCategoryModel model, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a category from storage.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task DeleteCategoryAsync(long id, CancellationToken cancellationToken);
    }
}