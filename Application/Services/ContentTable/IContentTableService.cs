using Application.DTO;

namespace Application.Services.ContentTable;

public interface IContentTableService
{
    Task<AnswerDto> HandleTableContentAsync(int encyclopediaId, CancellationToken cancellationToken);
}