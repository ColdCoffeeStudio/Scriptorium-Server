using Domain.Shared;

namespace Domain.Entities;

public class ContentTable : IEntity<ContentTable>
{
    public Guid Id { get; }
    public List<ContentTableEntry> Entries { get; }

    public static Result<ContentTable> Create(List<ContentTableEntry> entries)
    {
        return new Result<ContentTable>(new ContentTable(Guid.NewGuid(), entries), Error.Empty(), true);
    }

    public static ContentTable Empty()
    {
        return new ContentTable(Guid.Empty, new List<ContentTableEntry>());
    }

    private ContentTable(Guid id, List<ContentTableEntry> entries)
    {
        Id = id;
        Entries = entries;
    }
}