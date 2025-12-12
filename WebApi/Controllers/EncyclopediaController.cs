using Application.DTO;
using Application.Services.EncyclopediaSearch;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Route("api/v1/encyclopedia")]
public class EncyclopediaController(ISender sender, IEncyclopediaSearchService service): ApiController(sender)
{
    [HttpGet("filters")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("content-table/{encyclopediaId}")]
    public async Task<IActionResult> GetContentTable(int encyclopediaId, CancellationToken cancellationToken)
    {
        AnswerDto result = await service.HandleEncyclopediaSearchAsync(cancellationToken);

        return result.Success
            ? Ok(result.Value)
            : BadRequest(result.error);
    }
}