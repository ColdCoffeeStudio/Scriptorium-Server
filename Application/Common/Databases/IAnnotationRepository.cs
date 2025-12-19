using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Databases;

public interface IAnnotationRepository
{
    Task<Result<AnnotationList>> FetchAnnotationFromEncyclopedia(int encyclopediaId, CancellationToken cancellationToken);
    Task<Result<Annotation>> FetchAnnotationFromId(int id, CancellationToken cancellationToken);
}