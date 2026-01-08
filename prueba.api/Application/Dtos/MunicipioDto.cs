namespace Aplication.DTOs;
public record MunicipioCreateDto(
    string Nombre,
    Guid DepartamentoId
);

public record MunicipioUpdateDto(
    Guid Id,
    string Nombre,
    Guid DepartamentoId
);

public record MunicipioResponseDto(
    Guid Id,
    string Nombre,
    Guid DepartamentoId
);
