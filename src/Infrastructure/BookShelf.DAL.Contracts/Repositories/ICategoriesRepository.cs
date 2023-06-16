using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Contracts.Repositories
{
    public interface ICategoriesRepository
    {
        /// <summary>
        /// Get category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<Category> GetCategoryAsync(long id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a category to the repository.
        /// </summary>
        /// <param name="category">Category details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<long> AddCategoryAsync(UpdateCategoryModel category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update category details.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="category">Category details.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task UpdateCategoryAsync(long id, UpdateCategoryModel category, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a category by its identifier.
        /// </summary>
        /// <param name="id">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task DeleteCategoryAsync(long id, CancellationToken cancellationToken = default);
    }
}