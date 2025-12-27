using Domain.Shared;

namespace Application.DTO;

public record AnswerListDto(bool Success, List<IDto> Value, Error error);