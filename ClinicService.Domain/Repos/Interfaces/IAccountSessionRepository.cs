using ClinicService.Domain.Entities;
using ClinicService.Domain.Repos;
/// <summary>Crud accounts repository.</summary>
/// <inheritdoc/>
public interface IAccountSessionRepository : ICrudRepository<AccountSession, int>
{
}