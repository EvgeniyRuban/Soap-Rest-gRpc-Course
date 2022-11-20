using ClinicService.Domain.Entities;

namespace ClinicService.Domain.Repos.Interfaces
{
    /// <summary>Crud accounts repository.</summary>
    /// <inheritdoc/>
    public interface IAccountRepository : ICrudRepository<Account, int>
    {
    }
}