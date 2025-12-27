using Application.Common.Abstractions.Messaging;
using Application.Common.Databases;
using Domain.Entities;
using Domain.Shared;
using Microsoft.Extensions.Logging;

namespace Application.AnnotationOperations.Queries.FetchAnnotationFromEncyclopediaId;

public sealed class FetchAnnotationFromEncyclopediaIdQueryHandler(
    IAnnotationRepository annotationRepository,
    IEncyclopediaRepository encyclopediaRepository,
    ILogger<FetchAnnotationFromEncyclopediaIdQueryHandler> logger) : IQueryHandler<FetchAnnotationFromEncyclopediaIdQuery, AnnotationList>
{
    public async Task<Result<AnnotationList>> Handle(FetchAnnotationFromEncyclopediaIdQuery request, CancellationToken cancellationToken)
    {
        return await annotationRepository.FetchAnnotationFromEncyclopedia(request.EncyclopediaId, cancellationToken);
    }
}