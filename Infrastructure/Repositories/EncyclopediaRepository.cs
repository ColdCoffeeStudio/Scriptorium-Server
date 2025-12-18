using Application.Common.Databases;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Context;
using Infrastructure.Contexts;
using Microsoft.Extensions.Logging;
using Scribe = Infrastructure.Contexts.Scribe;

namespace Infrastructure.Repositories;

internal sealed class EncyclopediaRepository(ScriptoriumDbContext context, ILogger<EncyclopediaRepository> logger) : IEncyclopediaRepository
{
    public async Task<Result<EncyclopediaList>> FetchEncyclopedia(CancellationToken cancellationToken)
    {
        await using (context)
        {
            EncyclopediaRepositoryErrors errors = new EncyclopediaRepositoryErrors();
            bool canConnect = await context.Database.CanConnectAsync(cancellationToken);
            Result<EncyclopediaList> result;
        
            if (canConnect)
            {
                List<Encyclopedium> queryEncyclopedias = (
                    from e in context.Encyclopedia
                    orderby e.id
                    select e
                ).ToList();

                List<Scribe> queryScribes = (
                    from s in context.Scribes
                    orderby s.id
                    select s
                ).ToList();
                
                List<Encyclopedia> encyclopedias = new List<Encyclopedia>();
                
                foreach (Encyclopedium encyclopedia in queryEncyclopedias)
                {

                    Result<Domain.Entities.Scribe> matchingScribe = MatchingScribe(queryScribes, new Guid(encyclopedia.scribeId));
                    if (matchingScribe.Failed)
                    {
                        return new Result<EncyclopediaList>(EncyclopediaList.Empty(),
                            matchingScribe.Error, false);
                    }

                    Result<Encyclopedia> tmpEncyclopedia = Encyclopedia.Create(encyclopedia.id, encyclopedia.title, matchingScribe.Value);

                    if (tmpEncyclopedia.Failed)
                    {
                        return new Result<EncyclopediaList>(EncyclopediaList.Empty(),
                            errors.InvalidData(encyclopedia.id, tmpEncyclopedia.Error), false);
                    }
                    
                    encyclopedias.Add(tmpEncyclopedia.Value);
                }

                result = EncyclopediaList.Create(encyclopedias);
            }
            else
            {
                result = new Result<EncyclopediaList>(EncyclopediaList.Empty(), errors.DatabaseConnectionError(), false);
            }

            return result;
        }
    }

    private Result<Domain.Entities.Scribe> MatchingScribe(List<Scribe> queryScribes, Guid scribeId)
    {
        Result<Domain.Entities.Scribe> result = new Result<Domain.Entities.Scribe>(Domain.Entities.Scribe.Empty(), EncyclopediaRepositoryErrors.NoScribeFound(scribeId), false);

        foreach (Scribe scribe in queryScribes)
        {
            Guid queryScribeId = new Guid(scribe.id);
            if (queryScribeId == scribeId)
            {
                result = Domain.Entities.Scribe.Create(queryScribeId, scribe.username);
            }
        }
        
        return result;
    }
}