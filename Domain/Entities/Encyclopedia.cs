using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Encyclopedia : IEntity<Encyclopedia>
{
    public int Id { get; }
    public string Title { get; }
    public Scribe Scribe { get; }

    public static Result<Encyclopedia> Create(int id, string title, Scribe scribe)
    {
        EncyclopediaErrors errors = new EncyclopediaErrors();
        Result<Encyclopedia> result;

        if (id <= 0)
        {
            result = new Result<Encyclopedia>(Encyclopedia.Empty(), errors.MissingId(), false);
        }
        else if (String.IsNullOrWhiteSpace(title))
        {
            result = new Result<Encyclopedia>(Encyclopedia.Empty(), errors.MissingTitle(), false);
        }
        else
        {
            result = scribe.Equals(Scribe.Empty()) 
                ? new Result<Encyclopedia>(Encyclopedia.Empty(), errors.MissingScribe(), false) 
                : new Result<Encyclopedia>(new Encyclopedia(id, title, scribe), Error.Empty(), true);
        }
        
        return result;
    }

    public static Encyclopedia Empty()
    {
        return new Encyclopedia(-1, "", Scribe.Empty());
    }

    private Encyclopedia(int id, string title, Scribe scribe)
    {
        Id = id;
        Title = title;
        Scribe = scribe;
    }
}