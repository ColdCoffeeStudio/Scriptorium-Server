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
        Result<Encyclopedia> encyclopedia = await encyclopediaRepository.FetchEncyclopediaFromId(request.EncyclopediaId, cancellationToken);

        return encyclopedia.Succeeded
            ? await annotationRepository.FetchAnnotationFromEncyclopedia(encyclopedia.Value, cancellationToken)
            : new Result<AnnotationList>(AnnotationList.Empty(), encyclopedia.Error, false);
    }
}