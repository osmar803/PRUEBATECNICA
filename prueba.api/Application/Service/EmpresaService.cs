using AutoMapper;
using Aplication.DTOs;
using Prueba.Domain;
using Prueba.Domain.Exceptions;
using Domain.repository;

public class EmpresaService
{
    private readonly IEmpresaRepository _empresaRepository;
    private readonly IMapper _mapper;

    public EmpresaService(IEmpresaRepository empresaRepository, IMapper mapper)
    {
        _empresaRepository = empresaRepository;
        _mapper = mapper;
    }

    // 🔹 Obtener todas las empresas
    public async Task<List<EmpresaResponseDto>> ObtenerTodosAsync()
    {
        var empresas = await _empresaRepository.ObtenerTodosAsync();
        return _mapper.Map<List<EmpresaResponseDto>>(empresas);
    }

    // 🔹 Obtener empresa por Id
    public async Task<EmpresaResponseDto> ObtenerPorIdAsync(Guid id)
    {
        var empresa = await _empresaRepository.ObtenerPorIdAsync(id);
        if (empresa == null)
            throw new EntidadNoEncontradaException("Empresa", id);

        return _mapper.Map<EmpresaResponseDto>(empresa);
    }

    // 🔹 Crear nueva empresa
    public async Task<Guid> CrearAsync(EmpresaCreateDto dto)
    {
        var empresaExistente = await _empresaRepository.ObtenerPorNitAsync(dto.Nit);
        if (empresaExistente != null)
            throw new ConflictoDominioException("El NIT ya está registrado");

        var empresa = _mapper.Map<Empresa>(dto); // Mapeo directo del DTO a la entidad
        await _empresaRepository.AgregarAsync(empresa);
        return empresa.Id;
    }

    // 🔹 Actualizar empresa
    public async Task ActualizarAsync(EmpresaUpdateDto dto)
    {
        var empresa = await _empresaRepository.ObtenerPorIdAsync(dto.Id);
        if (empresa == null)
            throw new EntidadNoEncontradaException("Empresa", dto.Id);

        // Actualizar las propiedades respetando las reglas de la entidad
        empresa.CambiarNit(dto.Nit);
        empresa.CambiarRazonSocial(dto.RazonSocial);
        empresa.CambiarNombreComercial(dto.NombreComercial);
        empresa.CambiarTelefono(dto.Telefono);
        empresa.CambiarCorreo(dto.CorreoElectronico);
        empresa.CambiarMunicipio(dto.MunicipioId);

        await _empresaRepository.ActualizarAsync(empresa);
    }

    // 🔹 Eliminar empresa
    public async Task EliminarAsync(Guid id)
    {
        var empresa = await _empresaRepository.ObtenerPorIdAsync(id);
        if (empresa == null)
            throw new EntidadNoEncontradaException("Empresa", id);

        await _empresaRepository.EliminarAsync(id);
    }

    // 🔹 Asignar colaborador a empresa
    public async Task AsignarColaboradorAsync(Guid empresaId, Guid colaboradorId)
    {
        var empresa = await _empresaRepository.ObtenerPorIdAsync(empresaId);
        if (empresa == null)
            throw new EntidadNoEncontradaException("Empresa", empresaId);

        empresa.AsignarColaborador(colaboradorId);
        await _empresaRepository.ActualizarAsync(empresa);
    }
}