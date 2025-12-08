namespace Domain.Entities;

public interface IEntity<out TEntity>
{
    public static abstract TEntity Empty();
}