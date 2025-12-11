using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Abstraction;

namespace WebApi.Controllers;

[Route("api/v1/annotation")]
public class AnnotationController(ISender sender): ApiController(sender)
{
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(new { success = true, message = id });
    }
}