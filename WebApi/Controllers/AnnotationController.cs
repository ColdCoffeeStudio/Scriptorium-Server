using Application.DTO;
using Application.Services.AnnotationInformation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Route("api/v1/annotation")]
public class AnnotationController(
    ISender sender,
    IAnnotationInformationService annotationInformationService
): ApiController(sender)
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
    {
        AnswerDto result = await annotationInformationService.HandleAnnotationInformationAsync(id, cancellationToken);

        return result.Success
            ? Ok(result.Value)
            : BadRequest(result.error);
    }
}