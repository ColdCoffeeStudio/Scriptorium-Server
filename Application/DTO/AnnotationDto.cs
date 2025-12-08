namespace Application.DTO;

public record AnnotationDto(
    Guid Id,
    string Title,
    int StartPage,
    int LastPage,
    string ContentUrl,
    string Tags,
    DateOnly Date,
    Guid ThemeId,
    Guid EncyclopediaId
): IDto;