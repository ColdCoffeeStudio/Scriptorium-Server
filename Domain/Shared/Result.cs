using Domain.Entities;

namespace Domain.Shared;

public class Result<TEntity>(TEntity value, Error error, bool succeeded)
    where TEntity : IEntity<TEntity>
{
    public TEntity Value { get; } = value;
    public Error Error { get; } = error;
    public bool Succeeded { get; } = succeeded;
    public bool Failed => !Succeeded;

    public static Result<TEntity> Success(TEntity value) 
        => new Result<TEntity>(value, Error.Empty(), true);

    public static Result<TEntity> Failure(Error error) 
        => new Result<TEntity>(TEntity.Empty(), error, false);
}