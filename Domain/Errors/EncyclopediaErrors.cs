using Domain.Shared;

namespace Domain.Errors;

public class EncyclopediaErrors
{
    public Error MissingTitle()
    {
        return new Error("Encyclopedia.MissingTitle", 
            "Encyclopedia title is missing.");
    }

    public Error MissingScribe()
    {
        return new Error("Encyclopedia.MissingScribe", "Encyclopedia Scribe can't be empty.");
    }
}