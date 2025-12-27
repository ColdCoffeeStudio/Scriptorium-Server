using Domain.Shared;

namespace Domain.Errors;

public class EncyclopediaListErrors
{
    public Error InvalidEncyclopedia()
    {
        return new Error("EncyclopediaList.InvalidEncyclopedia", "There is an invalid Encyclopedia in the list.");
    }
}