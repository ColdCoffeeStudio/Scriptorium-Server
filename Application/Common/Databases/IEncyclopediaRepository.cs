using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Databases;

public interface IEncyclopediaRepository
{
    Task<Result<EncyclopediaList>> FetchEncyclopedia(CancellationToken cancellationToken);
}