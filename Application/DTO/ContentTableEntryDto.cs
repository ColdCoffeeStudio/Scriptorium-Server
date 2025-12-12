namespace Application.DTO;

public sealed record ContentTableEntryDto(int Id, string ThemeName, NoteIdDto[] notes): IDto;