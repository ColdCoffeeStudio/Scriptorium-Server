using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstraction;

public abstract class ApiController(ISender sender) : ControllerBase
{
    protected ISender Sender = sender;
}