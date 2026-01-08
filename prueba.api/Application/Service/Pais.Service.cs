using AutoMapper;
using Aplication.DTOs;
using Prueba.Domain;
using Prueba.Domain.Exceptions;
using  Domain.repository;


namespace Prueba.Application.Services;

public class PaisService
{
    private readonly IPaisRepository _paisRepository;
    private readonly IMapper _mapper;

    public PaisService(IPaisRepository paisRepository, IMapper mapper)
    {
        _paisRepository = paisRepository;
        _mapper = mapper;
    }

    // 🔹 Obtener todos
    public async Task<List<PaisResponseDto>> ObtenerTodosAsync()
    {
        var paises = await _paisRepository.ObtenerTodosAsync();
        return _mapper.Map<List<PaisResponseDto>>(paises);
    }

    // 🔹 Obtener por ID
    public async Task<PaisResponseDto> ObtenerPorIdAsync(Guid id)
    {
        var pais = await _paisRepository.ObtenerPorIdAsync(id);
        if (pais == null)
            throw new EntidadNoEncontradaException("País", id);

        return _mapper.Map<PaisResponseDto>(pais);
    }

    // 🔹 Crear
    public async Task<Guid> CrearAsync(PaisCreateDto dto)
    {
        var paisExistente = await _paisRepository.ObtenerPorNombreAsync(dto.Nombre);
        if (paisExistente != null)
            throw new ConflictoDominioException("Ya existe un país con ese nombre.");

        var pais = _mapper.Map<Pais>(dto); // Mapea PaisCreateDto -> Pais
        await _paisRepository.CrearAsync(pais);
        return pais.Id;
    }

    // 🔹 Actualizar
    public async Task ActualizarAsync(PaisUpdateDto dto)
    {
        var pais = await _paisRepository.ObtenerPorIdAsync(dto.Id);
        if (pais == null)
            throw new EntidadNoEncontradaException("País", dto.Id);

        var paisConMismoNombre = await _paisRepository.ObtenerPorNombreAsync(dto.Nombre);
        if (paisConMismoNombre != null && paisConMismoNombre.Id != dto.Id)
            throw new ConflictoDominioException("Ya existe otro país con ese nombre.");

        // Mapear solo las propiedades del DTO a la entidad existente
        _mapper.Map(dto, pais);

        await _paisRepository.ActualizarAsync(pais);
    }

    // 🔹 Eliminar
    public async Task EliminarAsync(Guid id)
    {
        var pais = await _paisRepository.ObtenerPorIdAsync(id);
        if (pais == null)
            throw new EntidadNoEncontradaException("País", id);

        await _paisRepository.EliminarAsync(pais);
    }
}