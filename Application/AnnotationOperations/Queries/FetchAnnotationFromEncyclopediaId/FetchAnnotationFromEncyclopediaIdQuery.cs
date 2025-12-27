using Application.Common.Abstractions.Messaging;
using Domain.Entities;

namespace Application.AnnotationOperations.Queries.FetchAnnotationFromEncyclopediaId;

public sealed record FetchAnnotationFromEncyclopediaIdQuery(int EncyclopediaId): IQuery<AnnotationList>;