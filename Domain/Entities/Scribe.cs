using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Scribe : IEntity<Scribe>
{
    public Guid Id { get; }
    public string Name { get; }
    
    public static Result<Scribe> Create(string name)
    {
        ScribeErrors errors = new ScribeErrors();
        Result<Scribe> result = String.IsNullOrEmpty(name) 
            ? new Result<Scribe>(Scribe.Empty(), errors.MissingName(), false)
            : new Result<Scribe>(new Scribe(Guid.NewGuid(), name), Error.Empty(), true);
        
        return result;
    }

    public static Scribe Empty()
    {
        return new Scribe(Guid.Empty, "");
    }

    private Scribe(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}