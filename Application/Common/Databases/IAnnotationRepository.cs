using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Databases;

public interface IAnnotationRepository
{
    Task<Result<AnnotationList>> FetchAnnotationFromEncyclopedia(Encyclopedia encyclopedia, CancellationToken cancellationToken);
}