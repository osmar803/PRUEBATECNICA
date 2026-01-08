namespace Aplication.DTOs;

public record PaisCreateDto(string Nombre);

public record PaisUpdateDto(Guid Id, string Nombre);

public record PaisResponseDto(Guid Id, string Nombre);
