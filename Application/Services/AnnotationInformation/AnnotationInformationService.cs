using Application.AnnotationOperations.Queries.FetchAnnotationFromId;
using Application.Common.Mappers;
using Application.DTO;
using Domain.Entities;
using Domain.Errors;
using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Services.AnnotationInformation;

public class AnnotationInformationService(ISender sender, ILogger<AnnotationInformationService> logger): IAnnotationInformationService
{
    public async Task<AnswerDto> HandleAnnotationInformationAsync(int id, CancellationToken cancellationToken)
    {
        AnswerDto result;
        AnnotationToDtoMapper mapper = new AnnotationToDtoMapper();
        AnnotationInformationServiceErrors errors = new AnnotationInformationServiceErrors();

        Result<Annotation> annotation = await sender.Send(new FetchAnnotationFromIdQuery(id), cancellationToken);
        
        return annotation.Succeeded
            ? new AnswerDto(true, mapper.Map(annotation.Value), Error.Empty())
            : new AnswerDto(false, mapper.Map(Annotation.Empty()), annotation.Error);
    }
}