using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Theme : IEntity<Theme>
{
    public int Id { get; }
    public string Name { get; }
    public string Folder { get; }

    public static Result<Theme> Create(int id, string name, string folder)
    {
        ThemeErrors errors =  new ThemeErrors();
        Result<Theme> result;

        if (id <= 0)
        {
            result = new Result<Theme>(Theme.Empty(), errors.MissingId(), false);
        }
        else if (String.IsNullOrEmpty(name))
        {
            result = new Result<Theme>(Theme.Empty(), errors.MissingName(), false);
        }
        else if (String.IsNullOrEmpty(folder))
        {
            result = new Result<Theme>(Theme.Empty(), errors.MissingFolder(), false);
        }
        else
        {
            result = IncorrectFolder(folder) 
                ? new Result<Theme>(Theme.Empty(), errors.InvalidFolder(), false) 
                : new Result<Theme>(new Theme(id, name, folder), Error.Empty(), true);
        }
        
        return result;
    }

    private static bool IncorrectFolder(string folder)
    {
        return !folder.Any(Char.IsWhiteSpace);
    }

    private Theme(int id, string name, string folder)
    {
        Id = id;
        Name = name;
        Folder = folder;
    }

    public static Theme Empty()
    {
        return new Theme(-1,"","");
    }
}