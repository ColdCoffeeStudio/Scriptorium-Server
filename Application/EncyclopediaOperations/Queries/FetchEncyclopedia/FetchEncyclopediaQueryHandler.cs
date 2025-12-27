using Application.Common.Abstractions.Messaging;
using Application.Common.Databases;
using Domain.Entities;
using Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Application.EncyclopediaOperations.Queries.FetchEncyclopedia;

public sealed class FetchEncyclopediaQueryHandler(
    IEncyclopediaRepository repository,
    ILogger<FetchEncyclopediaQueryHandler> logger) : IQueryHandler<FetchEncyclopediaQuery, EncyclopediaList>
{
    public async Task<Result<EncyclopediaList>> Handle(FetchEncyclopediaQuery request, CancellationToken cancellationToken)
    {
        Result<EncyclopediaList> result = await repository.FetchEncyclopedia(cancellationToken);
        
        return new Result<EncyclopediaList>(result.Value, result.Error, result.Succeeded);
    }
}