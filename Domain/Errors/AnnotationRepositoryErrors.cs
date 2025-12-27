using Domain.Shared;

namespace Domain.Errors;

public class AnnotationRepositoryErrors
{
    public Error DatabaseConnectionError()
    {
        return new Error("AnnotationRepositoryErrors.DatabaseConnectionError()", "Can't connect to the database");
    }

    public Error AnnotationCreationError(int annotationId, Error error)
    {
        return new Error("AnnotationRepositoryErrors.AnnotationCreationError", $"Something went wrong while initiating the annotation '{annotationId}': {error.Message}");
    }

    public Error AnnotationListCreationError(Error error)
    {
        return new Error("AnnotationRepositoryErrors.AnnotationListCreationError", $"Something went wrong while initiating the annotation list: {error.Message}");
    }

    public Error EncyclopediaCreationError(int encyclopediaId, Error error)
    {
        return new Error("AnnotationRepositoryErrors.EncyclopediaCreationError", $"Something went wrong while initiating the encyclopedia '{encyclopediaId}': {error.Message}");
    }

    public Error ThemeCreationError(int annotationId, int themeId, Error error)
    {
        return new Error("AnnotationRepositoryErrors.ThemeCreationError", $"Something went wrong while initiating the theme '{themeId}' for the annotation '{annotationId}': {error.Message}");
    }

    public Error ScribeCreationError(string scribeId, Error error)
    {
        return new Error("AnnotationRepositoryErrors.ScribeCreationError", $"Something went wrong while initiating the scribe '{scribeId}': {error.Message}");
    }
    
    public Error AnnotationNotFound(int id)
    {
        return new Error("AnnotationRepositoryErrors.AnnotationNotFound",
            $"No annotation found in the database for the id '{id}'.");
    }

    public Error EncyclopediaNotFound(int encyclopediaId)
    {
        return new Error("AnnotationRepositoryErrors.EncyclopediaNotFound",
            $"No encyclopedia found in the database for the id '{encyclopediaId}'.");
    }

    public Error ThemeNotFound(int themeId)
    {
        return new Error("AnnotationRepositoryErrors.ThemeNotFound",
            $"No theme found in the database for the id '{themeId}'.");
    }

    public Error ScribeNotFound(string scribeId)
    {
        return new Error("AnnotationRepositoryErrors.ScribeNotFound",
            $"No scribe found in the database for the id '{scribeId}'.");
    }

}