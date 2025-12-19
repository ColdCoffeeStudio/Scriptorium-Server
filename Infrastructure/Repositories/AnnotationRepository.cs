using Application.Common.Databases;
using Domain.Entities;
using Domain.Shared;
using Domain.Errors;
using Infrastructure.Context;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Annotation = Infrastructure.Contexts.Annotation;
using Scribe = Infrastructure.Contexts.Scribe;
using Theme = Infrastructure.Contexts.Theme;

namespace Infrastructure.Repositories;

internal sealed class AnnotationRepository(ScriptoriumDbContext context, ILogger<EncyclopediaRepository> logger) : IAnnotationRepository
{
    readonly AnnotationRepositoryErrors errors = new AnnotationRepositoryErrors();
    public async Task<Result<AnnotationList>> FetchAnnotationFromEncyclopedia(int encyclopediaId, CancellationToken cancellationToken)
    {
        bool canConnect = await context.Database.CanConnectAsync(cancellationToken);
        Result<AnnotationList> result;

        if (canConnect)
        {
            List<Annotation> queryAnnotations = await (
                from a in context.Annotations
                where a.encyclopediaId == encyclopediaId
                orderby a.id
                select a
            ).ToListAsync(cancellationToken: cancellationToken);
            
            List<Theme> queryThemes = await (
                from t in context.Themes
                orderby t.id
                select t
            ).ToListAsync(cancellationToken: cancellationToken);

            Result<Encyclopedia> encyclopedia = MatchingEncyclopedia(encyclopediaId);

            if (encyclopedia.Succeeded)
            {
                List<Domain.Entities.Annotation> annotations = new List<Domain.Entities.Annotation>();
                    
                
                foreach (Annotation annotation in queryAnnotations)
                {
                    Result<Domain.Entities.Annotation> tmpAnnotation = Map(annotation, queryThemes, encyclopedia.Value);
                    
                    if (tmpAnnotation.Succeeded)
                    {
                        annotations.Add(tmpAnnotation.Value);
                    }
                    else
                    {
                        return new Result<AnnotationList>(AnnotationList.Empty(), 
                            errors.AnnotationCreationError(annotation.id,tmpAnnotation.Error),false);
                    }
                }
            
                Result<AnnotationList> annotationList = AnnotationList.Create(annotations);

                result = annotationList.Succeeded
                    ? annotationList
                    : new Result<AnnotationList>(AnnotationList.Empty(), errors.AnnotationListCreationError(annotationList.Error), false);

            }
            else
            {
                result = new Result<AnnotationList>(AnnotationList.Empty(),
                    errors.EncyclopediaCreationError(encyclopediaId, encyclopedia.Error), false);
            }
        }
        else
        {
            result = new Result<AnnotationList>(AnnotationList.Empty(), errors.DatabaseConnectionError(), false);
        }

        return result;
    }

    public async Task<Result<Domain.Entities.Annotation>> FetchAnnotationFromId(int id, CancellationToken cancellationToken)
    {
        Result<Domain.Entities.Annotation> result;
        Annotation annotation = context.Annotations.Find(id)!;
        
        List<Theme> queryThemes = await (
            from t in context.Themes
            orderby t.id
            select t
        ).ToListAsync(cancellationToken: cancellationToken);
        
        Result<Domain.Entities.Theme> theme = MatchingTheme(queryThemes, annotation.themeId);

        if (theme.Failed)
        {
            result = new Result<Domain.Entities.Annotation>(Domain.Entities.Annotation.Empty(), errors.ThemeCreationError(annotation.id, annotation.themeId, theme.Error), false);
        }
        else
        {
            Result<Encyclopedia> encyclopedia = MatchingEncyclopedia(annotation.encyclopediaId);
            
            result = encyclopedia.Succeeded
                ? Domain.Entities.Annotation.Create(
                    annotation.id,
                    annotation.title,
                    annotation.startPage,
                    annotation.endPage,
                    annotation.contentUrl,
                    annotation.tags,
                    annotation.date,
                    theme.Value,
                    encyclopedia.Value
                )
                : new Result<Domain.Entities.Annotation>(Domain.Entities.Annotation.Empty(), errors.EncyclopediaCreationError(annotation.encyclopediaId, theme.Error), false);
        }

        return result;
    }
    
    private Result<Domain.Entities.Theme> MatchingTheme(List<Theme> queryThemes, int annotationThemeId)
    {
        Theme queryTheme = queryThemes.FirstOrDefault(t => t.id == annotationThemeId)!;
        
        return Domain.Entities.Theme.Create(queryTheme.id, queryTheme.name, queryTheme.folder);       
    }

    private Result<Encyclopedia> MatchingEncyclopedia(int annotationEncyclopediaId)
    {
        Encyclopedium queryEncyclopedia = (
            from e in context.Encyclopedia
            where e.id == annotationEncyclopediaId
            orderby e.id
            select e
        ).FirstOrDefault()!;

        Result<Domain.Entities.Scribe> scribe = MatchingScribe(queryEncyclopedia.scribeId);

        return scribe.Succeeded
            ? Encyclopedia.Create(queryEncyclopedia.id, queryEncyclopedia.title, scribe.Value)
            : new Result<Encyclopedia>(Encyclopedia.Empty(), errors.ScribeCreationError(queryEncyclopedia.scribeId, scribe.Error),
                false);
    }

    private Result<Domain.Entities.Scribe> MatchingScribe(string scribeId)
    {
        Scribe queryScribe = (
            from s in context.Scribes
            where s.id == scribeId
            orderby s.id
            select s).FirstOrDefault()!;
        
        return Domain.Entities.Scribe.Create(new Guid(queryScribe.id), queryScribe.username);
    }

    private Result<Domain.Entities.Annotation> Map(Annotation queryAnnotation, List<Theme> queryThemes, Encyclopedia encyclopedia)
    {
        Result<Domain.Entities.Theme> theme = MatchingTheme(queryThemes, queryAnnotation.themeId);

        return theme.Succeeded
            ? Domain.Entities.Annotation.Create(
                queryAnnotation.id,
                queryAnnotation.title,
                queryAnnotation.startPage,
                queryAnnotation.endPage,
                queryAnnotation.contentUrl,
                queryAnnotation.tags,
                queryAnnotation.date,
                theme.Value,
                encyclopedia
            )
            : new Result<Domain.Entities.Annotation>(Domain.Entities.Annotation.Empty(),
                errors.ThemeCreationError(queryAnnotation.id, queryAnnotation.themeId, theme.Error), false);
    }
}