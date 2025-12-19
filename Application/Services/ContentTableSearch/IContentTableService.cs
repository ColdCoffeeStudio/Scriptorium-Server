using Application.DTO;

namespace Application.Services.ContentTableSearch;

public interface IContentTableService
{
    Task<AnswerListDto> HandleContentTableAsync(int encyclopediaId, CancellationToken cancellationToken);
}