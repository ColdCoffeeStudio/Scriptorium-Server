using Application.Common.Abstractions.Messaging;
using Domain.Entities;

namespace Application.ContentTableOperations.Queries.FetchContentTable;

public sealed record FetchAnnotationFromEncyclopediaIdQuery(int EncyclopediaId): IQuery<AnnotationList>;