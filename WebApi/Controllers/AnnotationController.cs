using Application.DTO;
using Application.Services.AnnotationInformation;
using Domain.Errors;
using Domain.Shared;
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
        IActionResult response;

        Error notFoundError = new AnnotationRepositoryErrors().AnnotationNotFound(id);
        
        if (result.Success)
        {
            response = Ok(result.Value);
        }
        else if(result.error.Equals(notFoundError))
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