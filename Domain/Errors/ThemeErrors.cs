using Domain.Shared;

namespace Domain.Errors;

public class ThemeErrors
{
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