using AutoMapper;
using Aplication.DTOs;
using Prueba.Domain;
using Prueba.Domain.Exceptions;
using Domain.repository;


namespace Prueba.Application.Services;

public class DepartamentoService
{
    private readonly IDepartamentoRepository _departamentoRepository;
    private readonly IMapper _mapper;

    public DepartamentoService(IDepartamentoRepository departamentoRepository, IMapper mapper)
    {
        _departamentoRepository = departamentoRepository;
        _mapper = mapper;
    }

    // 🔹 Obtener todos
    public async Task<List<DepartamentoResponseDto>> ObtenerTodosAsync()
    {
        var departamentos = await _departamentoRepository.ObtenerTodosAsync();
        return _mapper.Map<List<DepartamentoResponseDto>>(departamentos);
    }

    // 🔹 Obtener por ID
    public async Task<DepartamentoResponseDto> ObtenerPorIdAsync(Guid id)
    {
        var departamento = await _departamentoRepository.ObtenerPorIdAsync(id);
        if (departamento == null)
            throw new EntidadNoEncontradaException("Departamento", id);

        return _mapper.Map<DepartamentoResponseDto>(departamento);
    }

    // 🔹 Obtener por país
    public async Task<List<DepartamentoResponseDto>> ObtenerPorPaisAsync(Guid paisId)
    {
        if (paisId == Guid.Empty)
            throw new ArgumentException("El país es obligatorio.");

        var departamentos = await _departamentoRepository.ObtenerPorPaisAsync(paisId);
        return _mapper.Map<List<DepartamentoResponseDto>>(departamentos);
    }

    // 🔹 Crear
    public async Task<Guid> CrearAsync(DepartamentoCreateDto dto)
    {
        var departamento = _mapper.Map<Departamento>(dto);
        await _departamentoRepository.AgregarAsync(departamento);
        return departamento.Id;
    }

    // 🔹 Actualizar
    public async Task ActualizarAsync(DepartamentoUpdateDto dto)
    {
        var departamento = await _departamentoRepository.ObtenerPorIdAsync(dto.Id);
        if (departamento == null)
            throw new EntidadNoEncontradaException("Departamento", dto.Id);

        // Respetando reglas de negocio
        departamento.CambiarNombre(dto.Nombre);

        if (departamento.PaisId != dto.PaisId)
        {
            departamento.CambiarPais(dto.PaisId); // Hacemos público el método o lo llamamos directamente
        }

        await _departamentoRepository.ActualizarAsync(departamento);
    }

    // 🔹 Eliminar
    public async Task EliminarAsync(Guid id)
    {
        var departamento = await _departamentoRepository.ObtenerPorIdAsync(id);
        if (departamento == null)
            throw new EntidadNoEncontradaException("Departamento", id);

        await _departamentoRepository.EliminarAsync(id);
    }
}