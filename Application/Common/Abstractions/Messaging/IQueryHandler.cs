using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Common.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IEntity<TResponse>
{
    new Task<Result<TResponse>> Handle(TQuery query,CancellationToken cancellationToken);
}