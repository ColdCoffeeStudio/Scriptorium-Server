using Application.DTO;
using Domain.Shared;

namespace Application.Services.EncyclopediaSearch;

public interface IEncyclopediaSearchService
{
    Task<AnswerListDto> HandleEncyclopediaSearchAsync(CancellationToken cancellationToken);
}