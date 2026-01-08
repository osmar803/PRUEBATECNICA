using AutoMapper;
using Aplication.DTOs;
using Prueba.Domain;
using Prueba.Domain.Exceptions;
using Domain.repository;

public class ColaboradorService
{
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IMapper _mapper;

    public ColaboradorService(IColaboradorRepository colaboradorRepository, IMapper mapper)
    {
        _colaboradorRepository = colaboradorRepository;
        _mapper = mapper;
    }

    // 🔹 Obtener todos
    public async Task<List<ColaboradorResponseDto>> ObtenerTodosAsync()
    {
        var colaboradores = await _colaboradorRepository.ObtenerTodosAsync();
        return _mapper.Map<List<ColaboradorResponseDto>>(colaboradores);
    }

    // 🔹 Obtener por Id
    public async Task<ColaboradorResponseDto> ObtenerPorIdAsync(Guid id)
    {
        var colaborador = await _colaboradorRepository.ObtenerPorIdAsync(id);
        if (colaborador == null)
            throw new EntidadNoEncontradaException("Colaborador", id);

        return _mapper.Map<ColaboradorResponseDto>(colaborador);
    }

    // 🔹 Crear
    public async Task<Guid> CrearAsync(ColaboradorCreateDto dto)
    {
        var colaborador = _mapper.Map<Colaborador>(dto);
        await _colaboradorRepository.AgregarAsync(colaborador);
        return colaborador.Id;
    }

    // 🔹 Actualizar
    public async Task ActualizarAsync(ColaboradorUpdateDto dto)
    {
        var colaborador = await _colaboradorRepository.ObtenerPorIdAsync(dto.Id);
        if (colaborador == null)
            throw new EntidadNoEncontradaException("Colaborador", dto.Id);

        // Actualiza solo las propiedades necesarias respetando las reglas de dominio
        colaborador.CambiarNombre(dto.NombreCompleto);
        colaborador.CambiarEdad(dto.Edad);
        colaborador.CambiarTelefono(dto.Telefono);
        colaborador.CambiarCorreo(dto.CorreoElectronico);

        await _colaboradorRepository.ActualizarAsync(colaborador);
    }

    // 🔹 Eliminar
    public async Task EliminarAsync(Guid id)
    {
        var colaborador = await _colaboradorRepository.ObtenerPorIdAsync(id);
        if (colaborador == null)
            throw new EntidadNoEncontradaException("Colaborador", id);

        await _colaboradorRepository.EliminarAsync(id);
    }

    // 🔹 Asignar colaborador a empresa
    public async Task AsignarEmpresaAsync(Guid colaboradorId, Guid empresaId)
    {
        var colaborador = await _colaboradorRepository.ObtenerPorIdAsync(colaboradorId);
        if (colaborador == null)
            throw new EntidadNoEncontradaException("Colaborador", colaboradorId);

        colaborador.AsignarEmpresa(empresaId);
        await _colaboradorRepository.ActualizarAsync(colaborador);
    }
}