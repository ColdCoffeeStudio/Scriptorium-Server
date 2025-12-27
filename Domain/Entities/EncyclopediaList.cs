using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class EncyclopediaList : IEntity<EncyclopediaList>
{
    public Guid Id { get; }
    public List<Encyclopedia> List { get; }
    
    public static Result<EncyclopediaList> Create(List<Encyclopedia> encyclopedias)
    {
        Result<EncyclopediaList> result = new Result<EncyclopediaList>(new EncyclopediaList(Guid.NewGuid(), encyclopedias), Error.Empty(), true);
        EncyclopediaListErrors errors = new EncyclopediaListErrors();
        
        foreach (Encyclopedia encyclopedia in encyclopedias)
        {
            if (encyclopedia == Encyclopedia.Empty())
            {
                result = new Result<EncyclopediaList>(EncyclopediaList.Empty(), errors.InvalidEncyclopedia(), false);
            }
        }
        
        return result;
    }
    
    public static EncyclopediaList Empty()
    {
        return new EncyclopediaList(Guid.Empty, new List<Encyclopedia>());
    }

    private EncyclopediaList(Guid id, List<Encyclopedia> list)
    {
        Id = id;
        List = list;
    }
}