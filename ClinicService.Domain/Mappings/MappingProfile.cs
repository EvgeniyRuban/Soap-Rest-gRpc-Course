using AutoMapper;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;

namespace ClinicService.Domain.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateClientMapping();
    }

    private void CreateClientMapping()
    {
        CreateMap<Client, ClientResponse>()
            .ForMember(dest => dest.Id, c => c.MapFrom(src => src.Id));

        CreateMap<CreateClientRequest, Client>()
            .ForMember(dest => dest.FirstName, c => c.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.Surname, c => c.MapFrom(src => src.Surname))
            .ForMember(dest => dest.Patronymic, c => c.MapFrom(src => src.Patronymic))
            .ForMember(dest => dest.Document, c => c.MapFrom(src => src.Document));

        CreateMap<UpdateClientRequest, Client>()
            .ForMember(response => response.Id, c => c.MapFrom(src => src.ClientToUpdateId))
            .ForMember(response => response.FirstName, c => c.MapFrom(src => src.FirstName))
            .ForMember(response => response.Surname, c => c.MapFrom(src => src.Surname))
            .ForMember(response => response.Patronymic, c => c.MapFrom(src => src.Patronymic))
            .ForMember(response => response.Document, c => c.MapFrom(src => src.Document));
    }
}