using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ClinicService.DAL.Repos;

public class AccountSessionRepository : IAccountSessionRepository
{
    private readonly ClinicServiceDbContext _dbContext;


    public AccountSessionRepository(ClinicServiceDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        _dbContext = dbContext;
    }


    public async Task<int> Add(AccountSession entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var result = await _dbContext.AccountSessions.AddAsync(entity, stoppingToken);
        await _dbContext.SaveChangesAsync(stoppingToken);
        return result.Entity.Id;
    }
    public async Task Delete(int id, CancellationToken stoppingToken = default)
    {
        var entity = await _dbContext.AccountSessions.FirstOrDefaultAsync(a => a.Id == id, stoppingToken);
        if (entity is null)
        {
            throw new EntityNotFoundException();
        }
        _dbContext.AccountSessions.Remove(entity);
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
    public async Task<AccountSession?> Get(int id, CancellationToken stoppingToken = default)
        => await _dbContext.AccountSessions.FirstOrDefaultAsync(a => a.Id == id, stoppingToken);
    public async Task<IReadOnlyCollection<AccountSession>> GetAll(CancellationToken stoppingToken = default)
        => await _dbContext.AccountSessions.ToListAsync(stoppingToken);
    public async Task Update(AccountSession entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var entityToUpdate = await _dbContext.AccountSessions.FirstOrDefaultAsync(a => a.Id == entity.Id, stoppingToken);
        if (entityToUpdate is null)
        {
            throw new EntityNotFoundException();
        }
        entityToUpdate.SessionToken = entity.SessionToken;
        entityToUpdate.TimeCreated = entity.TimeCreated;
        entityToUpdate.TimeLastRequest = entity.TimeLastRequest;
        entityToUpdate.TimeClosed = entity.TimeClosed;
        entityToUpdate.IsClosed = entity.IsClosed;
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
}