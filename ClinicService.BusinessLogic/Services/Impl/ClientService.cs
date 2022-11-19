using AutoMapper;
using ClinicService.Domain.Repos;
using ClinicService.Domain.Services;

namespace ClinicService.BusinessLogic.Services;

public class ClientService : ClientServiceBase
{
    public ClientService(IClientRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}