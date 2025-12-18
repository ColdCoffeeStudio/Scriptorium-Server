using Domain.Shared;

namespace Domain.Errors;

public class EncyclopediaRepositoryErrors
{
    public Error DatabaseConnectionError()
    {
        return new Error("EncyclopediaRepository.DatabaseConnectionError", "Can't connect to the database");
    }
    
    public Error InvalidData(int encyclopediaId, Error error)
    {
        return new Error("EncyclopediaRepository.InvalidData", $"Something went wrong with the fetched data for the encyclopediaId '{encyclopediaId}': {error.Message}");
    }

    public static Error NoScribeFound(Guid scribeId)
    {
        return new Error("EncyclopediaRepository.NoScribeFound", $"No scribe was found for the ScribeId '{scribeId}'.");
    }
}