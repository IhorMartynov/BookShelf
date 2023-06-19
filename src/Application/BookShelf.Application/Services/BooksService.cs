using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.Application.Contracts.Services;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.Domain.Models;

namespace BookShelf.Application.Services
{
    internal sealed class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;

        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        /// <inheritdoc/>
        public Task<Book> GetBookByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            _booksRepository.GetBookAsync(id, cancellationToken);

        /// <inheritdoc/>
        public Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default) =>
            _booksRepository.GetAllBooksAsync(cancellationToken);

        /// <inheritdoc/>
        public Task<BooksPage> GetBooksAsync(
            string searchText,
            string sortColumnName,
            bool sortAscending,
            int pageNumber,
            int pageSize = 20,
            CancellationToken cancellationToken = default) =>
            _booksRepository.GetBooksAsync(searchText, sortColumnName, sortAscending, pageNumber, pageSize,
                cancellationToken);

        /// <inheritdoc/>
        public Task<Guid> AddBookAsync(AddBookModel model, long categoryId, CancellationToken cancellationToken) =>
            _booksRepository.AddBookAsync(model, categoryId, cancellationToken);

        /// <inheritdoc/>
        public Task UpdateBookAsync(Guid id, UpdateBookModel model, long? categoryId,
            CancellationToken cancellationToken = default) =>
            _booksRepository.UpdateBookAsync(id, model, categoryId, cancellationToken);

        /// <inheritdoc/>
        public Task DeleteBookAsync(Guid id, CancellationToken cancellationToken = default) =>
            _booksRepository.DeleteBookAsync(id, cancellationToken);
    }
}
