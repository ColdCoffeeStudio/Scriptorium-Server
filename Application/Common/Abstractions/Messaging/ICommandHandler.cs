using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Common.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> 
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IEntity<TResponse>
{
    new Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
}