using Application.DTO;
using Domain.Entities;
using Domain.Shared;

namespace Application.Common.Mappers;

public class EncyclopediaListAnswerListDtoMapper
{
    public AnswerListDto Map(EncyclopediaList list)
    {
        List<IDto> encyclopediaDtos = new List<IDto>();
        EncyclopediaToDtoMapper mapper = new EncyclopediaToDtoMapper();
        
        foreach (Encyclopedia encyclopedia in list.List)
        {
            encyclopediaDtos.Add(mapper.Map(encyclopedia));
        }

        return new AnswerListDto(true,  encyclopediaDtos, Error.Empty());
    }
}