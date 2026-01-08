namespace Aplication.DTOs;

public record CrearAdministradorDto(
    string Usuario,
    string Clave
);

public record LoginRequestDto(
    string Usuario,
    string Clave
);

public record LoginResponseDto(
    string Token
);
