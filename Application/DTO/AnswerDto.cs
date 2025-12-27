using Domain.Shared;

namespace Application.DTO;

public record AnswerDto(bool Success, IDto Value, Error error);