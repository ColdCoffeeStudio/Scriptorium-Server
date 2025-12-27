using Application.Common.Mappers;
using Application.DTO;
using Application.EncyclopediaOperations.Queries.FetchEncyclopedia;
using Domain.Entities;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.EncyclopediaSearch;

public class EncyclopediaSearchService(ISender sender, ILogger<EncyclopediaSearchService> logger): IEncyclopediaSearchService
{
    public async Task<AnswerListDto> HandleEncyclopediaSearchAsync(CancellationToken cancellationToken)
    {
        EncyclopediaListAnswerListDtoMapper mapper = new EncyclopediaListAnswerListDtoMapper();
        Result<EncyclopediaList> encyclopediaList = await sender.Send(new FetchEncyclopediaQuery(), cancellationToken);
        
        return mapper.Map(encyclopediaList.Value);
    }
}