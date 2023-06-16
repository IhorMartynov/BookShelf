using BookShelf.Domain.Models;

namespace BookShelf.WebApi.Core.Models;

public record AddBookDto(AddBookModel BookDetails, long CategoryId);