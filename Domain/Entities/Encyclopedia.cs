using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Encyclopedia : IEntity<Encyclopedia>
{
    private Guid Id { get; }
    private string Title { get; }
    private Scribe Scribe { get; }

    public static Result<Encyclopedia> Create(string title, Scribe scribe)
    {
        EncyclopediaErrors errors = new EncyclopediaErrors();
        Result<Encyclopedia> result;
        
        if (String.IsNullOrWhiteSpace(title))
        {
            result = new Result<Encyclopedia>(Encyclopedia.Empty(), errors.MissingTitle(), false);
        }
        else
        {
            result = scribe.Equals(Scribe.Empty()) 
                ? new Result<Encyclopedia>(Encyclopedia.Empty(), errors.MissingScribe(), false) 
                : new Result<Encyclopedia>(new Encyclopedia(Guid.NewGuid(), title, scribe), Error.Empty(), true);
        }
        
        return result;
    }

    public static Encyclopedia Empty()
    {
        return new Encyclopedia(Guid.Empty, "", Scribe.Empty());
    }

    private Encyclopedia(Guid id, string title, Scribe scribe)
    {
        Id = id;
        Title = title;
        Scribe = scribe;
    }
}