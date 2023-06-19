using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookShelf.DAL.Contracts.Repositories;
using BookShelf.DAL.Entities;
using BookShelf.DAL.Mappers;
using BookShelf.Domain.Exceptions;
using BookShelf.Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BookShelf.DAL.Repositories
{
    internal sealed class BooksRepository : IBooksRepository
    {
        private readonly LibraryContext _libraryContext;
        private readonly IBookMapper _bookMapper;
        private readonly IBookEntityMapper _bookEntityMapper;

        public BooksRepository(LibraryContext libraryContext,
            IBookMapper bookMapper,
            IBookEntityMapper bookEntityMapper)
        {
            _libraryContext = libraryContext;
            _bookMapper = bookMapper;
            _bookEntityMapper = bookEntityMapper;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken = default)
        {
            var entities = await _libraryContext.Books
                .AsNoTracking()
                .Include(x => x.Category)
                .ToArrayAsync(cancellationToken);

            return entities.Select(_bookMapper.Map)
                .ToArray();
        }

        /// <inheritdoc/>
        public async Task<BooksPage> GetBooksAsync(string searchText,
            string sortColumnName,
            bool sortAscending,
            int pageNumber,
            int pageSize = 20,
            CancellationToken cancellationToken = default)
        {
            var pageNumberCorrected = pageNumber > 0 ? pageNumber : 1;
            var pageSizeCorrected = pageSize > 0 ? pageSize : 20;

            var parameters = new object[]
            {
                new SqlParameter("@SearchTerm", SqlDbType.VarChar) {Value = searchText ?? "", Direction = ParameterDirection.Input},
                new SqlParameter("@SortColumn", SqlDbType.VarChar) {Value = GetSortColumn(sortColumnName), Direction = ParameterDirection.Input},
                new SqlParameter("@PageNumber", SqlDbType.Int) {Value = pageNumberCorrected, Direction = ParameterDirection.Input},
                new SqlParameter("@PageSize", SqlDbType.Int) {Value = pageSizeCorrected, Direction = ParameterDirection.Input}
            };

            var books = await _libraryContext.SearchBooksWithPaginationResults
                .FromSqlRaw("EXEC sp_SearchBooksWithPagination @SearchTerm, @SortColumn, @PageNumber, @PageSize", parameters)
                .ToArrayAsync(cancellationToken);

            return new BooksPage
            {
                Books = books.Select(_bookMapper.Map).ToArray(),
                Page = pageNumberCorrected,
                PageSize = pageSizeCorrected
            };
        }

        /// <inheritdoc/>
        public async Task<Book> GetBookAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Books
                .AsNoTracking()
                .Include(x => x.Category)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return entity is null
                ? null
                : _bookMapper.Map(entity);
        }

        /// <inheritdoc/>
        public async Task<Guid> AddBookAsync(AddBookModel book, long categoryId, CancellationToken cancellationToken = default)
        {
            var entity = _bookEntityMapper.Map(book, categoryId);

            _libraryContext.Books.Add(entity);

            await _libraryContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        /// <inheritdoc/>
        public async Task UpdateBookAsync(Guid bookId, UpdateBookModel updateBookModel, long? categoryId,
            CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Books
                             .FindAsync(new object[] {bookId}, cancellationToken)
                         ?? throw new EntityNotFoundException(nameof(BookEntity));

            if (!string.IsNullOrEmpty(updateBookModel.Title))
                entity.Title = updateBookModel.Title;
            if (!string.IsNullOrEmpty(updateBookModel.Author))
                entity.Author = updateBookModel.Author;
            if (!string.IsNullOrEmpty(updateBookModel.ISBN))
                entity.ISBN = updateBookModel.ISBN;
            if (updateBookModel.PublicationYear.HasValue)
                entity.PublicationYear = updateBookModel.PublicationYear.Value;
            if (updateBookModel.Quantity.HasValue)
                entity.Quantity = updateBookModel.Quantity.Value;
            if (categoryId.HasValue)
                entity.CategoryId = categoryId.Value;

            await _libraryContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task DeleteBookAsync(Guid bookId, CancellationToken cancellationToken = default)
        {
            var entity = await _libraryContext.Books
                             .FindAsync(new object[] {bookId}, cancellationToken)
                         ?? throw new EntityNotFoundException(nameof(BookEntity));

            _libraryContext.Books.Remove(entity);

            await _libraryContext.SaveChangesAsync(cancellationToken);
        }

        private static string GetSortColumn(string sortColumn)
        {
            switch (sortColumn?.ToUpperInvariant())
            {
                case "TITLE":
                    return nameof(BookEntity.Title);
                case "AUTHOR":
                    return nameof(BookEntity.Author);
                case "CATEGORY":
                    return nameof(BookEntity.Category);
            }

            return nameof(BookEntity.Title);
        }
    }
}
