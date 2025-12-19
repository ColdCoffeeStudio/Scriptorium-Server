using Application.Common.Databases;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using Infrastructure.Context;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Scribe = Infrastructure.Contexts.Scribe;

namespace Infrastructure.Repositories;

internal sealed class EncyclopediaRepository(ScriptoriumDbContext context, ILogger<EncyclopediaRepository> logger) : IEncyclopediaRepository
{
    public async Task<Result<EncyclopediaList>> FetchEncyclopedia(CancellationToken cancellationToken)
    {
        EncyclopediaRepositoryErrors errors = new EncyclopediaRepositoryErrors();
        bool canConnect = await context.Database.CanConnectAsync(cancellationToken);
        Result<EncyclopediaList> result;
    
        if (canConnect)
        {
            List<Encyclopedium> queryEncyclopedias = await (
                from e in context.Encyclopedia
                orderby e.id
                select e
            ).ToListAsync(cancellationToken: cancellationToken);

            List<Scribe> queryScribes = await (
                from s in context.Scribes
                orderby s.id
                select s
            ).ToListAsync(cancellationToken: cancellationToken);
            
            List<Encyclopedia> encyclopedias = new List<Encyclopedia>();
            
            foreach (Encyclopedium encyclopedia in queryEncyclopedias)
            {
                Result<Encyclopedia> tmpEncyclopedia = Map(encyclopedia, queryScribes);
                
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

    public async Task<Result<Encyclopedia>> FetchEncyclopediaFromId(int encyclopediaId,
        CancellationToken cancellationToken)
    {
        Encyclopedium? encyclopedium = await (
            from e in context.Encyclopedia
            where e.id == encyclopediaId
            orderby e.id
            select e
        ).FirstOrDefaultAsync(cancellationToken: cancellationToken)!;

        Scribe scribe = await (
            from s in context.Scribes
            where s.id == encyclopedium.scribeId
            orderby s.id
            select s
        ).FirstOrDefaultAsync(cancellationToken: cancellationToken)!;
        
        return Map(encyclopedium, [(scribe)]);
    }

    private Result<Encyclopedia> Map(Encyclopedium encyclopedia, List<Scribe> queryScribes)
    {
        Result<Domain.Entities.Scribe> matchingScribe = MatchingScribe(queryScribes, new Guid(encyclopedia.scribeId));
        if (matchingScribe.Failed)
        {
            return new Result<Encyclopedia>(Encyclopedia.Empty(),
                matchingScribe.Error, false);
        }

        return Encyclopedia.Create(encyclopedia.id, encyclopedia.title, matchingScribe.Value);
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