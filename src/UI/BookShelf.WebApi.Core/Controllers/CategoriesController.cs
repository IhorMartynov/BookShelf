using BookShelf.Application.Contracts.Services;
using BookShelf.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookShelf.WebApi.Core.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService) =>
        _categoriesService = categoriesService;

    [HttpGet]
    public Task<IEnumerable<Category>> GetAll(CancellationToken cancellationToken) =>
        _categoriesService.GetAllCategoriesAsync(cancellationToken);

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Category), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Get([FromRoute] long id, CancellationToken cancellationToken)
    {
        var book = await _categoriesService.GetCategoryByIdAsync(id, cancellationToken);

        return book is null
            ? NotFound()
            : Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] UpdateCategoryModel model, CancellationToken cancellationToken)
    {
        var id = await _categoriesService.AddCategoryAsync(model, cancellationToken);

        return CreatedAtAction(nameof(Get), new{id}, id);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] long id, [FromBody] UpdateCategoryModel model, CancellationToken cancellationToken)
    {
        await _categoriesService.UpdateCategoryAsync(id, model, cancellationToken);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete([FromRoute] long id, CancellationToken cancellationToken)
    {
        await _categoriesService.DeleteCategoryAsync(id, cancellationToken);

        return Ok();
    }
}