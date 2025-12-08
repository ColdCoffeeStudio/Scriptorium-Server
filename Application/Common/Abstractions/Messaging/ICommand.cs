using Domain.Entities;
using Domain.Shared;
using MediatR;

namespace Application.Common.Abstractions.Messaging;

public interface ICommand<TEntity> : IRequest<Result<TEntity>> 
    where TEntity : IEntity<TEntity>
{
}
