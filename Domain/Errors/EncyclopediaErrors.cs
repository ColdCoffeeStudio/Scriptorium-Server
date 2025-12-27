using Domain.Shared;

namespace Domain.Errors;

public class EncyclopediaErrors
{
    public Error MissingId()
    {
        return new Error("Encyclopedia.MissingId", "Encyclopedia Id must be greater than 0.");
    }
    
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