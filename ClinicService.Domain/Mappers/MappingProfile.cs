﻿using AutoMapper;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Models;

namespace ClinicService.Domain.Mappers;

public sealed class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateClientMapping();
    }

    public void CreateClientMapping()
    {
        CreateMap<CreateClientRequest, Client>();
        CreateMap<UpdateClientRequest, Client>().ForMember(dest => dest.Id, c => c.MapFrom(src => src.ClientToUpdateId));
        CreateMap<Client, ClientDto>();
    }
}