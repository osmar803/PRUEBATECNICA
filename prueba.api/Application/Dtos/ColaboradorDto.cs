namespace Aplication.DTOs;

public record ColaboradorCreateDto(
    string NombreCompleto,
    int Edad,
    string Telefono,
    string CorreoElectronico
);

public record ColaboradorUpdateDto(
    Guid Id,
    string NombreCompleto,
    int Edad,
    string Telefono,
    string CorreoElectronico
);

public record ColaboradorResponseDto(
    Guid Id,
    string NombreCompleto,
    int Edad,
    string Telefono,
    string CorreoElectronico
);
