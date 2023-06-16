using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Domain.Models;

namespace BookShelf.DAL.Contracts.Repositories
{
    public interface IBooksRepository
    {
        /// <summary>
        /// Get all books from the books repository.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Search for books in the repository.
        /// </summary>
        /// <param name="searchText">String to search.</param>
        /// <param name="sortColumnName">Name of the column to sort by.</param>
        /// <param name="sortAscending">Should sorting be ascending?</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetBooksAsync(string searchText,
            string sortColumnName,
            bool sortAscending,
            int pageNumber,
            int pageSize = 20,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Get book by its identifier.
        /// </summary>
        /// <param name="id">Book identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Add a book to the repository.
        /// </summary>
        /// <param name="book"></param>
        /// <param name="categoryId"></param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Identifier of the created entity.</returns>
        Task<Guid> AddBookAsync(AddBookModel book, long categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Update a book in the repository.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="updateBookModel">Book details.</param>
        /// <param name="categoryId">Category identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task UpdateBookAsync(Guid bookId, UpdateBookModel updateBookModel, long? categoryId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Delete a book with the given ID.
        /// </summary>
        /// <param name="bookId">Book identifier.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns></returns>
        Task DeleteBookAsync(Guid bookId, CancellationToken cancellationToken = default);
    }
}
