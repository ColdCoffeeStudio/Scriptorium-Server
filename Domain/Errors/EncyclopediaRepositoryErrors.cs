using Domain.Shared;

namespace Domain.Errors;

public class EncyclopediaRepositoryErrors
{
    public Error DatabaseConnectionError()
    {
        return new Error("EncyclopediaRepository.DatabaseConnectionError", "Can't connect to the database");
    }
    
    public Error InvalidData(Error error)
    {
        return new Error($"EncyclopediaRepository.InvalidData", $"Something went wrong with the fetched data : {error.ToString()}");
    }
}