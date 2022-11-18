using ClinicService.Domain.Entities;
using ClinicService.Domain.Exceptions;
using ClinicService.Domain.Repos;
using Microsoft.EntityFrameworkCore;

namespace ClinicService.DAL.Repos;

public class ClientRepository : IClientRepository
{
    private readonly ClinicServiceDbContext _dbContext;


    public ClientRepository(ClinicServiceDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        _dbContext = dbContext;
    }


    public async Task<int> Add(Client entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var newEntity = await _dbContext.Clients.AddAsync(entity);
        await _dbContext.SaveChangesAsync(stoppingToken);
        return newEntity.Entity.Id;
    }
    public async Task Delete(int id, CancellationToken stoppingToken = default)
    {
        var entityToDelete = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id, stoppingToken);
        if (entityToDelete is null)
        {
            throw new EntityNotFoundException();
        }
        _dbContext.Clients.Remove(entityToDelete);
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
    public async Task<Client?> Get(int id, CancellationToken stoppingToken = default)
        => await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == id, stoppingToken);
    public async Task<IReadOnlyCollection<Client>> GetAll(CancellationToken stoppingToken)
        => await _dbContext.Clients.ToListAsync();
    public async Task Update(Client entity, CancellationToken stoppingToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        var entityToUpdate = await _dbContext.Clients.FirstOrDefaultAsync(c => c.Id == entity.Id, stoppingToken);
        if (entityToUpdate is null)
        {
            throw new EntityNotFoundException();
        }
        entityToUpdate.FirstName = entity.FirstName;
        entityToUpdate.Surname = entity.Surname;
        entityToUpdate.Patronymic = entity.Patronymic;
        entityToUpdate.Document = entity.Document;
        await _dbContext.SaveChangesAsync(stoppingToken);
    }
}