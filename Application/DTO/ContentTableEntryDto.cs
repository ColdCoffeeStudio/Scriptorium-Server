namespace Application.DTO;

public sealed record ContentTableEntryDto(int Id, string ThemeName, List<NoteIdDto> notes): IDto;