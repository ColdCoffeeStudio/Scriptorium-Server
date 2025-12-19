using Application.Common.Abstractions.Messaging;
using Application.Common.Databases;
using Domain.Entities;
using Domain.Shared;

namespace Application.AnnotationOperations.Queries.FetchAnnotationFromId;

public class FetchAnnotationFromIdQueryHandler(
    IAnnotationRepository repository
    ): IQueryHandler<FetchAnnotationFromIdQuery, Annotation>
{
    public async Task<Result<Annotation>> Handle(FetchAnnotationFromIdQuery request,
        CancellationToken cancellationToken)
    {
        return await repository.FetchAnnotationFromId(request.id, cancellationToken);
    }
}