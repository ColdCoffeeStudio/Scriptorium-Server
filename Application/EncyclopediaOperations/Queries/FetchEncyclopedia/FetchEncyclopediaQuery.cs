using Application.Common.Abstractions.Messaging;
using Domain.Entities;

namespace Application.EncyclopediaOperations.Queries.FetchEncyclopedia;

public sealed record FetchEncyclopediaQuery(): IQuery<EncyclopediaList>;