using Domain.Errors;
using Domain.Shared;

namespace Domain.Entities;

public class Theme : IEntity<Theme>
{
    private Guid Id { get; }
    private string Name { get; }
    private string Folder { get; }

    public static Result<Theme> Create(string name, string folder)
    {
        ThemeErrors errors =  new ThemeErrors();
        Result<Theme> result;

        if (String.IsNullOrEmpty(name))
        {
            result = new Result<Theme>(Theme.Empty(), errors.MissingName(), false);
        }
        else
        {
            if (String.IsNullOrEmpty(folder))
            {
                result = new Result<Theme>(new Theme(Guid.NewGuid(), name, folder), Error.Empty(), true);

            }
            else
            {
                result = IncorrectFolder(folder) 
                    ? new Result<Theme>(Theme.Empty(), errors.InvalidFolder(), false) 
                    : new Result<Theme>(Theme.Empty(), errors.MissingFolder(), false);
            }
        }

        return result;
    }

    private static bool IncorrectFolder(string folder)
    {
        return !folder.Any(Char.IsWhiteSpace);
    }

    private Theme(Guid id, string name, string folder)
    {
        Id = id;
        Name = name;
        Folder = folder;
    }

    public static Theme Empty()
    {
        return new Theme(Guid.Empty,"","");
    }
}