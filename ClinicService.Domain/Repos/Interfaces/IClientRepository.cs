using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos;

public interface IClientRepository : ICrudRepository<Client, int>
{
}