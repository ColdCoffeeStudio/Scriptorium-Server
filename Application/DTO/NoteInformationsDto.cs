namespace Application.DTO;

public sealed record NoteInformationsDto(int Id, string ContentUrl, int StartPage, int EndPage, string Tags, DateOnly Date): IDto;