
namespace Aplication.DTOs;
public record DepartamentoCreateDto(
    string Nombre,
    Guid PaisId
);

public record DepartamentoUpdateDto(
    Guid Id,
    string Nombre,
    Guid PaisId
);

public record DepartamentoResponseDto(
    Guid Id,
    string Nombre,
    Guid PaisId
);
