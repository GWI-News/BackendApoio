using AutoMapper;
using gwiBack.Application.DTOs;
using gwiBack.Domain.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Formation, FormationDTO>();
        CreateMap<FormationDTO, Formation>();
    }
}
