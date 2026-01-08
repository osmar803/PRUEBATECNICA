using AutoMapper;
using Prueba.Domain;
using Aplication.DTOs;


namespace Application.Mappings;

public class DomainToDtoProfile : Profile
{
    public DomainToDtoProfile()
    {
        // País
        CreateMap<Pais, PaisResponseDto>();

        // Departamento
        CreateMap<Departamento, DepartamentoResponseDto>();

        // Municipio
        CreateMap<Municipio, MunicipioResponseDto>();

        // Empresa
        CreateMap<Empresa, EmpresaResponseDto>();

        // Colaborador
        CreateMap<Colaborador, ColaboradorResponseDto>();
    }
}
