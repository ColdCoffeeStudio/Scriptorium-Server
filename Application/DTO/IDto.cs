using System.Text.Json.Serialization;

namespace Application.DTO;

[JsonDerivedType(typeof(EncyclopediaDto), typeDiscriminator: "encyclopedia")]
[JsonDerivedType(typeof(ContentTableEntryDto), typeDiscriminator: "contentTableEntry")]

public interface IDto
{
    
}