namespace Application.DTO;

public record AnnotationDto(
    int Id,
    string Title,
    int StartPage,
    int LastPage,
    string ContentUrl,
    string Tags,
    DateOnly Date,
    int ThemeId,
    int EncyclopediaId
): IDto;