using AutoMapper;
using Prueba.Domain;
using Aplication.DTOs;

namespace Application.Mappings;

public class DtoToDomainProfile : Profile
{
    public DtoToDomainProfile()
    {
        // País
        CreateMap<PaisCreateDto, Pais>()
            .ConstructUsing(dto => new Pais(dto.Nombre));

        // Departamento
        CreateMap<DepartamentoCreateDto, Departamento>()
            .ConstructUsing(dto => new Departamento(dto.Nombre, dto.PaisId));

        // Municipio
        CreateMap<MunicipioCreateDto, Municipio>()
            .ConstructUsing(dto => new Municipio(dto.Nombre, dto.DepartamentoId));

        // Empresa
        CreateMap<EmpresaCreateDto, Empresa>()
            .ConstructUsing(dto =>
                new Empresa(
                    dto.Nit,
                    dto.RazonSocial,
                    dto.NombreComercial,
                    dto.Telefono,
                    dto.CorreoElectronico,
                    dto.MunicipioId
                ));

        // Colaborador
        CreateMap<ColaboradorCreateDto, Colaborador>()
            .ConstructUsing(dto =>
                new Colaborador(
                    dto.NombreCompleto,
                    dto.Edad,
                    dto.Telefono,
                    dto.CorreoElectronico
                ));
    }
}
