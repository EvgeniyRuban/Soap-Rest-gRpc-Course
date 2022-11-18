using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos;

/// <summary>Crud clients repository.</summary>
/// <inheritdoc/>
public interface IClientRepository : ICrudRepository<Client, int>
{
}