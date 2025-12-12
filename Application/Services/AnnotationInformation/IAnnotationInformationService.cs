using Application.DTO;

namespace Application.Services.AnnotationInformation;

public interface IAnnotationInformationService
{
    Task<AnswerDto> HandleAnnotationInformationAsync(int annotationId, CancellationToken cancellationToken);
}