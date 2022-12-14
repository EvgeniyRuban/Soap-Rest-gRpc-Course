using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Repos;
using Microsoft.EntityFrameworkCore;

namespace ClinicService.DAL.Repos;

public class AccountRepository : IAccountRepository
{
    private readonly ClinicServiceDbContext _dbContext;


    public AccountRepository(ClinicServiceDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        _dbContext = dbContext;
    }


    public async Task<int> Add(Account entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var result = await _dbContext.Accounts.AddAsync(entity, stoppingToken);
        await _dbContext.SaveChangesAsync(stoppingToken);
        return result.Entity.Id;
    }
    public async Task Delete(int id, CancellationToken stoppingToken = default)
    {
        var entity = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id && !a.Locked, stoppingToken);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        _dbContext.Accounts.Remove(entity);
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
    public async Task<Account?> Get(int id, CancellationToken stoppingToken = default)
        => await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == id && !a.Locked, stoppingToken);
    public async Task<Account?> Get(string login, CancellationToken stoppingToken = default)
    {
        if (string.IsNullOrWhiteSpace(login))
        {
            throw new ArgumentException(login, nameof(login));
        }
        return await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Email == login && !a.Locked, stoppingToken);
    }
    public async Task<IReadOnlyCollection<Account>> GetAll(CancellationToken stoppingToken = default)
        => await _dbContext.Accounts.Where(a => !a.Locked).ToListAsync(stoppingToken);
    public async Task Update(Account entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var entityToUpdate = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.Id == entity.Id && !a.Locked, stoppingToken);
        if (entityToUpdate is null)
        {
            throw new EntityNotFoundException();
        }
        entityToUpdate.Email = entity.Email;
        entityToUpdate.FirstName = entity.FirstName;
        entityToUpdate.Surname = entity.Surname;
        entityToUpdate.Patronymic = entity.Patronymic;
        entityToUpdate.PasswordHash = entity.PasswordHash;
        entityToUpdate.PasswordSalt = entity.PasswordSalt;
        entityToUpdate.Locked = entity.Locked;
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
}