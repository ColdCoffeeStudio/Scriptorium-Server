using Domain.Shared;

namespace Domain.Errors;

public class ScribeErrors
{
    public Error MissingName()
    {
        return new Error("Scribe.MissingName", "Scribe name is missing.");
    }
}