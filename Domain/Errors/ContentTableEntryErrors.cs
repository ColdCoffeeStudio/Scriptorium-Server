using Domain.Shared;

namespace Domain.Errors;

public class ContentTableEntryErrors
{
    public Error MissingId()
    {
        return new Error("ContentTableEntryErrors.MissingId", "The content table entry Id must be greater than 0.");
    }

    public Error MissingThemeName()
    {
        return new Error("ContentTableEntryErrors.MissingThemeName", "The content table entry theme name is missing.");
    }
}