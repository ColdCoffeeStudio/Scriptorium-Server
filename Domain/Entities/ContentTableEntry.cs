using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class ContentTableEntry : IEntity<ContentTableEntry>
{
    public int Id { get; }
    public string ThemeName { get; }
    public List<Annotation> Notes { get; }

    public static Result<ContentTableEntry> Create(int id, string themeName, List<Annotation> notes)
    {
        ContentTableEntryErrors errors = new ContentTableEntryErrors();
        Result<ContentTableEntry> result;

        if (id <= 0)
        {
            result = new Result<ContentTableEntry>(ContentTableEntry.Empty(), errors.MissingId(), false);
        }
        else if (string.IsNullOrEmpty(themeName))
        {
            result = new Result<ContentTableEntry>(ContentTableEntry.Empty(), errors.MissingThemeName(), false);
        }
        else
        {
            result = new Result<ContentTableEntry>(new ContentTableEntry(id, themeName, notes), Error.Empty(), true);
        }

        return result;
    }
    
    private ContentTableEntry(int id, string themeName, List<Annotation> notes)
    {
        Id = id;
        ThemeName = themeName;
        Notes = notes;
    }

    public static ContentTableEntry Empty()
    {
        return new ContentTableEntry(-1, "", new List<Annotation>());
    }
}