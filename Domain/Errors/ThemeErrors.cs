using Domain.Shared;

namespace Domain.Errors;

public class ThemeErrors
{
    public Error MissingId()
    {
        return new Error("Theme.MissingId", "Theme id must be greater than 0.");
    }
    
    public Error MissingName()
    {
        return new Error("Theme.MissingName", "Theme name is missing.");
    }

    public Error MissingFolder()
    {
        return new Error("Theme.MissingFolder", "Theme folder is missing.");
    }
    
    public Error InvalidFolder()
    {
        return new Error("Theme.InvalidFolder", "The folder contains invalid characters.");
    }

}