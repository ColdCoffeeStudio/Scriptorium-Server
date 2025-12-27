using Application.DTO;
using Application.Services.ContentTableSearch;
using Application.Services.EncyclopediaSearch;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Route("api/v1/encyclopedia")]
public class EncyclopediaController(
    ISender sender,
    IEncyclopediaSearchService encyclopediaSearchService,
    IContentTableService contentTableService
): ApiController(sender)
{
    [HttpGet("filters")]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        AnswerListDto result = await encyclopediaSearchService.HandleEncyclopediaSearchAsync(cancellationToken);
        
        return result.Success
            ? Ok(result.Value)
            : BadRequest(result.error);
    }

    [HttpGet("content-table/{encyclopediaId}")]
    public async Task<IActionResult> GetContentTable(int encyclopediaId, CancellationToken cancellationToken)
    {
        AnswerListDto result = await contentTableService.HandleContentTableAsync(encyclopediaId, cancellationToken);
        IActionResult response;

        Error notFoundError = new AnnotationRepositoryErrors()
            .EncyclopediaNotFound(encyclopediaId);

        if (result.Success)
        {
            response = Ok(result.Value);
        }
        else if (result.error.Equals(notFoundError))
        {
            response = NotFound();
        }
        else
        {
            response = BadRequest(result.error);
        }
        
        return response;
    }
}