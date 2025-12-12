namespace Application.DTO;

public record EncyclopediaDto(
    int Id,
    string Title,
    Guid ScribeId
): IDto;