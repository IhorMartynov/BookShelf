using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Contracts.Services
{
    public interface IBooksService
    {
        /// <summary>
        /// Retrieves a book by its ID.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Book object or null if book is not found.</returns>
        Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets all books.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets books from a storage.
        /// </summary>
        /// <param name="searchText">String to search.</param>
        /// <param name="sortColumnName">Name of the column to apply sorting to.</param>
        /// <param name="sortAscending">Should sort be ascending?</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number of books per page.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksAsync(string searchText,
            string sortColumnName,
            bool sortAscending,
            int pageNumber,
            int pageSize = 20,
            CancellationToken cancellationToken = default);


        /// <summary>
        /// Adds a book to a storage.
        /// </summary>
        /// <param name="model">Book details.</param>
        /// <param name="categoryId">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<Guid> AddBookAsync(AddBookModel model, long categoryId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates book details.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <param name="model">Book details.</param>
        /// <param name="categoryId">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task UpdateBookAsync(Guid id, UpdateBookModel model, long? categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes a book.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        /// <exception cref="EntityNotFoundException"></exception>
        Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
