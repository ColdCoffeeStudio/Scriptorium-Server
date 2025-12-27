using Application.DTO;
using Domain.Entities;

namespace Application.Common.Mappers;

public class EncyclopediaToDtoMapper
{
    public EncyclopediaDto Map(Encyclopedia encyclopedia)
    {
        EncyclopediaDto encyclopediaDto = new EncyclopediaDto(
            encyclopedia.Id,
            encyclopedia.Title,
            encyclopedia.Scribe.Id,
            encyclopedia.Scribe.Name
        );
        
        return encyclopediaDto;
    }
}