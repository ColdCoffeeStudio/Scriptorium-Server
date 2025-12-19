using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Databases;

public interface IEncyclopediaRepository
{
    Task<Result<EncyclopediaList>> FetchEncyclopedia(CancellationToken cancellationToken);
    Task<Result<Encyclopedia>> FetchEncyclopediaFromId(int encyclopediaId, CancellationToken cancellationToken);
}