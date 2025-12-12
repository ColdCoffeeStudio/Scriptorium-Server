using Application.DTO;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.EncyclopediaSearch;

public class EncyclopediaSearchService(ISender sender, ILogger<EncyclopediaSearchService> logger): IEncyclopediaSearchService
{
    public async Task<AnswerDto> HandleEncyclopediaSearchAsync(CancellationToken cancellationToken)
    {
        EncyclopediaDto result = new EncyclopediaDto(1, "Volume 1", new Guid("6adf4413-ba64-49eb-9cfe-340005f19ca0"));

        return new AnswerDto(true, result, Error.Empty());
    }
}