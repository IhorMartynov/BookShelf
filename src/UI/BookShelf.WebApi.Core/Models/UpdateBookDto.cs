using BookShelf.Domain.Models;

namespace BookShelf.WebApi.Core.Models;

public record UpdateBookDto(UpdateBookModel BookDetails, long? CategoryId);