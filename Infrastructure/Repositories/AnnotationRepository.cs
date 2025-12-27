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
    private readonly AnnotationRepositoryErrors _errors = new();
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

            Result<Encyclopedia> encyclopedia = await MatchingEncyclopedia(encyclopediaId);

            if (encyclopedia.Succeeded)
            {
                List<Domain.Entities.Annotation> annotations = new List<Domain.Entities.Annotation>();
                    
                foreach (Annotation annotation in queryAnnotations)
                {
                    Result<Domain.Entities.Annotation> tmpAnnotation = await Map(annotation, queryThemes, encyclopedia.Value);
                    
                    if (tmpAnnotation.Succeeded)
                    {
                        annotations.Add(tmpAnnotation.Value);
                    }
                    else
                    {
                        return new Result<AnnotationList>(AnnotationList.Empty(), 
                            _errors.AnnotationCreationError(annotation.id,tmpAnnotation.Error),false);
                    }
                }
            
                Result<AnnotationList> annotationList = AnnotationList.Create(annotations);

                result = annotationList.Succeeded
                    ? annotationList
                    : new Result<AnnotationList>(AnnotationList.Empty(), _errors.AnnotationListCreationError(annotationList.Error), false);

            }
            else
            {
                result = new Result<AnnotationList>(AnnotationList.Empty(),
                    encyclopedia.Error, false);
            }
        }
        else
        {
            result = new Result<AnnotationList>(AnnotationList.Empty(), _errors.DatabaseConnectionError(), false);
        }

        return result;
    }

    public async Task<Result<Domain.Entities.Annotation>> FetchAnnotationFromId(int id, CancellationToken cancellationToken)
    {
        Annotation? queryAnnotation = await context.Annotations.FindAsync(id, cancellationToken);
    
        if (queryAnnotation is not { } annotation)
        {
            return new Result<Domain.Entities.Annotation>(
                Domain.Entities.Annotation.Empty(), 
                _errors.AnnotationNotFound(id), 
                false);
        }
        
        Result<Domain.Entities.Annotation> result;
        
        List<Theme> queryThemes = await (
            from t in context.Themes
            orderby t.id
            select t
        ).ToListAsync(cancellationToken: cancellationToken);
        
        Result<Domain.Entities.Theme> theme = await MatchingTheme(annotation.themeId);

        if (theme.Failed)
        {
            result = new Result<Domain.Entities.Annotation>(Domain.Entities.Annotation.Empty(), _errors.ThemeCreationError(annotation.id, annotation.themeId, theme.Error), false);
        }
        else
        {
            Result<Encyclopedia> encyclopedia = await MatchingEncyclopedia(annotation.encyclopediaId);
            
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
                : new Result<Domain.Entities.Annotation>(Domain.Entities.Annotation.Empty(), _errors.EncyclopediaCreationError(annotation.encyclopediaId, theme.Error), false);
        }

        return result;
    }
    
    private async Task<Result<Domain.Entities.Theme>> MatchingTheme(int annotationThemeId)
    {
        Theme? queryTheme = await context.Themes.FindAsync(annotationThemeId);

        return queryTheme is not { } theme 
            ? new Result<Domain.Entities.Theme>(
                Domain.Entities.Theme.Empty(),
                _errors.ThemeNotFound(annotationThemeId),
                false) 
            : Domain.Entities.Theme.Create(theme.id, theme.name, theme.folder);
    }

    private async Task<Result<Encyclopedia>> MatchingEncyclopedia(int annotationEncyclopediaId)
    {
        Result<Encyclopedia> result;
        Encyclopedium? queryEncyclopedia = await context.Encyclopedia.FindAsync(annotationEncyclopediaId);
    
        if (queryEncyclopedia is not { } encyclopedia)
        {
            return new Result<Encyclopedia>(Encyclopedia.Empty(),
                _errors.EncyclopediaNotFound(annotationEncyclopediaId), false);
        }
        
        Result<Domain.Entities.Scribe> scribe = await MatchingScribe(queryEncyclopedia.scribeId);

        return scribe.Succeeded
            ? Encyclopedia.Create(queryEncyclopedia.id, queryEncyclopedia.title, scribe.Value)
            : new Result<Encyclopedia>(Encyclopedia.Empty(), scribe.Error,
                false);
        
    }

    private async Task<Result<Domain.Entities.Scribe>> MatchingScribe(string scribeId)
    {
        Scribe? queryScribe = await context.Scribes.FindAsync(scribeId);

        return queryScribe is not { } scribe
            ? new Result<Domain.Entities.Scribe>(
                Domain.Entities.Scribe.Empty(),
                _errors.ScribeNotFound(scribeId),
                false)
            : Domain.Entities.Scribe.Create(new Guid(scribe.id), scribe.username);
    }

    private async Task<Result<Domain.Entities.Annotation>> Map(Annotation queryAnnotation, List<Theme> queryThemes, Encyclopedia encyclopedia)
    {
        Result<Domain.Entities.Theme> theme = await MatchingTheme(queryAnnotation.themeId);

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
                _errors.ThemeCreationError(queryAnnotation.id, queryAnnotation.themeId, theme.Error), false);
    }
}