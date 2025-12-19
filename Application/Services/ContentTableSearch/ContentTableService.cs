using Application.AnnotationOperations.Queries.FetchAnnotationFromEncyclopediaId;
using Application.Common.Mappers;
using Application.DTO;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.ContentTableSearch;

public class ContentTableService (ISender sender, ILogger<ContentTableService> logger): IContentTableService
{
    public async Task<AnswerListDto> HandleContentTableAsync(int encyclopediaId, CancellationToken cancellationToken)
    {
        AnswerListDto result;
        ContentTableServiceErrors errors = new ContentTableServiceErrors();
        ContentTableToDtoMapper mapper = new ContentTableToDtoMapper();
        Result<AnnotationList> annotationsList = await sender.Send(new FetchAnnotationFromEncyclopediaIdQuery(encyclopediaId), cancellationToken);

        if (annotationsList.Succeeded)
        {
            List<ContentTableEntry> contentTableEntries = new List<ContentTableEntry>();
            List<Annotation> annotations = annotationsList.Value.Annotations;
            List<Theme> themes = annotations
                .Select(a => a.Theme)
                .Distinct()
                .ToList();

            int themeIndex = 0;
            bool errorOccured = false;
            Error loopError = Error.Empty();

            while (themeIndex < themes.Count && !errorOccured)
            {
                Theme theme = themes.ElementAt(themeIndex);
                List<Annotation> tmpNotes = annotations
                    .Where(a => a.Theme.Id == theme.Id)
                    .ToList();

                Result<ContentTableEntry> tmpContentTableEntry =
                    ContentTableEntry.Create(theme.Id, theme.Name, tmpNotes);

                if (tmpContentTableEntry.Failed)
                {
                    errorOccured = true;
                    loopError = errors.ContentTableEntryCreationError(tmpContentTableEntry.Error);
                }
                else
                {
                    contentTableEntries.Add(tmpContentTableEntry.Value);
                    themeIndex++;
                }
            }

            if (!errorOccured)
            {
                if(contentTableEntries.Count == 0)
                {
                    result = new AnswerListDto(true, new List<IDto>(), Error.Empty());
                }
                else
                {
                    Result<ContentTable> tmpContentTable = ContentTable.Create(contentTableEntries);
                    
                    result = tmpContentTable.Succeeded
                        ? new AnswerListDto(true, mapper.Map(tmpContentTable.Value), Error.Empty())
                        : new AnswerListDto(false, new List<IDto>(), errors.ContentTableCreationError(tmpContentTable.Error));
                }
            }
            else
            {
                result = new AnswerListDto(
                    false,
                    new List<IDto>(),
                    loopError);
            }

        }
        else
        {
            result = new AnswerListDto(false, new List<IDto>(), errors.AnnotationListFetchingError(encyclopediaId, annotationsList.Error));
        }

        return result;
    }
}