using Application.DTO;
using Domain.Entities;

namespace Application.Common.Mappers;

public class AnnotationToDtoMapper
{
    public AnnotationDto Map(Annotation annotation)
    {
        AnnotationDto annotationDto = new AnnotationDto(
            annotation.Id,
            annotation.Title,
            annotation.StartPage,
            annotation.EndPage,
            annotation.ContentUrl,
            annotation.Tags,
            annotation.Date,
            annotation.Theme.Id,
            annotation.Encyclopedia.Id
        );
        
        return annotationDto;
    }
}