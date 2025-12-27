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
    private readonly ContentTableServiceErrors _errors = new();
    private readonly ContentTableToDtoMapper _mapper = new();

    public async Task<AnswerListDto> HandleContentTableAsync(int encyclopediaId, CancellationToken cancellationToken)
    {
        Result<AnnotationList> annotationsList = await sender.Send(new FetchAnnotationFromEncyclopediaIdQuery(encyclopediaId), cancellationToken);

        return annotationsList.Succeeded 
            ? BuildContentTable(annotationsList.Value) 
            : new AnswerListDto(false, new List<IDto>(), annotationsList.Error);
    }

    private AnswerListDto BuildContentTable(AnnotationList annotationsList)
    {
        List<ContentTableEntry> contentTableEntries = [];
        List<Annotation> annotations = annotationsList.Annotations;
        List<Theme> themes = annotations
            .Select(a => a.Theme)
            .Distinct()
            .ToList();

        foreach (Annotation annotation in annotations)
        {
            Theme theme = annotation.Theme;
            
            List<Annotation> tmpNotes = annotations
                .Where(a => a.Theme.Id == theme.Id)
                .ToList();

            Result<ContentTableEntry> tmpContentTableEntry =
                ContentTableEntry.Create(theme.Id, theme.Name, tmpNotes);

            if (tmpContentTableEntry.Failed)
            {
                return new AnswerListDto(false, new List<IDto>(), _errors.ContentTableEntryCreationError(tmpContentTableEntry.Error));
            }
            
            contentTableEntries.Add(tmpContentTableEntry.Value);
        }

        Result<ContentTable> tmpContentTable = ContentTable.Create(contentTableEntries);
                
        AnswerListDto result = tmpContentTable.Succeeded
            ? new AnswerListDto(true, _mapper.Map(tmpContentTable.Value), Error.Empty())
            : new AnswerListDto(false, new List<IDto>(), _errors.ContentTableCreationError(tmpContentTable.Error));

        return result;
    }
}