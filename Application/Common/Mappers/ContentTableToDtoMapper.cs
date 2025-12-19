using Application.DTO;
using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Mappers;

public class ContentTableToDtoMapper
{
    public List<IDto> Map(ContentTable contentTable)
    {
        List<IDto> contentTableDto = new List<IDto>();

        foreach (ContentTableEntry entry in contentTable.Entries)
        {
            List<NoteIdDto> notesDto = new List<NoteIdDto>();
            foreach (Annotation note in entry.Notes)
            {
                notesDto.Add(new NoteIdDto(note.Id, note.Title));
            }
            contentTableDto.Add(new ContentTableEntryDto(entry.Id, entry.ThemeName, notesDto));
        }

        return contentTableDto;
    }
}