namespace Application.DTO;

public record EncyclopediaDto(
    Guid Id,
    string Title,
    Guid ScribeId
): IDto;