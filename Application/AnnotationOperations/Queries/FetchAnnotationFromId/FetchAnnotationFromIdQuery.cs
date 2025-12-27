using Application.Common.Abstractions.Messaging;
using Domain.Entities;

namespace Application.AnnotationOperations.Queries.FetchAnnotationFromId;

public sealed record FetchAnnotationFromIdQuery(int id): IQuery<Annotation>;