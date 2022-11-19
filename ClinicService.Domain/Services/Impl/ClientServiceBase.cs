using AutoMapper;
using ClinicService.Domain.Entities;
using ClinicService.Domain.Repos;

namespace ClinicService.Domain.Services;

public abstract class ClientServiceBase : CrudServiceBase<Client, int>
{
    protected ClientServiceBase(IClientRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}