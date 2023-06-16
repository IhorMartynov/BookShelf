using BookShelf.Application.Contracts.Services;
using BookShelf.Domain.Models;
using BookShelf.WebApi.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.WebApi.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBooksService _booksService;

    public BooksController(IBooksService booksService) =>
        _booksService = booksService;

    [HttpGet]
    public Task<IEnumerable<Book>> GetAll(CancellationToken cancellationToken) =>
        _booksService.GetAllBooksAsync(cancellationToken);

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var book = await _booksService.GetBookByIdAsync(id, cancellationToken);

        return book is null
            ? NotFound()
            : Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] AddBookDto model, CancellationToken cancellationToken)
    {
        var id = await _booksService.AddBookAsync(model.BookDetails, model.CategoryId, cancellationToken);

        return CreatedAtAction(nameof(Get), new{id}, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] Guid id, [FromBody] UpdateBookDto model, CancellationToken cancellationToken)
    {
        await _booksService.UpdateBookAsync(id, model.BookDetails, model.CategoryId, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        await _booksService.DeleteBookAsync(id, cancellationToken);

        return Ok();
    }
}