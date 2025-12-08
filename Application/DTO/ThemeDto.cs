namespace Application.DTO;

public record ThemeDto(Guid Id, string Name, string Folder): IDto;