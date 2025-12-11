using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Route("api/v1/encyclopedia")]
public class EncyclopediaController(ISender sender): ApiController(sender)
{
    [HttpGet("filters")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("content-table/{encyclopediaId}")]
    public async Task<IActionResult> GetContentTable(int encyclopediaId, CancellationToken cancellationToken)
    {
        return Ok(new  { success = true, message = encyclopediaId});
    }
}