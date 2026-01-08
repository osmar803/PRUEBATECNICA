using AutoMapper;
using Aplication.DTOs;
using Domain.repository;
using Prueba.Domain;
using Prueba.Domain.Exceptions;

namespace Prueba.Application.Services;

public class MunicipioService
{
    private readonly IMunicipioRepository _municipioRepository;
    private readonly IMapper _mapper;

    public MunicipioService(IMunicipioRepository municipioRepository, IMapper mapper)
    {
        _municipioRepository = municipioRepository;
        _mapper = mapper;
    }

    // 🔹 Obtener todos los municipios
    public async Task<List<MunicipioResponseDto>> ObtenerTodosAsync()
    {
        var municipios = await _municipioRepository.ObtenerTodosAsync();
        return _mapper.Map<List<MunicipioResponseDto>>(municipios);
    }

    // 🔹 Obtener municipio por Id
    public async Task<MunicipioResponseDto> ObtenerPorIdAsync(Guid id)
    {
        var municipio = await _municipioRepository.ObtenerPorIdAsync(id);
        if (municipio == null)
            throw new EntidadNoEncontradaException("Municipio", id);

        return _mapper.Map<MunicipioResponseDto>(municipio);
    }

    // 🔹 Obtener municipios por departamento
    public async Task<List<MunicipioResponseDto>> ObtenerPorDepartamentoAsync(Guid departamentoId)
    {
        var municipios = await _municipioRepository.ObtenerPorDepartamentoAsync(departamentoId);
        return _mapper.Map<List<MunicipioResponseDto>>(municipios);
    }

    // 🔹 Crear municipio
    public async Task<Guid> CrearAsync(MunicipioCreateDto dto)
    {
        var municipio = _mapper.Map<Municipio>(dto);
        await _municipioRepository.AgregarAsync(municipio);
        return municipio.Id;
    }

    // 🔹 Actualizar municipio
    public async Task ActualizarAsync(MunicipioUpdateDto dto)
    {
        var municipio = await _municipioRepository.ObtenerPorIdAsync(dto.Id);
        if (municipio == null)
            throw new EntidadNoEncontradaException("Municipio", dto.Id);

        // Mapear propiedades del DTO a la entidad existente
        _mapper.Map(dto, municipio);

        await _municipioRepository.ActualizarAsync(municipio);
    }

    // 🔹 Eliminar municipio
    public async Task EliminarAsync(Guid id)
    {
        var municipio = await _municipioRepository.ObtenerPorIdAsync(id);
        if (municipio == null)
            throw new EntidadNoEncontradaException("Municipio", id);

        await _municipioRepository.EliminarAsync(id);
    }
}
