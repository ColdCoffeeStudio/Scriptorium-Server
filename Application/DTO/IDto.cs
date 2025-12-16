using System.Text.Json.Serialization;

namespace Application.DTO;

[JsonDerivedType(typeof(EncyclopediaDto), typeDiscriminator: "encyclopedia")]

public interface IDto
{
    
}